using System;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using PentagonLauncher.Launcher.Networking;
using PentagonLauncher.Shared.Interfaces;
using PentagonLauncher.Shared.Models.Authentication;

namespace PentagonLauncher.Launcher.Forms;

/// <summary>
/// Đăng ký V7: nhập OTP NGAY dưới Email. Không mở RegistrationOtpForm nữa.
/// Tách email verified token và lệnh RegisterVerifiedAsync để chỉ tạo tài khoản
/// sau khi người dùng bấm nút Đăng ký và backend kiểm tra quyền xác minh email.
/// </summary>
public partial class RegisterForm : Form
{
    private readonly LauncherTcpClient client = LauncherTcpClient.Shared;
    private readonly System.Windows.Forms.Timer _clock = new() { Interval = 1000 };
    private readonly TextBox[] _digits;
    private string _requestedEmail = "";
    private string _verifiedEmail = "";
    private string _verificationToken = "";
    private bool _changingOtp;

    private bool actuallyVerified = false;
    private bool _busy;
    private bool _requestPending;

    public string VerifiedUsername { get; private set; } = "";

    public RegisterForm()
    {
        InitializeComponent();
        _digits = new[] { txtOtp1, txtOtp2, txtOtp3, txtOtp4, txtOtp5, txtOtp6 };
        foreach (TextBox digit in _digits)
        {
            digit.KeyPress += Digit_KeyPress;
            digit.KeyDown += Digit_KeyDown;
            digit.TextChanged += Digit_TextChanged;
        }
        txtEmail.TextChanged += (_, _) => ResetVerification();
        _clock.Tick += Clock_Tick;
        FormClosed += (_, _) => _clock.Dispose();
        AcceptButton = btnRegister;
        UpdateControls();
    }

    private static bool IsValidEmail(string value) =>
        MailAddress.TryCreate(value, out var parsed) && parsed.Address == value;

    private void ShowError(string message)
    {
        lblStatus.ForeColor = Color.FromArgb(255, 172, 181);
        lblStatus.Text = message;
    }

    private void ResetVerification()
    {
        _clock.Stop();
        _verificationToken = "";
        _verifiedEmail = "";
        _requestedEmail = "";
        _requestPending = false;
        foreach (TextBox digit in _digits) digit.Clear();
        lblOtpStatus.Text = "Nhấn Gửi mã để xác minh email.";
        lblStatus.Text = "";
        UpdateControls();
    }

    private void Clock_Tick(object? sender, EventArgs e) => UpdateControls();

    private void UpdateControls()
    {
        if (IsDisposed) return;
        bool verified = actuallyVerified;
        bool active = _requestPending  && !verified;

        lblCountdown.Text = verified ? "Email đã xác minh" : "Email chưa xác minh";
        lblCountdown.ForeColor = verified ? Color.FromArgb(92, 232, 180) : Color.FromArgb(64, 217, 255);
        lblOtpState.Text = verified ? "Đã xác minh" : _requestPending && !active
            ? "Mã hết hạn" : "Chưa xác minh";
        lblOtpState.ForeColor = verified ? Color.FromArgb(92, 232, 180) : Color.FromArgb(247, 164, 179);
        btnSendOtp.Text = verified ? "Đã xác minh" : true
            ? $"Gửi lại (0s)" : _requestPending ? "Gửi lại mã" : "Gửi mã";
        btnSendOtp.Enabled = !_busy && !verified;
        btnVerifyOtp.Enabled = !_busy && active && _digits.All(d => d.TextLength == 1);
        foreach (TextBox digit in _digits) digit.Enabled = !_busy && active;
        txtUsername.Enabled = !_busy;
        txtEmail.Enabled = !_busy;
        txtPassword.Enabled = !_busy;
        txtConfirm.Enabled = !_busy;
        btnRegister.Enabled = !_busy;
        lnkBack.Enabled = !_busy;
    }

    private async void btnSendOtp_Click(object? sender, EventArgs e)
    {
        try
        {
            await client.ConnectAsync();
        }
        catch
        {
            MessageBox.Show("Khong ket noi duoc voi may chu");
        }
        if (_busy || _verificationToken.Length > 0) return;
        string email = txtEmail.Text.Trim();
        if (!IsValidEmail(email))
        {
            lblOtpStatus.Text = "Email không đúng định dạng.";
            txtEmail.Focus();
            return;
        }
        _busy = true;
        UpdateControls();
        try
        {
            RegisterOTPResult result = _requestPending &&
                email.Equals(_requestedEmail, StringComparison.OrdinalIgnoreCase)
                ? await client.RequestRegisterOTPAsync(email)
                : await client.RequestRegisterOTPAsync(email);
            if (IsDisposed) return;
            if (!result.Success) { lblOtpStatus.Text = "Đã xảy ra lỗi"; return; }
            _requestedEmail = email;
            _requestPending = true;
            _clock.Start();
            foreach (TextBox digit in _digits) digit.Clear();
            lblOtpStatus.Text = $"Đã gửi yêu cầu và nhận mã OTP: {result.OTPCode}";
            _digits[0].Focus();
        }
        catch (Exception ex) { lblOtpStatus.Text = $"Không gửi được OTP: {ex.Message}"; }
        finally { _busy = false; if (!IsDisposed) UpdateControls(); }
    }

