using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using PentagonLauncher.Launcher.Networking;
using PentagonLauncher.Shared.Models.Authentication;

namespace PentagonLauncher.Launcher.Forms;

public partial class ResetPasswordForm : Form
{
    private readonly LauncherTcpClient client = LauncherTcpClient.Shared;

    private readonly string _email;
    private string _resetToken = "";

    public ResetPasswordForm(string email)
    {
        InitializeComponent();
        AcceptButton = btnReset;
        _email = email;
    }

    public ResetPasswordForm(string email,string resetToken) : this(email) => _resetToken = resetToken;

    private async void btnReset_Click(object? sender, EventArgs e)
    {
        string password = txtPassword.Text;
        if (password.Length < 6) { lblStatus.Text = "Mật khẩu cần tối thiểu 6 ký tự."; return; }
        if (password != txtConfirm.Text) { lblStatus.Text = "Hai mật khẩu không khớp."; return; }
        btnReset.Enabled = false;
        try
        {
            var result = await client.ResetPasswordAsync(_email,_resetToken, password);
            using var dialog = new ResultForm(result.Success, result.Message);
            dialog.ShowDialog(this);
            if (result.Success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else lblStatus.Text = result.Message;
        }
        catch (Exception ex)
        {
            using var dialog = new ResultForm(false, $"Lỗi kết nối hoặc xác thực: {ex.Message}");
            dialog.ShowDialog(this);
            lblStatus.Text = ex.Message;
        }
        finally { if (!IsDisposed) btnReset.Enabled = true; }
    }
}
