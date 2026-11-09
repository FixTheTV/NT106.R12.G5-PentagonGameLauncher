using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class RegisterRequest
    {
        public string Username { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        // Khong ghi password vao log
        public string Password { get; init; } = string.Empty;
    }
}