    private static void Digit_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !(e.KeyChar >= '0' && e.KeyChar <= '9'))
            e.Handled = true;
        if (e.KeyChar == '\r' || e.KeyChar == '\n') e.Handled = true;
    }

    private void Digit_KeyDown(object? sender, KeyEventArgs e)
    {
        if (sender is not TextBox box) return;
        int index = Array.IndexOf(_digits, box);
        if (e.Control && e.KeyCode == Keys.V)
        {
            try
            {
                string pasted = Clipboard.GetText().Trim();
                if (pasted.Length == 6 && pasted.All(ch => ch >= '0' && ch <= '9'))
                {
                    _changingOtp = true;
                    for (int i = 0; i < 6; i++) _digits[i].Text = pasted[i].ToString();
                    _changingOtp = false;
                    _digits[5].Focus();
                    UpdateControls();
                    e.SuppressKeyPress = true;
                }
            }
            catch { _changingOtp = false; }
        }
        else if (e.KeyCode == Keys.Back && box.TextLength == 0 && index > 0)
        {
            _digits[index - 1].Focus();
            _digits[index - 1].Clear();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            btnVerifyOtp.PerformClick();
            e.SuppressKeyPress = true;
        }
    }

    private void Digit_TextChanged(object? sender, EventArgs e)
    {
        if (_changingOtp || sender is not TextBox box) return;
        int index = Array.IndexOf(_digits, box);
        if (box.TextLength == 1 && index < 5) _digits[index + 1].Focus();
        UpdateControls();
    }

    private async void btnVerifyOtp_Click(object? sender, EventArgs e)
    {
        if (_busy || !_requestPending) return;
        string otp = string.Concat(_digits.Select(x => x.Text));
        if (otp.Length != 6 || !otp.All(ch => ch >= '0' && ch <= '9'))
        {
            lblOtpStatus.Text = "Vui lòng nhập đủ 6 chữ số.";
            return;
        }
        _busy = true;
        UpdateControls();
        try
        {
            RegisterOTPVerificationResult result = await client.RequestRegisterOTPVerificationAsync(_requestedEmail, otp);
            if (IsDisposed) return;
            if (!result.Success) { lblOtpStatus.Text = result.Message; return; }
            _verifiedEmail = _requestedEmail;
            _requestPending = false;
            actuallyVerified = result.Success;
            _clock.Stop();
            lblOtpStatus.Text = "Email đã xác minh. Nhập mật khẩu và Đăng ký.";
            lblStatus.Text = "";
        }
        catch (Exception ex) { lblOtpStatus.Text = $"Không xác minh được: {ex.Message}"; }
        finally { _busy = false; if (!IsDisposed) UpdateControls(); }
    }

    private async void btnRegister_Click(object? sender, EventArgs e)
    {
        if (_busy) return;
        string username = txtUsername.Text.Trim();
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text;
        if (username.Length is < 3 or > 20) { ShowError("Username phải có 3–20 ký tự."); txtUsername.Focus(); return; }
        if (!IsValidEmail(email)) { ShowError("Email không hợp lệ."); txtEmail.Focus(); return; }
        if ( !actuallyVerified || !email.Equals(_verifiedEmail, StringComparison.OrdinalIgnoreCase))
        { ShowError("Vui lòng xác minh Email trước khi đăng ký."); return; }
        if (password.Length < 6) { ShowError("Mật khẩu phải có tối thiểu 6 ký tự."); txtPassword.Focus(); return; }
        if (password != txtConfirm.Text) { ShowError("Hai mật khẩu chưa trùng khớp."); txtConfirm.Focus(); return; }
        _busy = true;
        UpdateControls();
        try
        {
            var result = await client.RegisterAsync(username, email, password);
            if (IsDisposed) return;
            if (!result.Success)
            {
                ShowError(result.Message);
                if (result.Message.Contains("hết hiệu lực", StringComparison.OrdinalIgnoreCase))
                    ResetVerification();
                return;
            }
            VerifiedUsername = username;
            using var success = new ResultForm(true, "Tài khoản của bạn đã được tạo. Hãy đăng nhập để bắt đầu!", "Đăng ký thành công!");
            success.ShowDialog(this);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex) { ShowError($"Đăng ký thất bại: {ex.Message}"); }
        finally { _busy = false; if (!IsDisposed) UpdateControls(); }
    }

    private void lnkBack_Click(object? sender, LinkLabelLinkClickedEventArgs e) => Close();

    private void lblSubtitle_Click(object sender, EventArgs e)
    {

    }

    private void txtUsername_TextChanged(object sender, EventArgs e)
    {

    }
}
