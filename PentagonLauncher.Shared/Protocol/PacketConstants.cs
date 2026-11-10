using System;
using System.Collections.Generic;
using System.Text;
// Cố định các Maximum và Minimum của các payload.
namespace PentagonLauncher.Shared.Protocol
{
    public static class PacketConstants
    {
        public const int HeaderSize = 6;
        public const int MaximumPackerSize = 65_536;
        public const int MaximumUsernameByteLength = 80;
        public const int MaximunEmailByteLength = 1_024;
        public const int MaximumPasswordByteLength = 512;
        public const int MaximumOtpcodeByteLength = 24;
        public const int MaximumMessageByteLength = 2_048;
        public const int MinimumUsernameLength = 3;
        public const int MaximuUsernameLength = 20;
        public const int OtpcodeLength = 6;
        public const int MaximumChatByteLength = 500;
    }
}
