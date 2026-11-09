using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class ResetPasswordResult
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}
