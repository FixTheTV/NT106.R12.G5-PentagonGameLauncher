using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PentagonLauncher.Shared.Protocol;

namespace PentagonLauncher.Launcher.Networking
{
    // Khuôn mẫu kết quả trả về cho UI của Thanh Hiền dễ sử dụng
    public record BasicResponse(bool Success, string Message);
    public record LoginResponse(bool Success, string Message, int UserId = 0, string Username = "");

    public class LauncherTcpClient : IDisposable
    {
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
                        var writer = new PacketWriter();
                        byte[] pongPacket = writer.Build(Opcode.C2S_Pong);
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
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            try
            {
                var writer = new PacketWriter();
                writer.WriteString(username ?? string.Empty);
                writer.WriteString(password ?? string.Empty);
                byte[] requestBytes = writer.Build(Opcode.C2S_LoginRequest);

                var (responseOpcode, payload) = await SendRequestAsync(requestBytes);
                var reader = new PacketReader(payload);

                if (responseOpcode == Opcode.S2C_ErrorResponse)
                {
                    bool errSuccess = reader.ReadBoolean();
                    string errMsg = reader.ReadString(MaxStringBytes);
                    reader.EnsureFullyRead();
                    return new LoginResponse(false, errMsg);
                }

                bool success = reader.ReadBoolean();
                string message = reader.ReadString(MaxStringBytes);
                int userId = 0;
                string loggedInUsername = string.Empty;

                if (success)
                {
                    userId = reader.ReadInt32();
                    loggedInUsername = reader.ReadString(MaxStringBytes);
                }
                reader.EnsureFullyRead();

                return new LoginResponse(success, message, userId, loggedInUsername);
            }
            catch (Exception ex)
            {
                return new LoginResponse(false, $"Lỗi kết nối: {ex.Message}");
            }
        }

        // 4.2. Đăng ký (Gửi 1002 -> Nhận 2002)
        public async Task<BasicResponse> RegisterAsync(string username, string email, string password)
        {
            try
            {
                var writer = new PacketWriter();
                writer.WriteString(username ?? string.Empty);
                writer.WriteString(email ?? string.Empty);
                writer.WriteString(password ?? string.Empty);
                byte[] requestBytes = writer.Build(Opcode.C2S_RegisterRequest);

                return await ProcessBasicResponseAsync(requestBytes);
            }
            catch (Exception ex)
            {
                return new BasicResponse(false, $"Lỗi kết nối: {ex.Message}");
            }
        }

        // 4.3. Quên mật khẩu / Yêu cầu mã OTP (Gửi 1003 -> Nhận 2003)
        public async Task<BasicResponse> RequestPasswordResetAsync(string email)
        {
            try
            {
                var writer = new PacketWriter();
                writer.WriteString(email ?? string.Empty);
                byte[] requestBytes = writer.Build(Opcode.C2S_ForgotPasswordRequest);

                return await ProcessBasicResponseAsync(requestBytes);
            }
            catch (Exception ex)
            {
                return new BasicResponse(false, $"Lỗi kết nối: {ex.Message}");
            }
        }

        // 4.4. Đặt lại mật khẩu bằng OTP (Gửi 1004 -> Nhận 2004)
        public async Task<BasicResponse> ResetPasswordAsync(string email, string otp, string newPassword)
        {
            try
            {
                var writer = new PacketWriter();
                writer.WriteString(email ?? string.Empty);
                writer.WriteString(otp ?? string.Empty);
                writer.WriteString(newPassword ?? string.Empty);
                byte[] requestBytes = writer.Build(Opcode.C2S_ResetPasswordRequest);

                return await ProcessBasicResponseAsync(requestBytes);
            }
            catch (Exception ex)
            {
                return new BasicResponse(false, $"Lỗi kết nối: {ex.Message}");
            }
        }

        // Hàm dùng chung để bóc tách gói phản hồi cơ bản (Bool + String)
        private async Task<BasicResponse> ProcessBasicResponseAsync(byte[] requestBytes)
        {
            var (_, payload) = await SendRequestAsync(requestBytes);
            var reader = new PacketReader(payload);

            bool success = reader.ReadBoolean();
            string message = reader.ReadString(MaxStringBytes);
            reader.EnsureFullyRead();

            return new BasicResponse(success, message);
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

        public void Dispose()
        {
            Disconnect();
            _requestLock.Dispose();
        }
    }
}
