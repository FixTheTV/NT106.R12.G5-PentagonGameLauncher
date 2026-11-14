using PentagonLauncher.Shared.Models.Authentication;
using PentagonLauncher.Shared.Protocol;
using PentagonLauncher.Shared.Protocol.Serializers;
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

    public record DemoOTPRequestResponse(bool Success, string Message, DateTime ExpiresAtUtc);

    public class LauncherTcpClient : IDisposable
    {
        public static LauncherTcpClient Shared { get; } = new LauncherTcpClient();
        private TcpClient? _tcpClient;
        private NetworkStream? _stream;
        private CancellationTokenSource? _cts;

        // Hàng đợi đồng bộ cho cơ chế Request - Response
        private TaskCompletionSource<(Opcode Opcode, byte[] Payload)>? _pendingRequest;
        private readonly SemaphoreSlim _requestLock = new(1, 1);

        private const int MaxStringBytes = 256;

        public bool IsConnected => _tcpClient?.Connected ?? false;

        // Sự kiện bắn ra cho UI biết khi bị mất kết nối đột ngột với Server
        public event Action<string>? OnDisconnected;

        // 1. KẾT NỐI & NGẮT KẾT NỐI TCP
        public async Task<bool> ConnectAsync(string host = "127.0.0.1", int port = 12345)
        {
            if (IsConnected) return true;

            try
            {
                _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(host, port);
                _stream = _tcpClient.GetStream();
                _cts = new CancellationTokenSource();

                // Khởi chạy vòng lặp nhận dữ liệu chạy ngầm bên dưới Client
                _ = Task.Run(() => ReceiveLoopAsync(_cts.Token));
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
            try
            {
                _tcpClient?.Close();
            }
            catch { /* Bỏ qua lỗi khi đóng socket */ }
        }

        // 2. VÒNG LẶP NHẬN DỮ LIỆU TỪ SERVER (Chạy ngầm)
        private async Task ReceiveLoopAsync(CancellationToken token)
        {
            byte[] headerBuffer = new byte[PacketConstants.HeaderSize]; // 6 bytes cố định

            try
            {
                while (!token.IsCancellationRequested && _stream != null && IsConnected)
                {
                    // Đọc đủ 6 bytes Header
                    int headerRead = await ReadExactAsync(_stream, headerBuffer, PacketConstants.HeaderSize, token);
                    if (headerRead == 0) break; // Server đóng kết nối

                    PacketHeader header = PacketHeader.Parse(headerBuffer);
                    int payloadLength = header.PayloadLength;

                    // Đọc đủ phần Payload
                    byte[] payload = new byte[payloadLength];
                    if (payloadLength > 0)
                    {
                        int payloadRead = await ReadExactAsync(_stream, payload, payloadLength, token);
                        if (payloadRead == 0) break;
                    }

                    // Nếu Server gửi gói Ping (2000), tự động trả lời Pong (1000)
                    if (header.Opcode == Opcode.S2C_Ping)
                    {
                        byte[] pongPacket = ConnectionPacketSerializer.CreatePongPacket();
                        await _stream.WriteAsync(pongPacket, 0, pongPacket.Length, token);
                        continue;
                    }

                    // Đánh thức hàm Request đang chờ kết quả từ Server
                    _pendingRequest?.TrySetResult((header.Opcode, payload));
                }
            }
            catch (Exception ex)
            {
                _pendingRequest?.TrySetException(ex);
            }
            finally
            {
                Disconnect();
                OnDisconnected?.Invoke("Đã mất kết nối với máy chủ Pentagon Launcher.");
            }
        }

        // 3. CƠ CHẾ REQUEST - RESPONSE CỐT LÕI
        private async Task<(Opcode Opcode, byte[] Payload)> SendRequestAsync(byte[] packetBytes, int timeoutMs = 10000)
        {
            if (!IsConnected || _stream == null)
                throw new InvalidOperationException("Chưa kết nối tới máy chủ.");

            await _requestLock.WaitAsync();
            try
            {
                _pendingRequest = new TaskCompletionSource<(Opcode, byte[])>(TaskCreationOptions.RunContinuationsAsynchronously);

                // Gửi gói tin lên Server
                await _stream.WriteAsync(packetBytes, 0, packetBytes.Length);
                await _stream.FlushAsync();

                // Chờ Server phản hồi (tối đa 10 giây)
                using var timeoutCts = new CancellationTokenSource(timeoutMs);
                await using var reg = timeoutCts.Token.Register(() =>
                    _pendingRequest.TrySetException(new TimeoutException("Máy chủ không phản hồi (Timeout).")));

                return await _pendingRequest.Task;
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

                var (responseOpcode, payload) = await SendRequestAsync(requestBytes);
                if (responseOpcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ReadErrorResponse(responseOpcode, payload);
                    return new LoginResult { Success = errSuccess, Message = errMsg};
                }
                return AuthPacketSerializer.ReadLoginResult(CreatePacket(responseOpcode, payload));
            }
            catch (Exception ex)
            {
                return new LoginResult { Success = false, Message = $"Lỗi kết nối: {ex.Message}"};
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

                var (responseOpcode, payload) = await SendRequestAsync(requestBytes);
                if (responseOpcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ReadErrorResponse(responseOpcode, payload);
                    return new RegisterResult { Success = errSuccess, Message = errMsg };
                }
                return AuthPacketSerializer.ReadRequestResult(CreatePacket(responseOpcode, payload));
            }
            catch (Exception ex)
            {
                return new RegisterResult { Success = false, Message = $"Lỗi kết nối: {ex.Message}" };
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

                var (responseOpcode, payload) = await SendRequestAsync(requestBytes);
                if (responseOpcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ReadErrorResponse(responseOpcode, payload);
                    return new ForgotPasswordResult { Success = errSuccess, Message = errMsg };
                }
                return AuthPacketSerializer.ReadForgotPasswordResult(CreatePacket(responseOpcode, payload));
            }
            catch (Exception ex)
            {
                return new ForgotPasswordResult { Success = false, Message = $"Lỗi kết nối: {ex.Message}" };
            }
        }


        // 4.4. Đặt lại mật khẩu bằng OTP (Gửi 1004 -> Nhận 2004)
        public async Task<ResetPasswordResult> ResetPasswordAsync(string email, string otp, string newPassword)
        {
            try
            {
                byte[] requestBytes = AuthPacketSerializer.CreateResetPasswordPacket(new ResetPasswordRequest
                {
                    Email = email ?? string.Empty,
                    OtpCode = otp ?? string.Empty,
                    NewPassword = newPassword ?? string.Empty
                });

                var (responseOpcode, payload) = await SendRequestAsync(requestBytes);
                if (responseOpcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ReadErrorResponse(responseOpcode, payload);
                    return new ResetPasswordResult { Success = errSuccess, Message = errMsg };
                }
                return AuthPacketSerializer.ReadResetPasswordResult(CreatePacket(responseOpcode, payload));

            }
            catch (Exception ex)
            {
                return new ResetPasswordResult { Success = false, Message = $"Lỗi kết nối: {ex.Message}" };
            }
        }

        // 4.5. Gửi yêu cầu lấy mã OTP xác thực Email Lúc đăng ký (gui 1005, nhan 2005)
        public async Task<RegisterOTPResult> RequestRegisterOTPAsync(string email)
        {
            try
            {
                var writer = new PacketWriter();
                writer.WriteString(email ?? string.Empty);
                byte[] requestBytes = writer.Build(Opcode.C2S_RegisterOTPRequest);

                var (responseOpcode, payload) = await SendRequestAsync(requestBytes);
                var reader = new PacketReader(payload);

                if (responseOpcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, _) = ReadErrorResponse(responseOpcode, payload);
                    return new RegisterOTPResult { Success = errSuccess};
                }

                bool success = reader.ReadBoolean();
                string otpCode = reader.ReadString(MaxStringBytes);
                reader.EnsureFullyRead();

                return new RegisterOTPResult{ Success = success, OTPCode = otpCode};

            }
            catch
            {
                return new RegisterOTPResult { Success = false};
            }
        }

        //4.6. Gửi mã otp đẻ xác thực đăng kí
        public async Task<RegisterOTPVerificationResult> RequestRegisterOTPVerificationAsync(string email, string otpCode)
        {
            try
            {
                var writer = new PacketWriter();
                writer.WriteString(email ?? string.Empty);
                writer.WriteString(otpCode);
                byte[] requestBytes = writer.Build(Opcode.C2S_RegisterOTPVerificationRequest);

                var (responseOpcode, payload) = await SendRequestAsync(requestBytes);
                var reader = new PacketReader(payload);

                if (responseOpcode == Opcode.S2C_ErrorResponse)
                {
                    var (errSuccess, errMsg) = ReadErrorResponse(responseOpcode, payload);
                    return new RegisterOTPVerificationResult { Success = errSuccess, Message = errMsg };
                }

                bool success = reader.ReadBoolean();
                string message = reader.ReadString(MaxStringBytes);
                reader.EnsureFullyRead();

                return new RegisterOTPVerificationResult { Success = success, Message = message};

            }
            catch
            {
                return new RegisterOTPVerificationResult { Success = false, Message = "Đã xảy ra lỗi." };
            }
        }


        // Đảm bảo đọc đủ số byte từ TCP Stream
        private static async Task<int> ReadExactAsync(NetworkStream stream, byte[] buffer, int count, CancellationToken token)
        {
            int totalRead = 0;
            while (totalRead < count)
            {
                int read = await stream.ReadAsync(buffer.AsMemory(totalRead, count - totalRead), token);
                if (read == 0) return 0;
                totalRead += read;
            }
            return totalRead;
        }

        private static Packet CreatePacket(Opcode opcode, byte[] payload) =>
            new(new PacketHeader(payload.Length + PacketConstants.HeaderSize, opcode), payload);

        private static (bool Success, string Message) ReadErrorResponse(Opcode opcode, byte[] payload) =>
            ErrorPacketSerializer.ReadErrorResponsePacket(CreatePacket(opcode, payload));

        public void Dispose()
        {
            Disconnect();
            _requestLock.Dispose();
        }
    }
}
