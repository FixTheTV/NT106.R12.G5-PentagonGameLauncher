using System;
using System.Threading.Tasks;    //hỗ trợ cho lậop trình BẤT ĐỒNG BỘ: với Task, async/await, giúp chương trình thực hiện các tác vụ nặng (truy vấn dl, đọc ghi file) ở chế độ nền mà không làm đơ giao diện người dùng
using MySqlConnector;

namespace PentagonLauncher.Backend
{
    public class DatabaseService
    {
        // Chuỗi kết nối đến MySQL cục bộ trên máy tính của mình  (thay đổi user/pass nếu MySQL của mình có mật khẩu)
        private readonly string _connectionString = "Server=localhost;Database=tictactoe_db;Uid=root;Pwd=;";

        public DatabaseService() { }  //hàm khởi tạo không tham số(mặc định trỏ về localhost,...

        public DatabaseService(string connectionString) //... có tham số : đề khi triển khai thực tế lên máy tính khác(có password MýQL riêng thì truyền chuỗi kết nối trong file cấu hình đó vào hàm này- để chuyển đổi môi trường 
        {
            _connectionString = connectionString;
        }

        //phạm vi private: chỉ cho các hàm nội bộ thuộc class DatabaseService sủ dụng 
        private MySqlConnection GetConnection()  //mỗi lần ứng dụng cần thao tác vs database(đăng ký,...) =cần 1 đối tượng MySqlConnection(để kết nối) => các hàm bên dưới chỉ cần gọi hàm GetConnection()    [đỡ  ghi new MySqlConnection(_connectionString); dài dòng   ]
        {
            return new MySqlConnection(_connectionString); 
        }

        // 1. Kiểm tra username đã tồn tại chưa(True/false)
        public async Task<bool> IsUsernameExistsAsync(string username)  // Task<bool>: là 1 kiểu dữ liệu ( task đại diện cho công việc đang được xử lý ở chế độ nền,
        {
            using var conn = GetConnection();                           //tạo đối tượng kết nối. using để tự động đóng  (dispose() cái kết nối conn) khi hàm chạy xong haowcj gặp lỗi thoát ra ngoài 
            await conn.OpenAsync();                                     //giờ mới mở cổng kết nối( từ đối tượng kết nối conn) đến MySQL, await + OpenAsync() : hệ thống tạm nhường luồng xử lý cho việc khác trong lúc chờ kết nối mạng được thiết lập 
            using var cmd = new MySqlCommand("SELECT COUNT(1) FROM users WHERE username = @user", conn); //new MySqlCommand(...,con) tạo đối tượng cmd  thưc thi câu lệnh SQL... trên kết nối conn vừa mở,  
            cmd.Parameters.AddWithValue("@user", username);             //giờ mới đặt username vào @user(tham số giữ chỗ:Prameters )- ngăn chặn SQL Injection: kẻ xấu ko thể tiêm mã độc vào ô nhập liệu để phá hoại/đánh cắp
                                                                        //ExecuteScalarAsync(): thực thi câu lệnh SQL bất đồng bộ, chri trả về (kiểu object)  duy nhất 1 giá trị(ô 1 dòng 1=số lượng bản ghi vừa đếm được ); await: tạm dưng hàm Convert để chờ hàm ExecuteScalarAsync() thực thi xong 
            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());//vì ExecuteScalarAsync() tra vể kiểu object, nên cần Convert.ToInt32 để gán cho count, rồi so count với 0  
            return count > 0; 
        }

        // 2. Kiểm tra email đã tồn tại chưa
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand("SELECT COUNT(1) FROM users WHERE email = @email", conn);
            cmd.Parameters.AddWithValue("@email", email);
            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }

        // 3. Thêm tài khoản mới vào CSDL
        public async Task<bool> CreateUserAsync(string username, string email, string passwordHash)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand(
                "INSERT INTO users (username, email, password_hash) VALUES (@user, @email, @hash)", conn);
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@hash", passwordHash);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        // 4. Lấy thông tin tài khoản theo Username để đăng nhập
        public async Task<(int Id, string Username, string PasswordHash)?> GetUserByUsernameAsync(string username)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand("SELECT id, username, password_hash FROM users WHERE username = @user", conn);
            cmd.Parameters.AddWithValue("@user", username);
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return (reader.GetInt32("id"), reader.GetString("username"), reader.GetString("password_hash"));
            }
            return null;
        }

        // 5. Lấy User theo Email (để phục vụ quên mật khẩu)
        public async Task<int?> GetUserIdByEmailAsync(string email)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand("SELECT id FROM users WHERE email = @email", conn);
            cmd.Parameters.AddWithValue("@email", email);
            var result = await cmd.ExecuteScalarAsync();
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return null;
        }

        // 6. Lưu mã OTP mới vào bảng password_resets
        public async Task<bool> SaveOtpAsync(int userId, string otpCode, DateTime expiresAt)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand(
                "INSERT INTO password_resets (user_id, otp_code, expires_at) VALUES (@uid, @otp, @exp)", conn);
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@otp", otpCode);
            cmd.Parameters.AddWithValue("@exp", expiresAt);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        // 7. Lấy thông tin bản ghi OTP mới nhất chưa sử dụng của người dùng
        public async Task<(int ResetId, string OtpCode, DateTime ExpiresAt)?> GetLatestOtpAsync(int userId)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand(
                "SELECT id, otp_code, expires_at FROM password_resets WHERE user_id = @uid AND is_used = 0 ORDER BY created_at DESC LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@uid", userId);
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return (reader.GetInt32("id"), reader.GetString("otp_code"), reader.GetDateTime("expires_at"));
            }
            return null;
        }

        // 8. Đánh dấu mã OTP đã được sử dụng
        public async Task MarkOtpUsedAsync(int resetId)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand("UPDATE password_resets SET is_used = 1 WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", resetId);
            await cmd.ExecuteNonQueryAsync();
        }

        // 9. Cập nhật mật khẩu băm mới cho user
        public async Task<bool> UpdatePasswordAsync(int userId, string newPasswordHash)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand("UPDATE users SET password_hash = @hash WHERE id = @uid", conn);
            cmd.Parameters.AddWithValue("@hash", newPasswordHash);
            cmd.Parameters.AddWithValue("@uid", userId);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }

}
