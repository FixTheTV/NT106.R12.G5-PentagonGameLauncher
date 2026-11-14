using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using PentagonLauncher.Launcher.Networking;

namespace PentagonLauncher.Launcher.Forms;

public partial class AccountForm : Form
{
    private readonly string _username;

    public AccountForm() : this("demo") { }

    public AccountForm(string username)
    {
        InitializeComponent();
        _username = username;
        lblUsername.Text = username;
        Load += AccountForm_Load;
    }

    private async void AccountForm_Load(object? sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex) { lblEmail.Text = $"Lỗi tải hồ sơ: {ex.Message}"; }
    }

    private void btnBack_Click(object? sender, EventArgs e) => Close();
}
