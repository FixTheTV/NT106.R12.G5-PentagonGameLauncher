using System;
using System.Collections.Generic;
using System.Text;

namespace PentagonLauncher.Shared.Models.Authentication
{
    public class OTP
    {
        public OTP(string Code, OtpPurpose purpose)
        {
            this.Code = Code;
            this.Purpose = purpose;
        }
        public enum OtpPurpose : byte
        {
            Registration = 1,
            PasswordReset = 2
        }

        public string getPurpose()
        {
            switch (Purpose)
            {
                case OtpPurpose.Registration:
                    return "Registration";
                case OtpPurpose.PasswordReset:
                    return "PasswordReset";
                default:
                    return "unknown";
            }
        }

        public string Code;
        public OtpPurpose Purpose;
    }
}
