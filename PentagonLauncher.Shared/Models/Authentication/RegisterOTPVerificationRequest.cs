using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class RegisterOTPVerificationRequest
    {
        public string Email { get; init; } = string.Empty;

        public string OTPCode { get; init; } = string.Empty;
    }
}
