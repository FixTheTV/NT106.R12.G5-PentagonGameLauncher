using System;
using System.Threading.Tasks;
using MySqlConnector;
using PentagonLauncher.Shared.Models.Authentication;

namespace PentagonLauncher.Backend.Services
{
    public class DatabaseService
    {
        // 1. Chuỗi kết nối đến cơ sở dữ liệu
        private readonly string _connectionString;

        // 2. Khởi tạo DatabaseService
        public DatabaseService(string connectionString)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

            _connectionString = connectionString;
            InitializeDatabase();
        }

        // 3. Tạo kết nối mới đến MySQL
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // 4. Khởi tạo các bảng cần thiết trong cơ sở dữ liệu
        private void InitializeDatabase()
        {
            using var conn = GetConnection();
            conn.Open();

            string createUsersTable = @"
                CREATE TABLE IF NOT EXISTS users (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    username VARCHAR(50) NOT NULL UNIQUE,
                    email VARCHAR(255) NOT NULL UNIQUE,
                    password_hash VARCHAR(255) NOT NULL,
                    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
                );";

            string createOtpRequestsTable = @"
                CREATE TABLE IF NOT EXISTS otp_requests (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    email VARCHAR(255) NOT NULL,
                    purpose TINYINT UNSIGNED NOT NULL,
                    otp_code CHAR(6) NOT NULL,
                    is_verified BOOLEAN NOT NULL DEFAULT FALSE,
                    is_used BOOLEAN NOT NULL DEFAULT FALSE,

                    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

                    expired_at DATETIME
                        GENERATED ALWAYS AS (
                            DATE_ADD(created_at, INTERVAL 5 MINUTE)
                        ) STORED,

                    INDEX ix_otp_lookup (
                        email,
                        purpose,
                        is_used,
                        expired_at
                    )
                );";

            string createPasswordResetsTable = @"
                CREATE TABLE IF NOT EXISTS password_resets (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    user_id INT NOT NULL,
                    otp_request_id INT NOT NULL,
                    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

                    CONSTRAINT fk_password_resets_users
                        FOREIGN KEY (user_id)
                        REFERENCES users(id)
                        ON DELETE CASCADE,

                    CONSTRAINT fk_password_resets_otps
                        FOREIGN KEY (otp_request_id)
                        REFERENCES otp_requests(id)
                        ON DELETE CASCADE
                );";

            using var cmd = new MySqlCommand(
                createUsersTable
                + createOtpRequestsTable
                + createPasswordResetsTable,
                conn);

            cmd.ExecuteNonQuery();
        }

        // ============================================================
        // 5. QUẢN LÝ TÀI KHOẢN
        // ============================================================

        // 5.1. Kiểm tra username đã tồn tại
        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(
                "SELECT COUNT(1) FROM users WHERE username = @user",
                conn);

