using System;
using System.Collections.Generic;
using System.Text;
// Lỗi gửi từ server về client, chỉ dùng cho S2C_ErrorResponse

namespace PentagonLauncher.Shared.Protocol
{
    public enum ProtocolErrorCode : ushort
    {
        UnknowOpcode = 1,
        InvalidPacketLength = 2,
        InvalidPayload = 3,
        InvalidClientState = 4,
        InternalServerError = 5
    }
}
