using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class ResetPasswordRequest
    {
        public string Email { get; init; } = string.Empty;

        // OTP dac ta gom 6 so
        public string OtpCode { get; init; } = string.Empty;

        public string NewPassword { get; init; } = string.Empty;
    }
}
