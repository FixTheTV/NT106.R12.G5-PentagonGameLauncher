using PentagonLauncher.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class ErrorResponse
    {
        public ProtocolErrorCode ErrorCode { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}
