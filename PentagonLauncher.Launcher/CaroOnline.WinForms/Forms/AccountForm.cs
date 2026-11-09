using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using CaroOnline.WinForms.Services;

namespace CaroOnline.WinForms.Forms;

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
            AccountProfile? profile = await Program.Auth.GetProfileAsync(_username);
            if (profile is null)
            {
                lblEmail.Text = "Không có dữ liệu";
                lblCreated.Text = "—";
                return;
            }
            lblUsername.Text = profile.Username;
            lblEmail.Text = profile.Email;
            lblCreated.Text = profile.CreatedAt.ToString("dd/MM/yyyy");
        }
        catch (Exception ex) { lblEmail.Text = $"Lỗi tải hồ sơ: {ex.Message}"; }
    }

    private void btnBack_Click(object? sender, EventArgs e) => Close();
}
