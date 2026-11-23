using BCrypt.Net;  //thư viện để băm mật khẩu 1 chiều(HashPassword khi tạo, Verify khi đối chiếu)
using PentagonLauncher.Shared.Interfaces;
using PentagonLauncher.Shared.Models.Authentication;//de lấy được file LoginResult.cs va file Longin Result.cs bên .Shared.Models.authentcation , để bổ sung 2 hàm overload (hàm cùng tên nhưng nhận model ) 
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PentagonLauncher.Backend.Services;

public class AuthService: IBackendAuthService
{
    public async Task<bool> IsVerifiedCodeAsync(string email, byte purpose, string code) =>
        await _db.GetValidOtpIdAsync(email, (OTP.OtpPurpose)purpose, code) is not null;
    private readonly DatabaseService _db;
    private readonly OTPService _otpService;
    // Biểu thức chính quy: Username chỉ gồm chữ cái, số, gạch dưới, từ 3 - 20 ký tự
    private static readonly Regex UsernameRegex = new Regex("^[a-zA-Z0-9_]{3,20}$", RegexOptions.Compiled);

    public AuthService(DatabaseService db, OTPService otpService)
    {
        _db = db;
        _otpService = otpService;
    }


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

        int? verifiedOtpId = await _db.GetVerifiedRegistrationOtpIdAsync(email);

        if (verifiedOtpId is null)
        {
            return (false, "Email chưa được xác minh OTP hoặc mã đã hết hạn.");
        }

        // Băm mật khẩu bằng BCrypt
        string hash = BCrypt.Net.BCrypt.HashPassword(password);
        bool created = await _db.CompleteOtpAsync(verifiedOtpId.Value, email, hash, username);

        if (!created)
        {
            return (false, "Không tạo được tài khoản: tên/email có thể vừa được sử dụng hoặc OTP không còn hiệu lực. Vui lòng kiểm tra và xác minh lại.");
        }


        return (true, "Đăng ký tài khoản thành công.");
    }

    //hàm overload(hàm cùng tên-RegisterAsync nhưng nhận Model): Cùng gọi hàm RegisterAsync, nhưng ko truyền vào 3 biến như ở bên trên, mà truyền vào đối tượng kiểu LoginRequest, thì nó sẽ chạy  hàm này, chứu ko phải hàm trên 
    public async Task<RegisterResult> RegisterAsync(RegisterRequest request)
    {
        var res = await RegisterAsync(request.Username, request.Email, request.Password);
        return new RegisterResult
        {
            Success = res.Success,
            Message = res.Message
        };
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
    //Hàm overload-LoginAsync
    public async Task<LoginResult> LoginAsync(LoginRequest request)
    {
        var res = await LoginAsync(request.Username, request.Password);
        return new LoginResult
        {
            Success = res.Success,
            Message = res.Message,
            UserId = res.UserId,
            Username = res.Username
        };
    }

    // Task: Xây dựng chức năng đăng xuất
    public (bool Success, string Message) Logout(string username)
    {
        // Tầng nghiệp vụ ghi nhận phiên đăng xuất thành công
        return (true, $"Tài khoản {username} đã đăng xuất thành công.");
    }




    // Task: Xây dựng chức năng yêu cầu đặt lại mật khẩu & Gửi mã xác nhận
    public async Task<(bool Success, string Message)> RequestPasswordResetAsync(string email)
    {
        email = email.Trim().ToLower();

        int? userId = await _db.GetUserIdByEmailAsync(email);

        if (userId == null)
        {
            return (false, "Email này không tồn tại trong hệ thống.");
        }
        else
        {
            var result = await _otpService.CreatePasswordResetOTP(email, OTP.OtpPurpose.PasswordReset);

        }
            


        return (true, "Yêu cầu thành công");
    }
    // Hàm overload RequestPasswordResetAsync: Nhận Model ForgotPasswordRequest
    public async Task<ForgotPasswordResult> RequestPasswordResetAsync(ForgotPasswordRequest request)
    {
        var res = await RequestPasswordResetAsync(request.Email);
        return new ForgotPasswordResult
        {
            Success = res.Success,
            Message = res.Message
        };
    }

    // Task: Xây dựng kiểm tra mã xác nhận, hạn mã & đặt lại mật khẩu
    public async Task<(bool Success, string Message)> ResetPasswordAsync(string email, string newPassword)
    {
        email = email.Trim().ToLower();
        if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
            return (false, "Mật khẩu cần ít nhất 6 ký tự.");

        int? otpId = await _db.GetVerifiedOtpIdAsync(email, OTP.OtpPurpose.PasswordReset);
        if (otpId is null)
            return (false, "Vui lòng xác minh OTP đặt lại mật khẩu còn hiệu lực.");

        int? userId = await _db.GetUserIdByEmailAsync(email);
        if (userId == null)
            return (false, "Tài khoản không tồn tại.");


        string newHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        if (!await _db.CompleteOtpAsync(otpId.Value, email, newHash))
            return (false, "Không cập nhật được mật khẩu.");


        return (true, "Đặt lại mật khẩu thành công! Bạn có thể đăng nhập ngay.");
    }

    // Hàm overload ResetPasswordAsync: Nhận Model ResetPasswordRequest
    public async Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var res = await ResetPasswordAsync(request.Email, request.NewPassword);
        return new ResetPasswordResult
        {
            Success = res.Success,
            Message = res.Message
        };
    }


    public async Task<OtpResult> VerifyRegisterOTP(string email, string otpCode, OTP.OtpPurpose purpose)
    {
        if (purpose != OTP.OtpPurpose.Registration) return new OtpResult { Success = false };
        var otpID = await _db.GetValidOtpIdAsync(email,purpose,otpCode);

        if (otpID != null)
        {
            var markVerified = await _db.MarkOtpVerifiedAsync(otpID);
            if (markVerified)
            {
                return new OtpResult { Success = true };
            }
        }
        else
        {
            return new OtpResult { Success = false };
        }

        return new OtpResult { Success = false };

    }


    public async Task<OtpResult> VerifyResetPassOTP(string email, string otpCode, OTP.OtpPurpose purpose)
    {
        if (purpose != OTP.OtpPurpose.PasswordReset) return new OtpResult { Success = false };
        var otpID = await _db.GetValidOtpIdAsync(email, purpose, otpCode);

        if (otpID != null)
        {
            var markVerified = await _db.MarkOtpVerifiedAsync(otpID);
            if (markVerified)
            {
                return new OtpResult { Success = true };
            }
        }
        else
        {
            return new OtpResult { Success = false };
        }

        return new OtpResult { Success = false };

    }



}



//Băm mật khẩu một chiều bằng thư viện chuẩn BCrypt.Net (HashPassword khi tạo, Verify khi đối chiếu).
//Kiểm tra chặt chẽ độ dài, định dạng regex tên người dùng (^[a-zA-Z0-9_]{3,20}$) và email hợp lệ.   
//Luồng OTP: sinh ngẫu nhiên 6 chữ số, quy định thời hạn hiệu lực đúng 5 phút (DateTime.UtcNow.AddMinutes(5)) và in ra console rõ ràng để phục vụ demo.
