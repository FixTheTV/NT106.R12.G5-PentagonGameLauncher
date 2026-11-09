using System;
using System.Threading.Tasks;

namespace CaroOnline.WinForms.Services;

public record AuthResult(bool Success, string Message, string Username = "");
public record OtpVerificationResult(bool Success, string Message, string ResetToken = "");
public record AccountProfile(string Username, string Email, DateTime CreatedAt);

/// <summary>UTC thời hạn OTP trả về từ Backend (không tự đặt hạn ở UI).</summary>
public record RegistrationOtpResult(bool Success, string Message,
    DateTime ExpiresAtUtc, DateTime ResendAvailableAtUtc);

/// <summary>Token xác thực email dùng một lần, phải gắn với đúng email và có hạn trên server.</summary>
public record EmailVerificationResult(bool Success, string Message, string VerificationToken = "");

/// <summary>
/// Contract Backend ↔ WinForms. Khi triển khai thật, thay DemoAuthService bằng một API service.
/// Đăng ký mới: xác minh email TRƯỚC, sau đó người dùng bấm Đăng ký để tạo tài khoản.
/// </summary>
public interface IAuthService
{
    Task<AuthResult> LoginAsync(string username, string password);
    Task<RegistrationOtpResult> RequestRegistrationOtpAsync(string email);
    Task<RegistrationOtpResult> ResendRegistrationOtpAsync(string email);
    Task<EmailVerificationResult> VerifyRegistrationOtpAsync(string email, string otp);
    Task<AuthResult> RegisterVerifiedAsync(string username, string email, string password, string verificationToken);
    Task<AuthResult> RequestPasswordResetAsync(string email);
    Task<OtpVerificationResult> VerifyOtpAsync(string email, string otp);
    Task<AuthResult> ResetPasswordAsync(string resetToken, string newPassword);
    Task<AccountProfile?> GetProfileAsync(string username);
}
