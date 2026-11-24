using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class RegisterOTPVerificationRequest
    {
        // Dùng chung payload xác minh cho đăng ký và đặt lại mật khẩu.
        public OTP.OtpPurpose Purpose { get; init; } = OTP.OtpPurpose.Registration;
        public string Email { get; init; } = string.Empty;

        public string OTPCode { get; init; } = string.Empty;
    }
}
