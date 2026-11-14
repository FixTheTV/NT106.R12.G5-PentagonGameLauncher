using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using PentagonLauncher.Launcher.Networking;
using PentagonLauncher.Shared.Models;
using PentagonLauncher.Shared.Models.Authentication;

namespace PentagonLauncher.Launcher.Forms;

public partial class ForgotPasswordForm : Form
{
    private readonly LauncherTcpClient client = LauncherTcpClient.Shared;
    public ForgotPasswordForm()
    {
        InitializeComponent();
        AcceptButton = btnSend;
    }

    private async void btnSend_Click(object? sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        if (!MailAddress.TryCreate(email, out var addr) || addr.Address != email)
        {
            lblStatus.Text = "Vui lòng nhập Email đúng định dạng.";
            return;
        }
        btnSend.Enabled = false;
        try
        {
            var result = await client.RequestPasswordResetAsync(email);
            if (!result.Success) { lblStatus.Text = result.Message; return; }
            using var otp = new OtpForm(email);
            Hide();
            if (otp.ShowDialog(this) == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            Show();
        }
        catch (Exception ex) { lblStatus.Text = $"Không gửi được mã: {ex.Message}"; }
        finally { btnSend.Enabled = true; }
    }

    private void btnBack_Click(object? sender, EventArgs e) => Close();
}
