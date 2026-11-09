namespace PentagonLauncher.Launcher.Services;

public record AuthResult(bool Success, string Message, string Username = "");
public record OtpVerificationResult(bool Success, string Message, string ResetToken = "");
public record AccountProfile(string Username, string Email, DateTime CreatedAt);
public record RegistrationOtpResult(bool Success, string Message, DateTime ExpiresAtUtc, DateTime ResendAvailableAtUtc);
public record EmailVerificationResult(bool Success, string Message, string VerificationToken = "");

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
