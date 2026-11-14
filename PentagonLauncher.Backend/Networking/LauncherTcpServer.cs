using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PentagonLauncher.Shared.Protocol;
using PentagonLauncher.Shared.Protocol.Serializers;
using PentagonLauncher.Shared.Models.Authentication;
using PentagonLauncher.Backend.Services;

namespace PentagonLauncher.Backend.Networking
{
    public class LauncherTcpServer
    {
        private  string justSendOtp = string.Empty;
        private readonly SessionManager _sessionManager;
        private readonly AuthService _authService;

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
                    byte[] errorPacket = ErrorPacketSerializer.CreateErrorResponsePacket(false, pEx.Message);
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
            Packet packet = new(new PacketHeader(payload.Length + PacketConstants.HeaderSize, opcode), payload);

            switch (opcode)
            {
                case Opcode.C2S_Pong: // 1000
                {
                    ConnectionPacketSerializer.ReadPongPacket(packet);
                    break;
                }

                case Opcode.C2S_LoginRequest: // 1001
                {
                    LoginRequest request = AuthPacketSerializer.ReadLoginRequest(packet);
                    var result = await _authService.LoginAsync(request);

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
                        Console.WriteLine($"[Auth] Đăng nhập thất bại ({request.Username}): {result.Message}");
                    }

                    // Gửi gói 2001 (S2C_LoginResult) về cho Thanh Hiền
                    byte[] responsePacket = AuthPacketSerializer.CreateLoginResultPacket(result);
                    await peer.SendPacketAsync(responsePacket);
                    break;
                }

                case Opcode.C2S_RegisterRequest: // 1002
                {
                    RegisterRequest request = AuthPacketSerializer.ReadRegisterRequest(packet);
                    var result = await _authService.RegisterAsync(request);

                    // Gửi gói 2002 (S2C_RegisterResult)
                    byte[] responsePacket = AuthPacketSerializer.CreateRegisterResultPacker(result);
                    await peer.SendPacketAsync(responsePacket);
                    break;
                }

                case Opcode.C2S_ForgotPasswordRequest: // 1003
                {
                    ForgotPasswordRequest request = AuthPacketSerializer.ReadForgotPasswordRequest(packet);
                    var result = await _authService.RequestPasswordResetAsync(request);

                    // Gửi gói 2003 (S2C_ForgotPasswordResult)
                    byte[] responsePacket = AuthPacketSerializer.CreateForgotPasswordResultPacket(result);
                    await peer.SendPacketAsync(responsePacket);
                    break;
                }

                case Opcode.C2S_ResetPasswordRequest: // 1004
                {
                    ResetPasswordRequest request = AuthPacketSerializer.ReadResetPasswordRequest(packet);
                    var result = await _authService.ResetPasswordAsync(request);

                    // Gửi gói 2004 (S2C_ResetPasswordResult)
                    byte[] responsePacket = AuthPacketSerializer.CreateResetPasswordResultPacket(result);
                    await peer.SendPacketAsync(responsePacket);
                    break;
                }

                case Opcode.C2S_RegisterOTPRequest: //1005
                {
                        PacketReader reader = new(packet.Payload);
                        string email = reader.ReadString(PacketConstants.MaximunEmailByteLength);
                        reader.EnsureFullyRead();

                        string otpCode = Random.Shared.Next(100000, 1000000).ToString();
                        justSendOtp = otpCode;
                        var writer = new PacketWriter();
                        writer.WriteBoolean(true);
                        writer.WriteString(otpCode ?? string.Empty);
                        
                        byte[] responsepacket = writer.Build(Opcode.S2C_RegisterOTPResult);
                        await peer.SendPacketAsync(responsepacket);
                        break;
                }

                case Opcode.C2S_RegisterOTPVerificationRequest: //1006
                    {
                        PacketReader reader = new(packet.Payload);
                        string email = reader.ReadString(PacketConstants.MaximunEmailByteLength);
                        string otpCode = reader.ReadString(PacketConstants.MaximumOtpcodeByteLength);
                        reader.EnsureFullyRead();

                        var writer = new PacketWriter();
                        if (justSendOtp == otpCode)
                        {
                            writer.WriteBoolean(true);
                            writer.WriteString("ngon luon");
                        }
                        else
                        {
                            writer.WriteBoolean(false);
                            writer.WriteString("otp sai");
                        }
                        

                        byte[] responsepacket = writer.Build(Opcode.S2C_RegisterOTPVerificationResult);
                        await peer.SendPacketAsync(responsepacket);
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

    }
}
