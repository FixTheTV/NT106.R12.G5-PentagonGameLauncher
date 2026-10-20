using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class LoginRequest
    {
        public string Username { get; init; } = string.Empty;
        //password khong ghi vao log
        public string Password { get; init; } = string.Empty;
    }
}
