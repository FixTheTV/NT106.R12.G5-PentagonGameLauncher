using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
namespace CaroOnline.WinForms.Forms;

public partial class ResultForm : Form
{
    public ResultForm() : this(true, "Mật khẩu đã được cập nhật.") { }

    public ResultForm(bool success, string message, string? customHeadline = null)
    {
        InitializeComponent();
        lblIcon.Text = success ? "✓" : "✕";
        lblIcon.ForeColor = success
            ? Color.FromArgb(106, 238, 176)
            : Color.FromArgb(255, 105, 135);
        lblHeadline.Text = customHeadline ?? (success
            ? "Đặt lại mật khẩu thành công!" : "Đặt lại mật khẩu thất bại");
        lblMessage.Text = message;
        iconPanel.BackColor = success ? Color.FromArgb(28, 61, 64) : Color.FromArgb(71, 36, 61);
        iconPanel.GradientColor = success ? Color.FromArgb(22, 44, 58) : Color.FromArgb(44, 27, 49);
        iconPanel.BorderColor = success ? Color.FromArgb(72, 147, 121) : Color.FromArgb(173, 69, 98);
        btnAction.Text = success ? "Đăng nhập ngay" : "Thử lại";
        AcceptButton = btnAction;
    }

    private void btnAction_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }
}
