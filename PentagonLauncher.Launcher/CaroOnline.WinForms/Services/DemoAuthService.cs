using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CaroOnline.WinForms.Services;

/// <summary>
/// DỮ LIỆU GIẢ LẬP CHỈ ĐỂ DUYỆT GIAO DIỆN, KHÔNG DÙNG TRONG SẢN PHẨM THẬT.
/// Mã 123456 chỉ phục vụ demo (không gửi email); khi có backend thực,
/// OTP phải được tạo ngẫu nhiên, giới hạn lượt thử, lưu dạng hash và gửi email ở server.
/// </summary>
public sealed class DemoAuthService : IAuthService
{
    private sealed class User
    {
        public required string Username { get; init; }
        public required string Email { get; init; }
        public required string Password { get; set; }
        public required DateTime CreatedAt { get; init; }
    }

    private sealed class PendingRegistration
    {
        public required string Email { get; init; }
        public required DateTime ExpiresAtUtc { get; set; }
        public required DateTime ResendAvailableAtUtc { get; set; }
        public int FailedAttempts { get; set; }
    }

    private sealed class VerifiedEmail
    {
        public required string Email { get; init; }
        public required DateTime ExpiresAtUtc { get; init; }
    }

    private sealed class ResetRequest
    {
        public required string Email { get; init; }
        public required DateTime ExpiresAt { get; init; }
    }

