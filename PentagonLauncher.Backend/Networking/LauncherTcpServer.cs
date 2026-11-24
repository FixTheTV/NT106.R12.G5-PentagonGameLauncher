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
        private readonly SessionManager _sessionManager;
        private readonly AuthService _authService;

        private readonly OTPService _otpService;
        public LauncherTcpServer(SessionManager sessionManager, AuthService authService, OTPService otpService)
        {
            _sessionManager = sessionManager;
            _authService = authService;
            _otpService = otpService;
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

            try
            {
                while (!token.IsCancellationRequested && peer.TcpClient.Connected)
                {
                    Packet? packet = await PacketStreamReader.ReadPacketOrNullAsync(stream, token);
                    if (packet is null) break;
                    await HandlePacketAsync(peer, packet);
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
                try
                {
                    await peer.SendPacketAsync(ErrorPacketSerializer.CreateErrorResponsePacket(false,
                        "Máy chủ gặp lỗi xử lý. Nếu vừa thay đổi tài khoản, hãy kiểm tra kết quả trước khi thử lại."));
                }
                catch (Exception sendError) { Console.WriteLine($"[Error response] {sendError.GetType().Name}"); }
            }
            finally
            {
                // Xây dựng xử lý mất kết nối người dùng & Ngắt kết nối
                Console.WriteLine($"[Disconnected] Client {peer.ConnectionId} (User: {peer.Username ?? "Unauthenticated"}) đã ngắt kết nối.");
                _sessionManager.RemoveDisconnectedPeer(peer);
            }
        }

        // Serializer đọc/ghi payload; handler điều phối service và phiên kết nối.
        private async Task HandlePacketAsync(ClientPeer peer, Packet packet)
        {
            switch (packet.Opcode)
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
                    if (!peer.HasOtpPermission(1, request.Email) || !await _authService.IsVerifiedCodeAsync(request.Email, 1, peer.OtpPermissions[1].Code))
                    {
                        await peer.SendPacketAsync(AuthPacketSerializer.CreateRegisterResultPacker(new RegisterResult { Success = false, Message = "Hãy xác minh OTP đăng ký trên kết nối này." }));
                        break;
                    }
                    var result = await _authService.RegisterAsync(request);
                    if (result.Success) peer.OtpPermissions.Remove(1);

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
                    if (!peer.HasOtpPermission(2, request.Email) || !await _authService.IsVerifiedCodeAsync(request.Email, 2, peer.OtpPermissions[2].Code))
                    {
                        await peer.SendPacketAsync(AuthPacketSerializer.CreateResetPasswordResultPacket(new ResetPasswordResult { Success = false, Message = "Hãy xác minh OTP reset trên kết nối này." }));
                        break;
                    }
                    var result = await _authService.ResetPasswordAsync(request);
                    if (result.Success) peer.OtpPermissions.Remove(2);

                    // Gửi gói 2004 (S2C_ResetPasswordResult)
                    byte[] responsePacket = AuthPacketSerializer.CreateResetPasswordResultPacket(result);
                    await peer.SendPacketAsync(responsePacket);
                    break;
                }

                case Opcode.C2S_RegisterOTPRequest: //1005
                {
                        RegisterOTPRequest request = OtpPacketSerializer.ReadRegisterRequest(packet);
                        await _otpService.CreateRegistrationOTP(request.Email, OTP.OtpPurpose.Registration);
                        await peer.SendPacketAsync(OtpPacketSerializer.CreateResult(
                            Opcode.S2C_RegisterOTPResult,
                            new OtpResult { Success = true, Message = "Đã tạo OTP. Kiểm tra console backend (demo)." }));
                        break;
                }

                case Opcode.C2S_RegisterOTPVerificationRequest: //1006
                    {
                        RegisterOTPVerificationRequest request = OtpPacketSerializer.ReadVerificationRequest(
                            packet, Opcode.C2S_RegisterOTPVerificationRequest);
                        var res = await _authService.VerifyRegisterOTP(request.Email, request.OTPCode, request.Purpose);
                        if (res.Success)
                            peer.OtpPermissions[1] = (request.Email.Trim(), DateTime.UtcNow.AddMinutes(5), request.OTPCode);

                        await peer.SendPacketAsync(OtpPacketSerializer.CreateResult(
                            Opcode.S2C_RegisterOTPVerificationResult,
                            new OtpResult
                            {
                                Success = res.Success,
                                Message = res.Success ? "Đã xác thực OTP thành công" : "Đã xác thực OTP thất bại"
                            }));
                        break;
                    }
                case Opcode.C2S_ResetPassOTPVerificationRequest: //1007
                    {
                        RegisterOTPVerificationRequest request = OtpPacketSerializer.ReadVerificationRequest(
                            packet, Opcode.C2S_ResetPassOTPVerificationRequest);
                        var res = await _authService.VerifyResetPassOTP(request.Email, request.OTPCode, request.Purpose);
                        if (res.Success)
                            peer.OtpPermissions[2] = (request.Email.Trim(), DateTime.UtcNow.AddMinutes(5), request.OTPCode);

                        await peer.SendPacketAsync(OtpPacketSerializer.CreateResult(
                            Opcode.S2C_ResetPassOTPVerificationResult,
                            new OtpResult
                            {
                                Success = res.Success,
                                Message = res.Success ? "Đã xác thực OTP thành công" : "Đã xác thực OTP thất bại"
                            }));
                        break;
                    }

                default:
                    Console.WriteLine($"[Warning] Opcode chưa được hỗ trợ ở Sprint 1: {packet.Opcode}");
                    break;
            }
        }

    }
}
