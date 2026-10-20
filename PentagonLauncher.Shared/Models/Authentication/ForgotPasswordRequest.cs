using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class ForgotPasswordRequest
    {
        public string Email { get; init; } = string.Empty;
    }
}
