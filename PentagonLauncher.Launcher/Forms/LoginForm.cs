using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using PentagonLauncher.Launcher.Services;

namespace PentagonLauncher.Launcher.Forms {

    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
        }

        private async void btnLogin_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            {
                lblStatus.Text = "Vui lòng nhập Username và mật khẩu.";
                return;
            }

            btnLogin.Enabled = false;
            lblStatus.Text = "Đang đăng nhập...";
            try
            {
                AuthResult result = await Program.Auth.LoginAsync(username, password);
                if (!result.Success)
                {
                    lblStatus.Text = result.Message;
                    return;
                }

                lblStatus.Text = "Đăng nhập thành công.";
                txtPassword.Clear();
                // Username dùng giá trị Backend trả về, không dùng trực tiếp ô input.
                using var launcher = new LauncherForm(result.Username);
                Hide();
                launcher.ShowDialog(this);
                Show();
                txtUsername.Focus();
                lblStatus.Text = "Bạn đã đăng xuất.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Không thể đăng nhập: {ex.Message}";
            }
            finally { btnLogin.Enabled = true; }
        }

        private void btnRegister_Click(object? sender, EventArgs e)
        {
            using var form = new RegisterForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                txtUsername.Text = form.VerifiedUsername;
                lblStatus.Text = "Email đã xác minh. Đăng nhập để bắt đầu!";
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void lnkForgot_Click(object? sender, EventArgs e)
        {
            using var form = new ForgotPasswordForm();
            Hide();
            form.ShowDialog(this);
            Show();
        }
    }
}


