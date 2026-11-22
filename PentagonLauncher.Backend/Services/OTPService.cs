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
            OTP otp = new OTP(System.Security.Cryptography.RandomNumberGenerator.GetInt32(100000, 1000000).ToString(), purpose);
            await _databaseService.SaveOTPAsync(email, otp);
            Console.WriteLine($"[DEMO OTP] {email} / {purpose}: {otp.Code} (5 phút)");
            return otp;
        }

        public async Task<OTP> CreatePasswordResetOTP(string email, OTP.OtpPurpose purpose)
        {
            OTP otp = new OTP(System.Security.Cryptography.RandomNumberGenerator.GetInt32(100000, 1000000).ToString(), purpose);
            await _databaseService.SaveOTPAsync(email, otp);
            Console.WriteLine($"[DEMO OTP] {email} / {purpose}: {otp.Code} (5 phút)");
            return otp;
        }
    }
}
