using PentagonLauncher.Shared.Models.Authentication;
using PentagonLauncher.Shared.Protocol;
using PentagonLauncher.Shared.Protocol.Serializers;
using PentagonLauncher.Shared.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PentagonLauncher.Launcher.Networking
{
    // Khuôn mẫu kết quả trả về cho UI của Thanh Hiền dễ sử dụng
    //public record BasicResponse(bool Success, string Message);
    //public record LoginResponse(bool Success, string Message, int UserId = 0, string Username = "");


    public class LauncherTcpClient : IDisposable
    {
        public static LauncherTcpClient Shared { get; } = new LauncherTcpClient();
        private TcpClient? _tcpClient;
        private NetworkStream? _stream;
        private CancellationTokenSource? _cts;
        private Task? _receiveTask;
        private readonly SemaphoreSlim _connectionLock = new(1, 1);

        // Hàng đợi đồng bộ cho cơ chế Request - Response
        private TaskCompletionSource<Packet>? _pendingRequest;
        private readonly SemaphoreSlim _requestLock = new(1, 1);

        public bool IsConnected => _tcpClient?.Connected ?? false;

        // Sự kiện bắn ra cho UI biết khi bị mất kết nối đột ngột với Server
        public event Action<string>? OnDisconnected;

        // 1. KẾT NỐI & NGẮT KẾT NỐI TCP
        public async Task<bool> ConnectAsync(string? host = null, int? port = null)
        {
            await _connectionLock.WaitAsync();
            try { return await ConnectCoreAsync(host, port); }
            finally { _connectionLock.Release(); }
        }

        private async Task<bool> ConnectCoreAsync(string? host, int? port)
        {
            if (IsConnected) return true;
            // The previous receive loop must finish before replacing its socket.
            if (_receiveTask is not null) await _receiveTask;
            _cts?.Dispose();

            EnvironmentFile.Load();
            host ??= Environment.GetEnvironmentVariable("SERVER_HOST") ?? "127.0.0.1";
            port ??= EnvironmentFile.GetPort("SERVER_PORT", 12345);

            try
            {
                _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(host, port.Value);
                _stream = _tcpClient.GetStream();
                _cts = new CancellationTokenSource();

                // Khởi chạy vòng lặp nhận dữ liệu chạy ngầm bên dưới Client
                _receiveTask = ReceiveLoopAsync(_cts.Token);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Client Error] Không thể kết nối tới Server: {ex.Message}");
                return false;
            }
        }

        public void Disconnect()
        {
            _cts?.Cancel();
            _pendingRequest?.TrySetException(new IOException("Kết nối đã đóng. Vui lòng đăng nhập lại."));
            try
            {
                _tcpClient?.Close();
            }
            catch { /* Bỏ qua lỗi khi đóng socket */ }
        }

        public async Task LogoutAsync()
        {
            await _connectionLock.WaitAsync();
            try
            {
            Disconnect();
            if (_receiveTask is not null) await _receiveTask;
            _stream = null;
            _tcpClient = null;
            }
            finally { _connectionLock.Release(); }
        }

        // 2. VÒNG LẶP NHẬN DỮ LIỆU TỪ SERVER (Chạy ngầm)
        private async Task ReceiveLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested && _stream != null && IsConnected)
                {
                    Packet? packet = await PacketStreamReader.ReadPacketOrNullAsync(_stream, token);
                    if (packet is null) break;

                    // Nếu Server gửi gói Ping (2000), tự động trả lời Pong (1000)
                    if (packet.Opcode == Opcode.S2C_Ping)
                    {
                        ConnectionPacketSerializer.ReadPingPacket(packet);
                        byte[] pongPacket = ConnectionPacketSerializer.CreatePongPacket();
                        await _stream.WriteAsync(pongPacket, 0, pongPacket.Length, token);
                        continue;
                    }

                    // Đánh thức hàm Request đang chờ kết quả từ Server
                    _pendingRequest?.TrySetResult(packet);
                }
            }
            catch (Exception ex)
            {
                _pendingRequest?.TrySetException(ex);
            }
            finally
            {
                bool intentional = token.IsCancellationRequested;
                Disconnect();
                if (!intentional) OnDisconnected?.Invoke("Đã mất kết nối với máy chủ Pentagon Launcher.");
            }
        }

        // 3. CƠ CHẾ REQUEST - RESPONSE CỐT LÕI
        private async Task<Packet> SendRequestAsync(byte[] packetBytes, int timeoutMs = 10000)
        {
            if (!IsConnected || _stream == null)
                throw new InvalidOperationException("Chưa kết nối tới máy chủ.");

            await _requestLock.WaitAsync();
            try
            {
                if (!IsConnected || _stream == null)
                    throw new IOException("Kết nối đã đóng.");
                _pendingRequest = new TaskCompletionSource<Packet>(TaskCreationOptions.RunContinuationsAsynchronously);

                // Gửi gói tin lên Server
                await _stream.WriteAsync(packetBytes, 0, packetBytes.Length);
                await _stream.FlushAsync();

                // Chờ Server phản hồi (tối đa 10 giây)
                using var timeoutCts = new CancellationTokenSource(timeoutMs);
                await using var reg = timeoutCts.Token.Register(() =>
                    _pendingRequest?.TrySetException(new TimeoutException("Chưa nhận được phản hồi; chưa xác định thao tác đã hoàn tất hay chưa.")));

                return await _pendingRequest.Task;
            }
            catch (TimeoutException)
            {
                Disconnect();
                throw;
            }
            finally
            {
                _pendingRequest = null;
                _requestLock.Release();
            }
        }

        // 4. CÁC HÀM API DÀNH CHO UI CỦA THANH HIỀN GỌI TRỰC TIẾP

        // 4.1. Đăng nhập (Gửi 1001 -> Nhận 2001)
        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            try
            {
                byte[] requestBytes = AuthPacketSerializer.CreareLoginRequest(new LoginRequest
                {
                    Username = username ?? string.Empty,
                    Password = password ?? string.Empty
                });

                Packet response = await SendRequestAsync(requestBytes);
                if (response.Opcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ErrorPacketSerializer.ReadErrorResponsePacket(response);
                    return new LoginResult { Success = errSuccess, Message = errMsg};
                }
                return AuthPacketSerializer.ReadLoginResult(response);
            }
            catch (Exception ex)
            {
                return new LoginResult { Success = false, Message = DescribeError(ex)};
            }
        }

        // 4.2. Đăng ký (Gửi 1002 -> Nhận 2002)
        public async Task<RegisterResult> RegisterAsync(string username, string email, string password)
        {
            try
            {
                byte[] requestBytes = AuthPacketSerializer.CreateRegisterPacket(new RegisterRequest
                {
                    Username = username ?? string.Empty,
                    Email = email ?? string.Empty,
                    Password = password ?? string.Empty
                });

                Packet response = await SendRequestAsync(requestBytes);
                if (response.Opcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ErrorPacketSerializer.ReadErrorResponsePacket(response);
                    return new RegisterResult { Success = errSuccess, Message = errMsg };
                }
                return AuthPacketSerializer.ReadRequestResult(response);
            }
            catch (Exception ex)
            {
                return new RegisterResult { Success = false, Message = DescribeError(ex) };
            }
        }

        // 4.3. Quên mật khẩu / Yêu cầu mã OTP (Gửi 1003 -> Nhận 2003)
        public async Task<ForgotPasswordResult> RequestPasswordResetAsync(string email)
        {
            try
            {
                byte[] requestBytes = AuthPacketSerializer.CreateForgotPasswordPacket(new ForgotPasswordRequest
                {
                    Email = email ?? string.Empty
                });

                Packet response = await SendRequestAsync(requestBytes);

                if (response.Opcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ErrorPacketSerializer.ReadErrorResponsePacket(response);
                    return new ForgotPasswordResult { Success = errSuccess, Message = errMsg };
                }
                return AuthPacketSerializer.ReadForgotPasswordResult(response);
            }
            catch (Exception ex)
            {
                return new ForgotPasswordResult { Success = false, Message = DescribeError(ex) };
            }
        }


        // 4.4. Đặt lại mật khẩu bằng OTP (Gửi 1004 -> Nhận 2004)
        public async Task<ResetPasswordResult> ResetPasswordAsync(string email, string newPassword)
        {
            try
            {
                byte[] requestBytes = AuthPacketSerializer.CreateResetPasswordPacket(new ResetPasswordRequest
                {
                    Email = email ?? string.Empty,
                    NewPassword = newPassword ?? string.Empty
                });

                Packet response = await SendRequestAsync(requestBytes);
                if (response.Opcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ErrorPacketSerializer.ReadErrorResponsePacket(response);
                    return new ResetPasswordResult { Success = errSuccess, Message = errMsg };
                }
                return AuthPacketSerializer.ReadResetPasswordResult(response);

            }
            catch (Exception ex)
            {
                return new ResetPasswordResult { Success = false, Message = DescribeError(ex) };
            }
        }

        // 4.5. Gửi yêu cầu lấy mã OTP xác thực Email Lúc đăng ký (gui 1005, nhan 2005)
        public async Task<OtpResult> RequestRegisterOTPAsync(string email, OTP.OtpPurpose purpose)
        {
            try
            {
                byte[] requestBytes = OtpPacketSerializer.CreateRegisterRequest(new RegisterOTPRequest
                {
                    Email = email ?? string.Empty,
                    Purpose = purpose
                });

                Packet response = await SendRequestAsync(requestBytes);



                if (response.Opcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ErrorPacketSerializer.ReadErrorResponsePacket(response);
                    return new OtpResult { Success = errSuccess, Message = errMsg };
                }

                return OtpPacketSerializer.ReadResult(response, Opcode.S2C_RegisterOTPResult);

            }
            catch (Exception ex)
            {
                return new OtpResult { Success = false, Message = DescribeError(ex) };
            }
        }

        //4.6. Gửi mã otp đẻ xác thực đăng kí
        public async Task<OtpResult> RequestRegisterOTPVerificationAsync(string email, string otpCode, OTP.OtpPurpose purpose)
        {
            try
            {
                byte[] requestBytes = OtpPacketSerializer.CreateVerificationRequest(
                    Opcode.C2S_RegisterOTPVerificationRequest, new RegisterOTPVerificationRequest
                    {
                        Email = email ?? string.Empty,
                        OTPCode = otpCode,
                        Purpose = purpose
                    });

                Packet response = await SendRequestAsync(requestBytes);


                if (response.Opcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ErrorPacketSerializer.ReadErrorResponsePacket(response);
                    return new OtpResult { Success = errSuccess, Message = errMsg };
                }

                return OtpPacketSerializer.ReadResult(response, Opcode.S2C_RegisterOTPVerificationResult);

            }
            catch (Exception ex)
            {
                return new OtpResult { Success = false, Message = DescribeError(ex) };
            }
        }


        public async Task<OtpResult> RequestResetPassOTPVerifyAsync(string email, string otpCode, OTP.OtpPurpose purpose)
        {
            try
            {
                byte[] requestBytes = OtpPacketSerializer.CreateVerificationRequest(
                    Opcode.C2S_ResetPassOTPVerificationRequest, new RegisterOTPVerificationRequest
                    {
                        Email = email ?? string.Empty,
                        OTPCode = otpCode,
                        Purpose = purpose
                    });

                Packet response = await SendRequestAsync(requestBytes);


                if (response.Opcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ErrorPacketSerializer.ReadErrorResponsePacket(response);
                    return new OtpResult { Success = errSuccess, Message = errMsg };
                }

                return OtpPacketSerializer.ReadResult(response, Opcode.S2C_ResetPassOTPVerificationResult);

            }
            catch (Exception ex)
            {
                return new OtpResult { Success = false, Message = DescribeError(ex) };
            }
        }


        private static string DescribeError(Exception ex)
        {
            System.Diagnostics.Trace.WriteLine(ex);
            return ex switch
            {
                TimeoutException => ex.Message,
                ProtocolException => $"Dữ liệu không hợp lệ hoặc phiên bản client/server không khớp: {ex.Message}",
                ArgumentException => $"Dữ liệu nhập không hợp lệ: {ex.Message}",
                IOException or SocketException => "Kết nối bị gián đoạn. Nếu vừa gửi yêu cầu thay đổi dữ liệu, hãy kiểm tra kết quả trước khi thử lại.",
                InvalidOperationException => "Chưa có kết nối tới máy chủ. Vui lòng kết nối lại.",
                _ => "Có lỗi xử lý phía ứng dụng. Vui lòng thử lại."
            };
        }

        public void Dispose()
        {
            Disconnect();
            _requestLock.Dispose();
        }
    }
}
