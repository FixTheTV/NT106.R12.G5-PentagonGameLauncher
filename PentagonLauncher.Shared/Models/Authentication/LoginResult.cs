using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class LoginResult
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
        public int UserId { get; init; }
        public string Username { get; init; } = string.Empty;
    }
}
