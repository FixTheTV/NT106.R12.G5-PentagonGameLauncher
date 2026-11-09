using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using PentagonLauncher.Launcher.Services;

namespace PentagonLauncher.Launcher.Forms;

public partial class OtpForm : Form
{
    private string _email = "";
    private DateTime _expiresAt;
    private DateTime _resendAvailableAt;
    private readonly System.Windows.Forms.Timer _clock = new() { Interval = 1000 };
    private TextBox[] _otpBoxes = Array.Empty<TextBox>();
    private bool _updatingBoxes;

    // Constructor không tham số để Visual Studio Designer mở form bình thường.
    public OtpForm()
    {
        InitializeComponent();
        _otpBoxes = [txtOtp1, txtOtp2, txtOtp3, txtOtp4, txtOtp5, txtOtp6];
        foreach (var box in _otpBoxes)
        {
            box.TextAlign = HorizontalAlignment.Center;
            box.KeyPress += OtpBox_KeyPress;
            box.KeyDown += OtpBox_KeyDown;
            box.TextChanged += OtpBox_TextChanged;
        }
        _clock.Tick += Clock_Tick;
        FormClosed += (_, _) => _clock.Dispose();
        AcceptButton = btnVerify;
    }

    public OtpForm(string email) : this()
    {
        _email = email;
        lblEmail.Text = email;
        RestartCountdown();
        _otpBoxes[0].Focus();
        _clock.Start();
    }

    private void RestartCountdown()
    {
        _expiresAt = DateTime.UtcNow.AddMinutes(5);
        _resendAvailableAt = DateTime.UtcNow.AddSeconds(60);
        Clock_Tick(this, EventArgs.Empty);
    }

    private void Clock_Tick(object? sender, EventArgs e)
    {
        TimeSpan remain = _expiresAt - DateTime.UtcNow;
        if (remain < TimeSpan.Zero) remain = TimeSpan.Zero;
        lblCountdown.Text = $"⏱ Còn lại: {remain.Minutes:00}:{remain.Seconds:00}";
        btnVerify.Enabled = remain > TimeSpan.Zero;

        int resendSeconds = Math.Max(0, (int)Math.Ceiling((_resendAvailableAt - DateTime.UtcNow).TotalSeconds));
        btnResend.Enabled = resendSeconds == 0;
        btnResend.Text = resendSeconds > 0 ? $"Gửi lại mã (sau {resendSeconds}s)" : "Gửi lại mã";
        if (remain == TimeSpan.Zero)
            lblStatus.Text = "Mã đã hết hạn. Hãy bấm Gửi lại mã.";
    }

    private static void OtpBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
    }

    private void OtpBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (sender is not TextBox box) return;
        int pos = Array.IndexOf(_otpBoxes, box);
        if (e.KeyCode == Keys.Back && box.Text.Length == 0 && pos > 0)
        {
            _otpBoxes[pos - 1].Focus();
            _otpBoxes[pos - 1].Clear();
        }
        else if (e.Control && e.KeyCode == Keys.V)
        {
            // Hỗ trợ dán OTP bằng Ctrl+V; phần Clipboard chỉ chạy trong ứng dụng Windows.
            try
            {
                string content = Clipboard.GetText().Trim();
                if (content.Length == 6 && content.All(char.IsDigit))
                {
                    _updatingBoxes = true;
                    for (int i = 0; i < 6; i++) _otpBoxes[i].Text = content[i].ToString();
                    _updatingBoxes = false;
                    _otpBoxes[5].Focus();
                    e.SuppressKeyPress = true;
                }
            }
            catch { _updatingBoxes = false; }
        }
    }

    private void OtpBox_TextChanged(object? sender, EventArgs e)
    {
        if (_updatingBoxes || sender is not TextBox box) return;
        int pos = Array.IndexOf(_otpBoxes, box);
        if (box.Text.Length > 0 && pos < _otpBoxes.Length - 1)
            _otpBoxes[pos + 1].Focus();
    }

    private async void btnVerify_Click(object? sender, EventArgs e)
    {
        string code = string.Concat(_otpBoxes.Select(x => x.Text));
        if (code.Length != 6 || !code.All(char.IsDigit))
        {
            lblStatus.Text = "Vui lòng nhập đủ 6 chữ số.";
            return;
        }
        btnVerify.Enabled = false;
        try
        {
            OtpVerificationResult verify = await Program.Auth.VerifyOtpAsync(_email, code);
            if (!verify.Success) { lblStatus.Text = verify.Message; return; }
            _clock.Stop();
            using var reset = new ResetPasswordForm(verify.ResetToken);
            Hide();
            if (reset.ShowDialog(this) == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            // OTP đã xác minh chỉ dùng một lần: nếu đóng form đặt lại,
            // người dùng phải khởi tạo lại yêu cầu để lấy OTP khác.
            DialogResult = DialogResult.Cancel;
            Close();
        }
        catch (Exception ex) { lblStatus.Text = $"Xác thực thất bại: {ex.Message}"; }
        finally { if (!IsDisposed) Clock_Tick(this, EventArgs.Empty); }
    }

    private async void btnResend_Click(object? sender, EventArgs e)
    {
        btnResend.Enabled = false;
        try
        {
            var result = await Program.Auth.RequestPasswordResetAsync(_email);
            if (!result.Success) { lblStatus.Text = result.Message; return; }
            foreach (var box in _otpBoxes) box.Clear();
            _otpBoxes[0].Focus();
            lblStatus.Text = "Mã demo: 123456 (chỉ để thử giao diện).";
            RestartCountdown();
        }
        catch (Exception ex) { lblStatus.Text = $"Không gửi lại được mã: {ex.Message}"; }
    }
}