            cmd.Parameters.AddWithValue("@user", username);

            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }

        // 5.2. Kiểm tra email đã tồn tại
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(
                "SELECT COUNT(1) FROM users WHERE email = @email",
                conn);

            cmd.Parameters.AddWithValue("@email", email);

            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }

        // 5.3. Tạo tài khoản mới
        public async Task<bool> CreateUserAsync(
            string username,
            string email,
            string passwordHash)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(
                """
                INSERT INTO users (username, email, password_hash)
                VALUES (@user, @email, @hash)
                """,
                conn);

            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@hash", passwordHash);

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        // 5.4. Lấy thông tin tài khoản theo username
        public async Task<(int Id, string Username, string PasswordHash)?>
            GetUserByUsernameAsync(string username)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(
                """
                SELECT id, username, password_hash
                FROM users
                WHERE username = @user
                """,
                conn);

            cmd.Parameters.AddWithValue("@user", username);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return (
                    reader.GetInt32("id"),
                    reader.GetString("username"),
                    reader.GetString("password_hash"));
            }

            return null;
        }

        // 5.5. Lấy ID tài khoản theo email
        public async Task<int?> GetUserIdByEmailAsync(string email)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(
                "SELECT id FROM users WHERE email = @email",
                conn);

            cmd.Parameters.AddWithValue("@email", email);

            var result = await cmd.ExecuteScalarAsync();

            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }

            return null;
        }

        // 5.6. Hoàn tất đăng ký hoặc đổi mật khẩu sau khi xác thực OTP
        public async Task<bool> CompleteOtpAsync(
            int otpId,
            string email,
            string hash,
            string? username = null)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var tx = await conn.BeginTransactionAsync();

            using var consume = new MySqlCommand(
                """
                UPDATE otp_requests
                SET is_used = TRUE
                WHERE id = @id
                  AND email = @email
                  AND purpose = @purpose
                  AND is_verified = TRUE
                  AND is_used = FALSE
                  AND expired_at > CURRENT_TIMESTAMP
                """,
                conn,
                tx);

            consume.Parameters.AddWithValue("@id", otpId);
            consume.Parameters.AddWithValue("@email", email);
            consume.Parameters.AddWithValue(
                "@purpose",
                username is null ? 2 : 1);

            if (await consume.ExecuteNonQueryAsync() != 1)
            {
                return false;
            }

            using var account = new MySqlCommand(
                username is null
                    ? "UPDATE users SET password_hash = @hash WHERE email = @email"
                    : """
                      INSERT INTO users(username, email, password_hash)
                      VALUES(@username, @email, @hash)
                      """,
                conn,
                tx);

            account.Parameters.AddWithValue("@email", email);
            account.Parameters.AddWithValue("@hash", hash);

            if (username is not null)
            {
                account.Parameters.AddWithValue("@username", username);
            }

            try
            {
                if (await account.ExecuteNonQueryAsync() != 1)
                {
                    return false;
                }

                await tx.CommitAsync();
                return true;
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return false;
            }
        }

        // ============================================================
        // 6. QUẢN LÝ OTP
        // ============================================================

        // 6.1. Lưu OTP mới và vô hiệu hóa OTP cũ
        public async Task<bool> SaveOTPAsync(string email, OTP otp)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var tx = await conn.BeginTransactionAsync();

            using var invalidate = new MySqlCommand(
                """
                UPDATE otp_requests
                SET is_used = TRUE
                WHERE email = @email
                  AND purpose = @purpose
                  AND is_used = FALSE
                """,
                conn,
                tx);

            var normalizedEmail = email.Trim().ToLowerInvariant();

            invalidate.Parameters.AddWithValue("@email", normalizedEmail);
            invalidate.Parameters.AddWithValue("@purpose", (byte)otp.Purpose);

            await invalidate.ExecuteNonQueryAsync();

            using var cmd = new MySqlCommand(
                """
                INSERT INTO otp_requests (email, purpose, otp_code)
                VALUES (@email, @purpose, @code)
                """,
                conn,
                tx);

            cmd.Parameters.AddWithValue("@email", normalizedEmail);
            cmd.Parameters.AddWithValue("@code", otp.Code);
            cmd.Parameters.AddWithValue("@purpose", (int)otp.Purpose);

            bool saved = await cmd.ExecuteNonQueryAsync() > 0;

            await tx.CommitAsync();
            return saved;
        }

        // 6.2. Tìm OTP hợp lệ theo email, mục đích và mã OTP
        public async Task<int?> GetValidOtpIdAsync(
            string email,
            OTP.OtpPurpose purpose,
            string otpCode)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            const string sql = """
                SELECT id
                FROM otp_requests
                WHERE email = @email
                  AND purpose = @purpose
                  AND otp_code = @otpCode
                  AND is_used = FALSE
                  AND expired_at > CURRENT_TIMESTAMP
                ORDER BY created_at DESC
                LIMIT 1;
                """;

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue(
                "@email",
                email.Trim().ToLowerInvariant());

            cmd.Parameters.AddWithValue("@purpose", (byte)purpose);
            cmd.Parameters.AddWithValue("@otpCode", otpCode.Trim());

            object? result = await cmd.ExecuteScalarAsync();

            return result is null || result == DBNull.Value
                ? null
                : Convert.ToInt32(result);
        }

        // 6.3. Đánh dấu OTP đã được xác thực
        public async Task<bool> MarkOtpVerifiedAsync(int? otpId)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            const string sql = """
                UPDATE otp_requests
                SET is_verified = TRUE
                WHERE id = @otpId
                  AND is_used = FALSE
                  AND expired_at > CURRENT_TIMESTAMP;
                """;

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@otpId", otpId);

            return await cmd.ExecuteNonQueryAsync() == 1;
        }

        // 6.4. Lấy OTP đã xác thực theo email và mục đích
        public async Task<int?> GetVerifiedOtpIdAsync(
            string email,
            OTP.OtpPurpose purpose)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            const string sql = """
                SELECT id
                FROM otp_requests
                WHERE email = @email
                  AND purpose = @purpose
                  AND is_verified = TRUE
                  AND is_used = FALSE
                  AND expired_at > CURRENT_TIMESTAMP
                ORDER BY created_at DESC
                LIMIT 1;
                """;

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue(
                "@email",
                email.Trim().ToLowerInvariant());

            cmd.Parameters.AddWithValue("@purpose", (byte)purpose);

            object? result = await cmd.ExecuteScalarAsync();

            return result is null || result == DBNull.Value
                ? null
                : Convert.ToInt32(result);
        }

        // 6.5. Lấy OTP đăng ký đã xác thực
        public async Task<int?> GetVerifiedRegistrationOtpIdAsync(string email)
        {
            return await GetVerifiedOtpIdAsync(
                email,
                OTP.OtpPurpose.Registration);
        }

        // 6.6. Đánh dấu OTP đã được sử dụng
        public async Task MarkOtpUsedAsync(int resetId)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            const string sql = """
                UPDATE otp_requests
                SET is_used = TRUE
                WHERE id = @otpId;
                """;

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@otpId", resetId);

            await cmd.ExecuteNonQueryAsync();
        }

        // ============================================================
        // 7. QUẢN LÝ ĐẶT LẠI MẬT KHẨU
        // ============================================================

        // 7.1. Lưu yêu cầu đặt lại mật khẩu
        /*public async Task<bool> SavePassResetAsync(int userId)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(
                """
                INSERT INTO password_resets (user_id, otp_code)
                VALUES (@uid)
                """,
                conn);

            cmd.Parameters.AddWithValue("@uid", userId);

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        // 7.2. Lấy OTP đặt lại mật khẩu mới nhất chưa sử dụng
        public async Task<(int ResetId, string OtpCode, DateTime ExpiresAt)?>
            GetLatestOtpAsync(int userId)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(
                """
                SELECT id, otp_code, expired_at
                FROM password_resets
                WHERE user_id = @uid
                  AND is_used = 0
                ORDER BY created_at DESC
                LIMIT 1
                """,
                conn);

            cmd.Parameters.AddWithValue("@uid", userId);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return (
                    reader.GetInt32("id"),
                    reader.GetString("otp_code"),
                    reader.GetDateTime("expired_at"));
            }

            return null;
        }
        */

        // 7.3. Cập nhật mật khẩu mới
        public async Task<bool> UpdatePasswordAsync(
            int userId,
            string newPasswordHash)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(
                """
                UPDATE users
                SET password_hash = @hash
                WHERE id = @uid
                """,
                conn);

            cmd.Parameters.AddWithValue("@hash", newPasswordHash);
            cmd.Parameters.AddWithValue("@uid", userId);

            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}