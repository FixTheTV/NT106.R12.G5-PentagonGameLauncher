using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;

namespace PentagonLauncher.Backend
{
    public class AuthService
    {
        private readonly DatabaseService _db;
        // Biểu thức chính quy: Username chỉ gồm chữ cái, số, gạch dưới, từ 3 - 20 ký tự
        private static readonly Regex UsernameRegex = new Regex("^[a-zA-Z0-9_]{3,20}$", RegexOptions.Compiled);

        public AuthService(DatabaseService db)
        {
            _db = db;
        }

        #region 1. Nhóm Đăng ký & Đăng nhập

        // Task: Xây dựng kiểm tra dữ liệu đầu vào (Validate Input)
        public (bool IsValid, string Message) ValidateRegistrationInput(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || !UsernameRegex.IsMatch(username.Trim()))
                return (false, "Username phải từ 3 đến 20 ký tự (chỉ gồm chữ cái, số, dấu gạch dưới).");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
                return (false, "Định dạng email không hợp lệ.");

            if (string.IsNullOrEmpty(password) || password.Length < 6)
                return (false, "Mật khẩu tối thiểu phải từ 6 ký tự trở lên.");

            return (true, string.Empty);
        }

        // Task: Xây dựng chức năng đăng ký
        public async Task<(bool Success, string Message)> RegisterAsync(string username, string email, string password)
        {
            username = username.Trim();
            email = email.Trim().ToLower();

            // Kiểm tra dữ liệu hợp lệ
            var validation = ValidateRegistrationInput(username, email, password);
            if (!validation.IsValid)
                return (false, validation.Message);

            // Kiểm tra trùng lặp trong CSDL
            if (await _db.IsUsernameExistsAsync(username))
                return (false, "Tên tài khoản này đã tồn tại.");

            if (await _db.IsEmailExistsAsync(email))
                return (false, "Địa chỉ email này đã được sử dụng.");

            // Băm mật khẩu bằng BCrypt
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            bool created = await _db.CreateUserAsync(username, email, hash);

            return created ? (true, "Đăng ký tài khoản thành công.") : (false, "Lỗi khi lưu dữ liệu người dùng.");
        }

        // Task: Xây dựng chức năng đăng nhập
        public async Task<(bool Success, string Message, int UserId, string Username)> LoginAsync(string username, string password)
        {
            username = username.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return (false, "Vui lòng nhập đầy đủ tài khoản và mật khẩu.", 0, string.Empty);

            // Tìm trong DB
            var user = await _db.GetUserByUsernameAsync(username);
            if (user == null)
            {
                // Task: Xử lý trường hợp tài khoản không tồn tại
                return (false, "Tài khoản không tồn tại.", 0, string.Empty);
            }

            // Kiểm tra mật khẩu khớp với hash bằng BCrypt
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, user.Value.PasswordHash);
            if (!isPasswordCorrect)
                return (false, "Mật khẩu không chính xác.", 0, string.Empty);

            return (true, "Đăng nhập thành công.", user.Value.Id, user.Value.Username);
        }

        // Task: Xây dựng chức năng đăng xuất
        public (bool Success, string Message) Logout(string username)
        {
            // Tầng nghiệp vụ ghi nhận phiên đăng xuất thành công
            return (true, $"Tài khoản {username} đã đăng xuất thành công.");
        }

        #endregion

        #region 2. Nhóm Quên & Đặt lại mật khẩu (OTP)

        // Task: Xây dựng chức năng yêu cầu đặt lại mật khẩu & Gửi mã xác nhận
        public async Task<(bool Success, string Message)> RequestPasswordResetAsync(string email)
        {
            email = email.Trim().ToLower();

            // Task: Xử lý trường hợp tài khoản không tồn tại
            int? userId = await _db.GetUserIdByEmailAsync(email);
            if (userId == null)
                return (false, "Email này không tồn tại trong hệ thống.");

            // Sinh mã OTP 6 số ngẫu nhiên
            string otpCode = Random.Shared.Next(100000, 1000000).ToString();
            // Task: Xây dựng kiểm tra thời hạn mã xác nhận (hạn 5 phút)
            DateTime expiresAt = DateTime.UtcNow.AddMinutes(5);

            await _db.SaveOtpAsync(userId.Value, otpCode, expiresAt);

            // In mã OTP ra màn hình Console để chạy demo đồ án môn học
            Console.WriteLine("====================================================");
            Console.WriteLine($"[DEMO OTP] Ma xac nhan cho {email} la: {otpCode}");
            Console.WriteLine($"[DEMO OTP] Hieu luc den: {expiresAt.ToLocalTime()}");
            Console.WriteLine("====================================================");

            return (true, "Mã xác nhận 6 chữ số đã được gửi (kiểm tra console server).");
        }

        // Task: Xây dựng kiểm tra mã xác nhận, hạn mã & đặt lại mật khẩu
        public async Task<(bool Success, string Message)> ResetPasswordAsync(string email, string otpCode, string newPassword)
        {
            email = email.Trim().ToLower();
            otpCode = otpCode.Trim();

            // 1. Kiểm tra tài khoản tồn tại
            int? userId = await _db.GetUserIdByEmailAsync(email);
            if (userId == null)
                return (false, "Tài khoản không tồn tại.");

            // 2. Task: Xây dựng kiểm tra mật khẩu mới
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
                return (false, "Mật khẩu mới tối thiểu phải từ 6 ký tự.");

            // 3. Lấy OTP từ CSDL
            var otpRecord = await _db.GetLatestOtpAsync(userId.Value);
            if (otpRecord == null)
                return (false, "Không tìm thấy yêu cầu đặt lại mật khẩu nào.");

            // 4. Task: Xử lý trường hợp mã xác nhận không hợp lệ
            if (otpRecord.Value.OtpCode != otpCode)
                return (false, "Mã xác nhận không hợp lệ.");

            // 5. Task: Xử lý trường hợp mã xác nhận hết hạn
            if (DateTime.UtcNow > otpRecord.Value.ExpiresAt)
                return (false, "Mã xác nhận đã hết hạn (quá 5 phút). Vui lòng xin mã mới.");

            // 6. Cập nhật mật khẩu mới và đánh dấu OTP đã dùng
            string newHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _db.UpdatePasswordAsync(userId.Value, newHash);
            await _db.MarkOtpUsedAsync(otpRecord.Value.ResetId);

            return (true, "Đặt lại mật khẩu thành công! Bạn có thể đăng nhập ngay.");
        }

        #endregion
    }
}