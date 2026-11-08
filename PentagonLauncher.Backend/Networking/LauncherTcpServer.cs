using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PentagonLauncher.Shared.Protocol;

namespace PentagonLauncher.Backend.Networking
{
    public class LauncherTcpServer
    {
        private readonly int _port = 12345;
        private TcpListener? _listener;
        private CancellationTokenSource? _cts;

        private readonly SessionManager _sessionManager;
        private readonly AuthService _authService;

        // Giới hạn độ dài byte tối đa khi đọc chuỗi qua PacketReader
        private const int MaxStringBytes = 256;

        public LauncherTcpServer(SessionManager sessionManager, AuthService authService)
        {
            _sessionManager = sessionManager;
            _authService = authService;
        }

        // Hàm bọc trực tiếp TcpClient thành ClientPeer và chạy vòng lặp nhận dữ liệu (khớp với Program.cs của Long)
        public async Task HandleClientAsync(TcpClient tcpClient, CancellationToken token = default)
        {
            var peer = new ClientPeer(tcpClient);
            _sessionManager.AddPeer(peer);
            await ReceiveLoopAsync(peer, token);
        }

        // Xây dựng vòng lặp nhận dữ liệu & Xử lý lỗi kết nối (Public để gọi trực tiếp từ Program.cs)
        public async Task ReceiveLoopAsync(ClientPeer peer, CancellationToken token = default)
        {
            NetworkStream stream = peer.Stream;
            byte[] headerBuffer = new byte[PacketConstants.HeaderSize]; // 6 bytes cố định

            try
            {
                while (!token.IsCancellationRequested && peer.TcpClient.Connected)
                {
                    // Đọc đủ 6 bytes Header
                    int headerRead = await ReadExactAsync(stream, headerBuffer, PacketConstants.HeaderSize, token);
                    if (headerRead == 0) break; // Client chủ động đóng kết nối

                    PacketHeader header = PacketHeader.Parse(headerBuffer);
                    int payloadLength = header.PayloadLength;

                    // Đọc đủ phần Payload
                    byte[] payload = new byte[payloadLength];
                    if (payloadLength > 0)
                    {
                        int payloadRead = await ReadExactAsync(stream, payload, payloadLength, token);
                        if (payloadRead == 0) break;
                    }

                    // Xử lý gói tin
                    await HandlePacketAsync(peer, header.Opcode, payload);
                }
            }
            catch (ProtocolException pEx)
            {
                Console.WriteLine($"[Protocol Error] Gói tin không hợp lệ từ {peer.ConnectionId}: {pEx.Message}");
                // Gửi gói lỗi 9000 (S2C_ErrorResponse) về cho Client trước khi ngắt kết nối
                try
                {
                    byte[] errorPacket = BuildBasicResponsePacket(Opcode.S2C_ErrorResponse, false, pEx.Message);
                    await peer.SendPacketAsync(errorPacket);
                }
                catch { /* Bỏ qua nếu socket đã hỏng */ }
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"[Network Error] Đứt kết nối với Client {peer.ConnectionId}: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Server Error] Lỗi xử lý tại Client {peer.ConnectionId}: {ex.Message}");
            }
            finally
            {
                // Xây dựng xử lý mất kết nối người dùng & Ngắt kết nối
                Console.WriteLine($"[Disconnected] Client {peer.ConnectionId} (User: {peer.Username ?? "Unauthenticated"}) đã ngắt kết nối.");
                _sessionManager.RemoveDisconnectedPeer(peer);
            }
        }

