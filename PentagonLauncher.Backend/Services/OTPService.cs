using System;
using System.Collections.Generic;
using System.Text;
using PentagonLauncher.Shared.Models.Authentication;

namespace PentagonLauncher.Backend.Services
{
    public class OTPService
    {
        private readonly DatabaseService _databaseService;
        public OTPService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public async Task<OTP> CreateRegistrationOTP(string email, OTP.OtpPurpose purpose)
        {
            OTP otp = new OTP(Random.Shared.Next(100000, 1000000).ToString(), purpose);
            await _databaseService.SaveOTPAsync(email, otp);
            return otp;
        }

        public async Task<OTP> CreatePasswordResetOTP()
        {
            return new OTP(Random.Shared.Next(100000, 1000000).ToString(), OTP.OtpPurpose.PasswordReset);
        }
    }
}
