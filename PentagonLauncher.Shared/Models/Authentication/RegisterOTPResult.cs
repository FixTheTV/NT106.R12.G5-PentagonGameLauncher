using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public sealed class RegisterOTPResult()
    {
        public bool Success { get; init; }
        public string OTPCode { get; init; } = string.Empty;


    }
}