    private readonly Dictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);
    // Demo OTP cố định 123456 để thử form mà không cần nhà cung cấp email.
    // Tuyệt đối không dùng mã cố định hoặc lưu mật khẩu thuần trong backend thật.
    private readonly Dictionary<string, PendingRegistration> _pendingRegistrations =
        new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, VerifiedEmail> _verifiedEmailTokens = new();
    private readonly Dictionary<string, ResetRequest> _otpRequests = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, string> _validResetTokens = new();
    private readonly object _sync = new();

    public DemoAuthService()
    {
        _users["demo"] = new User
        {
            Username = "demo",
            Email = "demo@caro.local",
            Password = "123456",
            CreatedAt = DateTime.Today
        };
    }

    public Task<AuthResult> LoginAsync(string username, string password)
    {
        lock (_sync)
        {
            var ok = _users.TryGetValue(username.Trim(), out var user) && user.Password == password;
            return Task.FromResult(ok
                ? new AuthResult(true, "Đăng nhập thành công!", user!.Username)
                : new AuthResult(false, "Sai Username hoặc mật khẩu."));
        }
    }

    public Task<RegistrationOtpResult> RequestRegistrationOtpAsync(string email)
    {
        lock (_sync)
        {
            email = email.Trim();
            if (!System.Net.Mail.MailAddress.TryCreate(email, out var address) || address.Address != email)
                return Task.FromResult(RegistrationError("Email không đúng định dạng."));
            if (_users.Values.Any(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                return Task.FromResult(RegistrationError("Email đã được đăng ký."));
            DateTime now = DateTime.UtcNow;
            if (_pendingRegistrations.TryGetValue(email, out var pending) && now < pending.ResendAvailableAtUtc)
                return Task.FromResult(RegistrationError("Vui lòng đợi 60 giây để gửi lại mã."));
            return Task.FromResult(IssueRegistrationOtp(email, now));
        }
    }

    public Task<RegistrationOtpResult> ResendRegistrationOtpAsync(string email)
    {
        // Endpoint gửi lại OTP áp dụng cùng rate limit với lần gửi đầu.
        return RequestRegistrationOtpAsync(email);
    }

    private RegistrationOtpResult IssueRegistrationOtp(string email, DateTime now)
    {
        var request = new PendingRegistration
        {
            Email = email,
            ExpiresAtUtc = now.AddMinutes(5),
            ResendAvailableAtUtc = now.AddSeconds(60),
            FailedAttempts = 0
        };
        _pendingRegistrations[email] = request;
        // Mọi token xác minh cũ của cùng email bị thu hồi khi gửi mã mới.
        foreach (var token in _verifiedEmailTokens.Where(x =>
                     x.Value.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                     .Select(x => x.Key).ToList())
            _verifiedEmailTokens.Remove(token);
        return new RegistrationOtpResult(true,
            "Mã demo: 123456 (không gửi email thật).", request.ExpiresAtUtc, request.ResendAvailableAtUtc);
    }

    public Task<EmailVerificationResult> VerifyRegistrationOtpAsync(string email, string otp)
    {
        lock (_sync)
        {
            email = email.Trim();
            if (!_pendingRegistrations.TryGetValue(email, out var pending))
                return Task.FromResult(new EmailVerificationResult(false, "Hãy nhấn Gửi mã trước."));
            if (DateTime.UtcNow >= pending.ExpiresAtUtc)
                return Task.FromResult(new EmailVerificationResult(false, "OTP đã hết hạn. Hãy gửi lại mã."));
            if (pending.FailedAttempts >= 5)
                return Task.FromResult(new EmailVerificationResult(false, "Nhập sai quá 5 lần. Hãy gửi lại mã."));
            if (otp != "123456")
            {
                pending.FailedAttempts++;
                string error = pending.FailedAttempts >= 5
                    ? "Đã nhập sai 5 lần. Hãy gửi lại mã."
                    : "Mã OTP chưa đúng. Vui lòng thử lại.";
                return Task.FromResult(new EmailVerificationResult(false, error));
            }
            _pendingRegistrations.Remove(email); // OTP được sử dụng một lần.
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
            _verifiedEmailTokens[token] = new VerifiedEmail
            {
                Email = email,
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(10)
            };
            return Task.FromResult(new EmailVerificationResult(true,
                "Email đã xác minh! Bây giờ có thể tạo tài khoản.", token));
        }
    }

    public Task<AuthResult> RegisterVerifiedAsync(string username, string email, string password, string verificationToken)
    {
        lock (_sync)
        {
            username = username.Trim();
            email = email.Trim();
            if (username.Length is < 3 or > 20)
                return Task.FromResult(new AuthResult(false, "Username phải có 3–20 ký tự."));
            if (password.Length < 6)
                return Task.FromResult(new AuthResult(false, "Mật khẩu tối thiểu 6 ký tự."));
            if (!System.Net.Mail.MailAddress.TryCreate(email, out var addr) || addr.Address != email)
                return Task.FromResult(new AuthResult(false, "Email không đúng định dạng."));
            if (string.IsNullOrEmpty(verificationToken) ||
                !_verifiedEmailTokens.TryGetValue(verificationToken, out var verified) ||
                !string.Equals(verified.Email, email, StringComparison.OrdinalIgnoreCase) ||
                DateTime.UtcNow >= verified.ExpiresAtUtc)
                return Task.FromResult(new AuthResult(false, "Xác minh email đã hết hiệu lực. Vui lòng gửi OTP mới."));
            if (_users.ContainsKey(username))
                return Task.FromResult(new AuthResult(false, "Username đã được sử dụng."));
            if (_users.Values.Any(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                return Task.FromResult(new AuthResult(false, "Email đã được đăng ký."));

            _users[username] = new User
            {
                Username = username,
                Email = email,
                Password = password,
                CreatedAt = DateTime.Now
            };
            _verifiedEmailTokens.Remove(verificationToken); // Token chỉ dùng một lần.
            return Task.FromResult(new AuthResult(true, "Đăng ký thành công!", username));
        }
    }

    private static RegistrationOtpResult RegistrationError(string message) =>
        new(false, message, default, default);

    public Task<AuthResult> RequestPasswordResetAsync(string email)
    {
        lock (_sync)
        {
            email = email.Trim();
            if (_users.Values.Any(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                _otpRequests[email] = new ResetRequest { Email = email, ExpiresAt = DateTime.UtcNow.AddMinutes(5) };
            // Phản hồi chung, tránh tiết lộ email nào đã đăng ký.
            return Task.FromResult(new AuthResult(true, "Nếu email hợp lệ, mã xác nhận đã được gửi."));
        }
    }

    public Task<OtpVerificationResult> VerifyOtpAsync(string email, string otp)
    {
        lock (_sync)
        {
            if (!_otpRequests.TryGetValue(email, out var request))
                return Task.FromResult(new OtpVerificationResult(false, "Không tìm thấy yêu cầu. Vui lòng gửi lại mã."));
            if (DateTime.UtcNow > request.ExpiresAt)
            {
                _otpRequests.Remove(email);
                return Task.FromResult(new OtpVerificationResult(false, "OTP hết hạn sau 5 phút. Vui lòng gửi lại mã."));
            }
            if (otp != "123456")
                return Task.FromResult(new OtpVerificationResult(false, "Mã OTP không chính xác."));

            _otpRequests.Remove(email); // OTP chỉ dùng một lần.
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
            _validResetTokens[token] = email;
            return Task.FromResult(new OtpVerificationResult(true, "OTP hợp lệ.", token));
        }
    }

    public Task<AuthResult> ResetPasswordAsync(string resetToken, string newPassword)
    {
        lock (_sync)
        {
            if (!_validResetTokens.Remove(resetToken, out string? email))
                return Task.FromResult(new AuthResult(false, "Phiên xác nhận đã hết hạn hoặc được sử dụng."));
            var user = _users.Values.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (user is null)
                return Task.FromResult(new AuthResult(false, "Không tìm thấy tài khoản."));
            user.Password = newPassword;
            return Task.FromResult(new AuthResult(true, "Mật khẩu đã được cập nhật. Bạn có thể đăng nhập."));
        }
    }

    public Task<AccountProfile?> GetProfileAsync(string username)
    {
        lock (_sync)
        {
            var profile = _users.TryGetValue(username, out var user)
                ? new AccountProfile(user.Username, user.Email, user.CreatedAt)
                : null;
            return Task.FromResult(profile);
        }
    }
}
