using System.Security.Cryptography;

namespace PentagonLauncher.Launcher.Services;

/// <summary>In-memory implementation used only while the launcher UI is not connected to the backend.</summary>
public sealed class DemoAuthService : IAuthService
{
    private sealed record User(string Username, string Email, DateTime CreatedAt)
    {
        public string Password { get; set; } = "";
    }

    private readonly Dictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase)
    {
        ["demo"] = new User("demo", "demo@caro.local", DateTime.Today) { Password = "123456" }
    };
    private readonly Dictionary<string, DateTime> _registrationOtps = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, string> _registrationTokens = new();
    private readonly Dictionary<string, DateTime> _passwordOtps = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, string> _resetTokens = new();

    public Task<AuthResult> LoginAsync(string username, string password)
    {
        bool valid = _users.TryGetValue(username.Trim(), out User? user) && user.Password == password;
        return Task.FromResult(valid
            ? new AuthResult(true, "Đăng nhập thành công!", user!.Username)
            : new AuthResult(false, "Sai Username hoặc mật khẩu."));
    }

    public Task<RegistrationOtpResult> RequestRegistrationOtpAsync(string email)
    {
        email = email.Trim();
        if (!IsEmail(email)) return Task.FromResult(RegistrationError("Email không đúng định dạng."));
        if (_users.Values.Any(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            return Task.FromResult(RegistrationError("Email đã được đăng ký."));
        DateTime now = DateTime.UtcNow;
        _registrationOtps[email] = now.AddMinutes(5);
        return Task.FromResult(new RegistrationOtpResult(true, "Mã demo: 123456 (không gửi email thật).", now.AddMinutes(5), now.AddSeconds(60)));
    }

    public Task<RegistrationOtpResult> ResendRegistrationOtpAsync(string email) => RequestRegistrationOtpAsync(email);

    public Task<EmailVerificationResult> VerifyRegistrationOtpAsync(string email, string otp)
    {
        email = email.Trim();
        if (!_registrationOtps.Remove(email, out DateTime expiry) || DateTime.UtcNow >= expiry)
            return Task.FromResult(new EmailVerificationResult(false, "OTP đã hết hạn. Hãy gửi lại mã."));
        if (otp != "123456") return Task.FromResult(new EmailVerificationResult(false, "Mã OTP chưa đúng. Vui lòng thử lại."));
        string token = NewToken();
        _registrationTokens[token] = email;
        return Task.FromResult(new EmailVerificationResult(true, "Email đã xác minh! Bây giờ có thể tạo tài khoản.", token));
    }

    public Task<AuthResult> RegisterVerifiedAsync(string username, string email, string password, string verificationToken)
    {
        username = username.Trim(); email = email.Trim();
        if (username.Length is < 3 or > 20) return Task.FromResult(new AuthResult(false, "Username phải có 3–20 ký tự."));
        if (password.Length < 6) return Task.FromResult(new AuthResult(false, "Mật khẩu tối thiểu 6 ký tự."));
        if (!_registrationTokens.Remove(verificationToken, out string? verifiedEmail) || !string.Equals(verifiedEmail, email, StringComparison.OrdinalIgnoreCase))
            return Task.FromResult(new AuthResult(false, "Xác minh email đã hết hiệu lực. Vui lòng gửi OTP mới."));
        if (_users.ContainsKey(username)) return Task.FromResult(new AuthResult(false, "Username đã được sử dụng."));
        _users[username] = new User(username, email, DateTime.Now) { Password = password };
        return Task.FromResult(new AuthResult(true, "Đăng ký thành công!", username));
    }

    public Task<AuthResult> RequestPasswordResetAsync(string email)
    {
        email = email.Trim();
        if (_users.Values.Any(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase))) _passwordOtps[email] = DateTime.UtcNow.AddMinutes(5);
        return Task.FromResult(new AuthResult(true, "Nếu email hợp lệ, mã xác nhận đã được gửi."));
    }

    public Task<OtpVerificationResult> VerifyOtpAsync(string email, string otp)
    {
        email = email.Trim();
        if (!_passwordOtps.Remove(email, out DateTime expiry) || DateTime.UtcNow >= expiry)
            return Task.FromResult(new OtpVerificationResult(false, "OTP hết hạn hoặc không tồn tại. Vui lòng gửi lại mã."));
        if (otp != "123456") return Task.FromResult(new OtpVerificationResult(false, "Mã OTP không chính xác."));
        string token = NewToken(); _resetTokens[token] = email;
        return Task.FromResult(new OtpVerificationResult(true, "OTP hợp lệ.", token));
    }

    public Task<AuthResult> ResetPasswordAsync(string resetToken, string newPassword)
    {
        if (!_resetTokens.Remove(resetToken, out string? email)) return Task.FromResult(new AuthResult(false, "Phiên xác nhận đã hết hạn hoặc được sử dụng."));
        User? user = _users.Values.FirstOrDefault(item => item.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (user is null) return Task.FromResult(new AuthResult(false, "Không tìm thấy tài khoản."));
        user.Password = newPassword;
        return Task.FromResult(new AuthResult(true, "Mật khẩu đã được cập nhật. Bạn có thể đăng nhập."));
    }

    public Task<AccountProfile?> GetProfileAsync(string username) => Task.FromResult(
        _users.TryGetValue(username, out User? user) ? new AccountProfile(user.Username, user.Email, user.CreatedAt) : null);

    private static bool IsEmail(string email) => System.Net.Mail.MailAddress.TryCreate(email, out var address) && address.Address == email;
    private static string NewToken() => Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
    private static RegistrationOtpResult RegistrationError(string message) => new(false, message, default, default);
}
