using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class RegisterResult
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}
