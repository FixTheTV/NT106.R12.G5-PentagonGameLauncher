using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class RegisterOTPRequest
    {
        public OTP.OtpPurpose Purpose { get; init; } = OTP.OtpPurpose.Registration;
        public string Email { get; init; } = string.Empty;
    }
}
