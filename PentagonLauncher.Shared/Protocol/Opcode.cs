using System;
using System.Collections.Generic;
using System.Text;
// Danh sách mã Opcode đã thống nhất từ bữa trước + thêm cho phần Forgot và Reset cho trường hợp cần thay đôi mật khẩu.
// Không tự ý đổi mã Op trong lúc làm việc.
namespace PentagonLauncher.Shared.Protocol
{
    public enum Opcode : ushort
    {
        // Connection
        C2S_Pong = 1000,
        S2C_Ping = 2000,
        // Authentication
        C2S_LoginRequest = 1001,
        S2C_LoginResult = 2001,
        C2S_RegisterRequest = 1002,
        S2C_RegisterResult = 2002,
        C2S_ForgotPasswordRequest = 1003,
        S2C_ForgotPasswordResult = 2003,
        C2S_ResetPasswordRequest = 1004,
        S2C_ResetPasswordResult = 2004,
        // Lobby
        C2S_JoinLobbyRequest = 1101,
        S2C_JoinLobbyResponse = 2101,

        C2S_LeaveLobbyRequest = 1102,
        S2C_LobbyStateUpdate = 2102,

        C2S_SetReadyStateRequest = 1103,

        C2S_LobbyChat = 1104,
        S2C_LobbyChat = 2103,

        // Error
        S2C_ErrorResponse = 9000
    }
}