        // Xây dựng cơ chế Request-Response (Gọi PacketReader, AuthService, PacketWriter)
        private async Task HandlePacketAsync(ClientPeer peer, Opcode opcode, byte[] payload)
        {
            var reader = new PacketReader(payload);

            switch (opcode)
            {
                case Opcode.C2S_Pong: // 1000
                {
                    reader.EnsureFullyRead();
                    break;
                }

                case Opcode.C2S_LoginRequest: // 1001
                {
                    string username = reader.ReadString(MaxStringBytes);
                    string password = reader.ReadString(MaxStringBytes);
                    reader.EnsureFullyRead();

                    var result = await _authService.LoginAsync(username, password);

                    if (result.Success)
                    {
                        // Lưu thông tin vào đối tượng phiên & đổi trạng thái sang Authenticated
                        _sessionManager.AuthenticatePeer(peer, result.UserId, result.Username);
                        Console.WriteLine($"[Auth] Đăng nhập thành công: {result.Username} (ID: {result.UserId})");
                    }
                    else
                    {
                        // Đăng nhập thất bại: Giữ nguyên State = Connected
                        peer.State = ClientState.Connected;
                        Console.WriteLine($"[Auth] Đăng nhập thất bại ({username}): {result.Message}");
                    }

                    // Gửi gói 2001 (S2C_LoginResult) về cho Thanh Hiền
                    byte[] responsePacket = BuildLoginResultPacket(result.Success, result.Message, result.UserId, result.Username);
                    await peer.SendPacketAsync(responsePacket);
                    break;
                }

                case Opcode.C2S_RegisterRequest: // 1002
                {
                    string username = reader.ReadString(MaxStringBytes);
                    string email = reader.ReadString(MaxStringBytes);
                    string password = reader.ReadString(MaxStringBytes);
                    reader.EnsureFullyRead();

                    var result = await _authService.RegisterAsync(username, email, password);

                    // Gửi gói 2002 (S2C_RegisterResult)
                    byte[] responsePacket = BuildBasicResponsePacket(Opcode.S2C_RegisterResult, result.Success, result.Message);
                    await peer.SendPacketAsync(responsePacket);
                    break;
                }

                case Opcode.C2S_ForgotPasswordRequest: // 1003
                {
                    string email = reader.ReadString(MaxStringBytes);
                    reader.EnsureFullyRead();

                    var result = await _authService.RequestPasswordResetAsync(email);

                    // Gửi gói 2003 (S2C_ForgotPasswordResult)
                    byte[] responsePacket = BuildBasicResponsePacket(Opcode.S2C_ForgotPasswordResult, result.Success, result.Message);
                    await peer.SendPacketAsync(responsePacket);
                    break;
                }

                case Opcode.C2S_ResetPasswordRequest: // 1004
                {
                    string email = reader.ReadString(MaxStringBytes);
                    string otp = reader.ReadString(MaxStringBytes);
                    string newPassword = reader.ReadString(MaxStringBytes);
                    reader.EnsureFullyRead();

                    var result = await _authService.ResetPasswordAsync(email, otp, newPassword);

                    // Gửi gói 2004 (S2C_ResetPasswordResult)
                    byte[] responsePacket = BuildBasicResponsePacket(Opcode.S2C_ResetPasswordResult, result.Success, result.Message);
                    await peer.SendPacketAsync(responsePacket);
                    break;
                }

                default:
                    Console.WriteLine($"[Warning] Opcode chưa được hỗ trợ ở Sprint 1: {opcode}");
                    break;
            }
        }

        // Đảm bảo đọc đủ số byte từ luồng TCP
        private static async Task<int> ReadExactAsync(NetworkStream stream, byte[] buffer, int count, CancellationToken token)
        {
            int totalRead = 0;
            while (totalRead < count)
            {
                int read = await stream.ReadAsync(buffer.AsMemory(totalRead, count - totalRead), token);
                if (read == 0) return 0; // Đã đóng kết nối
                totalRead += read;
            }
            return totalRead;
        }

        // Đóng gói gói tin 2001 (S2C_LoginResult) bằng PacketWriter của Minh Hiếu
        private static byte[] BuildLoginResultPacket(bool success, string message, int userId, string username)
        {
            var writer = new PacketWriter();
            writer.WriteBoolean(success);
            writer.WriteString(message ?? string.Empty);

            if (success)
            {
                writer.WriteInt32(userId);
                writer.WriteString(username ?? string.Empty);
            }

            return writer.Build(Opcode.S2C_LoginResult);
        }

        // Đóng gói các gói phản hồi chuẩn (Register, Forgot, Reset, Error) bằng PacketWriter của Minh Hiếu
        private static byte[] BuildBasicResponsePacket(Opcode opcode, bool success, string message)
        {
            var writer = new PacketWriter();
            writer.WriteBoolean(success);
            writer.WriteString(message ?? string.Empty);
            return writer.Build(opcode);
        }
    }
}
