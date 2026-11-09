using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
namespace CaroOnline.WinForms.Forms;

public partial class LauncherForm : Form
{
    private readonly string _username;

    public LauncherForm() : this("demo") { }

    public LauncherForm(string username)
    {
        InitializeComponent();
        _username = username;
        lblUsername.Text = $"Xin chào, {_username}";
    }

    private void btnHome_Click(object? sender, EventArgs e) =>
        MessageBox.Show(this, "Bạn đang ở trang chủ Launcher.", "Caro Online");

    private void btnQuick_Click(object? sender, EventArgs e) =>
        MessageBox.Show(this, "Giao diện Chơi nhanh sẽ kết nối module gameplay của nhóm.", "Chơi nhanh");

    private void btnRooms_Click(object? sender, EventArgs e) =>
        MessageBox.Show(this, "Giao diện Phòng chơi sẽ kết nối module phòng của nhóm.", "Phòng chơi");

    private void btnLeaderboard_Click(object? sender, EventArgs e) =>
        MessageBox.Show(this, "Bảng xếp hạng sẽ được bổ sung khi có API gameplay.", "Xếp hạng");

    private void btnAccount_Click(object? sender, EventArgs e)
    {
        using var account = new AccountForm(_username);
        account.ShowDialog(this);
    }

    private void btnLogout_Click(object? sender, EventArgs e) => Close();
}
