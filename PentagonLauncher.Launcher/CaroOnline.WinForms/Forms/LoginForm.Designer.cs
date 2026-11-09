using System;
using System.Drawing;
using System.Windows.Forms;
using CaroOnline.WinForms.Controls;

namespace CaroOnline.WinForms.Forms;

public partial class LoginForm
{
    private System.ComponentModel.IContainer? components = null;
    private CaroLogo logoBrand = null!;
    private Label brandName = null!;
    private Label brandSub = null!;
    private GlassPanel cardMain = null!;
    private Label lblStep = null!;
    private Label lblTitle = null!;
    private Label lblSubtitle = null!;
    private Label lblUsername = null!;
    private GlassPanel boxUsername = null!;
    private Label icoUsername = null!;
    private TextBox txtUsername = null!;
    private Label lblPassword = null!;
    private GlassPanel boxPassword = null!;
    private Label icoPassword = null!;
    private TextBox txtPassword = null!;
    private LinkLabel lnkForgot = null!;
    private NeonButton btnLogin = null!;
    private Label lblOr = null!;
    private NeonButton btnRegister = null!;
    private Label lblStatus = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.logoBrand = new CaroLogo();
        this.brandName = new Label();
        this.brandSub = new Label();
        this.cardMain = new GlassPanel();
        this.lblStep = new Label();
        this.lblTitle = new Label();
        this.lblSubtitle = new Label();
        this.lblUsername = new Label();
        this.boxUsername = new GlassPanel();
        this.icoUsername = new Label();
        this.txtUsername = new TextBox();
        this.lblPassword = new Label();
        this.boxPassword = new GlassPanel();
        this.icoPassword = new Label();
        this.txtPassword = new TextBox();
        this.lnkForgot = new LinkLabel();
        this.btnLogin = new NeonButton();
        this.lblOr = new Label();
        this.btnRegister = new NeonButton();
        this.lblStatus = new Label();

        // logoBrand: editable from Visual Studio Designer.
        this.logoBrand.Name = "logoBrand";
        this.logoBrand.Location = new Point(150, 22);
        this.logoBrand.Size = new Size(66, 66);

        // brandName: editable from Visual Studio Designer.
        this.brandName.Name = "brandName";
        this.brandName.Location = new Point(232, 28);
        this.brandName.Size = new Size(365, 35);
        this.brandName.AutoSize = false;
        this.brandName.BackColor = Color.Transparent;
        this.brandName.TabStop = false;
        this.brandName.Text = "Caro Online";
        this.brandName.Font = new Font("Segoe UI", 23F, FontStyle.Bold);
        this.brandName.ForeColor = Color.FromArgb(246, 244, 255);
        this.brandName.TextAlign = ContentAlignment.MiddleLeft;

        // brandSub: editable from Visual Studio Designer.
        this.brandSub.Name = "brandSub";
        this.brandSub.Location = new Point(233, 62);
        this.brandSub.Size = new Size(420, 26);
        this.brandSub.AutoSize = false;
        this.brandSub.BackColor = Color.Transparent;
        this.brandSub.TabStop = false;
        this.brandSub.Text = "Chơi Caro trực tuyến cùng bạn bè";
        this.brandSub.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        this.brandSub.ForeColor = Color.FromArgb(170, 168, 205);
        this.brandSub.TextAlign = ContentAlignment.MiddleLeft;

        // cardMain: editable from Visual Studio Designer.
        this.cardMain.Name = "cardMain";
        this.cardMain.Location = new Point(130, 110);
        this.cardMain.Size = new Size(500, 503);
        this.cardMain.BackColor = Color.FromArgb(27, 27, 66);
        this.cardMain.GradientColor = Color.FromArgb(27, 27, 66);
        this.cardMain.BorderColor = Color.FromArgb(101, 87, 173);
        this.cardMain.CornerRadius = 20;

        // lblStep: editable from Visual Studio Designer.
        this.lblStep.Name = "lblStep";
        this.lblStep.Location = new Point(35, 20);
        this.lblStep.Size = new Size(420, 27);
        this.lblStep.AutoSize = false;
        this.lblStep.BackColor = Color.Transparent;
        this.lblStep.TabStop = false;
        this.lblStep.Text = "01  /  ĐĂNG NHẬP";
        this.lblStep.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblStep.ForeColor = Color.FromArgb(64, 217, 255);
        this.lblStep.TextAlign = ContentAlignment.MiddleLeft;

        // lblTitle: editable from Visual Studio Designer.
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Location = new Point(35, 53);
        this.lblTitle.Size = new Size(440, 49);
        this.lblTitle.AutoSize = false;
        this.lblTitle.BackColor = Color.Transparent;
        this.lblTitle.TabStop = false;
        this.lblTitle.Text = "Chào mừng trở lại";
        this.lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        this.lblTitle.ForeColor = Color.FromArgb(246, 244, 255);
        this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblSubtitle: editable from Visual Studio Designer.
        this.lblSubtitle.Name = "lblSubtitle";
        this.lblSubtitle.Location = new Point(35, 105);
        this.lblSubtitle.Size = new Size(430, 28);
        this.lblSubtitle.AutoSize = false;
        this.lblSubtitle.BackColor = Color.Transparent;
        this.lblSubtitle.TabStop = false;
        this.lblSubtitle.Text = "Sẵn sàng cho một ván cờ mới?";
        this.lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        this.lblSubtitle.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblUsername: editable from Visual Studio Designer.
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Location = new Point(35, 151);
        this.lblUsername.Size = new Size(410, 26);
        this.lblUsername.AutoSize = false;
        this.lblUsername.BackColor = Color.Transparent;
        this.lblUsername.TabStop = false;
        this.lblUsername.Text = "TÊN ĐĂNG NHẬP";
        this.lblUsername.Font = new Font("Segoe UI", 9.3F, FontStyle.Bold);
        this.lblUsername.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblUsername.TextAlign = ContentAlignment.MiddleLeft;

        // boxUsername: editable from Visual Studio Designer.
        this.boxUsername.Name = "boxUsername";
        this.boxUsername.Location = new Point(35, 180);
        this.boxUsername.Size = new Size(430, 51);
        this.boxUsername.BackColor = Color.FromArgb(21, 22, 50);
        this.boxUsername.GradientColor = Color.FromArgb(21, 22, 50);
        this.boxUsername.BorderColor = Color.FromArgb(79, 86, 139);
        this.boxUsername.CornerRadius = 11;

        // icoUsername: editable from Visual Studio Designer.
        this.icoUsername.Name = "icoUsername";
        this.icoUsername.Location = new Point(14, 8);
        this.icoUsername.Size = new Size(30, 32);
        this.icoUsername.AutoSize = false;
        this.icoUsername.BackColor = Color.Transparent;
        this.icoUsername.TabStop = false;
        this.icoUsername.Text = "@";
        this.icoUsername.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        this.icoUsername.ForeColor = Color.FromArgb(64, 217, 255);
        this.icoUsername.TextAlign = ContentAlignment.MiddleCenter;

        // txtUsername: editable from Visual Studio Designer.
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.Location = new Point(54, 15);
        this.txtUsername.Size = new Size(356, 27);
        this.txtUsername.BorderStyle = BorderStyle.None;
        this.txtUsername.BackColor = Color.FromArgb(21, 22, 50);
        this.txtUsername.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtUsername.Font = new Font("Segoe UI", 11F);
        this.txtUsername.TabIndex = 0;
        this.txtUsername.PlaceholderText = "Nhập username";
        this.txtUsername.MaxLength = 20;
        this.txtUsername.TextAlign = HorizontalAlignment.Left;

        // lblPassword: editable from Visual Studio Designer.
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Location = new Point(35, 240);
        this.lblPassword.Size = new Size(410, 26);
        this.lblPassword.AutoSize = false;
        this.lblPassword.BackColor = Color.Transparent;
        this.lblPassword.TabStop = false;
        this.lblPassword.Text = "MẬT KHẨU";
        this.lblPassword.Font = new Font("Segoe UI", 9.3F, FontStyle.Bold);
        this.lblPassword.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblPassword.TextAlign = ContentAlignment.MiddleLeft;

        // boxPassword: editable from Visual Studio Designer.
        this.boxPassword.Name = "boxPassword";
        this.boxPassword.Location = new Point(35, 269);
        this.boxPassword.Size = new Size(430, 51);
        this.boxPassword.BackColor = Color.FromArgb(21, 22, 50);
        this.boxPassword.GradientColor = Color.FromArgb(21, 22, 50);
        this.boxPassword.BorderColor = Color.FromArgb(79, 86, 139);
        this.boxPassword.CornerRadius = 11;

        // icoPassword: editable from Visual Studio Designer.
        this.icoPassword.Name = "icoPassword";
        this.icoPassword.Location = new Point(14, 8);
        this.icoPassword.Size = new Size(30, 32);
        this.icoPassword.AutoSize = false;
        this.icoPassword.BackColor = Color.Transparent;
        this.icoPassword.TabStop = false;
        this.icoPassword.Text = "●";
        this.icoPassword.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        this.icoPassword.ForeColor = Color.FromArgb(64, 217, 255);
        this.icoPassword.TextAlign = ContentAlignment.MiddleCenter;

        // txtPassword: editable from Visual Studio Designer.
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.Location = new Point(54, 15);
        this.txtPassword.Size = new Size(356, 27);
        this.txtPassword.BorderStyle = BorderStyle.None;
        this.txtPassword.BackColor = Color.FromArgb(21, 22, 50);
        this.txtPassword.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtPassword.Font = new Font("Segoe UI", 11F);
        this.txtPassword.TabIndex = 1;
        this.txtPassword.PlaceholderText = "Nhập mật khẩu";
        this.txtPassword.UseSystemPasswordChar = true;
        this.txtPassword.TextAlign = HorizontalAlignment.Left;

        // lnkForgot: editable from Visual Studio Designer.
        this.lnkForgot.Name = "lnkForgot";
        this.lnkForgot.Location = new Point(315, 328);
        this.lnkForgot.Size = new Size(150, 27);
        this.lnkForgot.BackColor = Color.Transparent;
        this.lnkForgot.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.lnkForgot.LinkBehavior = LinkBehavior.HoverUnderline;
        this.lnkForgot.AutoSize = false;
        this.lnkForgot.TextAlign = ContentAlignment.MiddleCenter;
        this.lnkForgot.Text = "Quên mật khẩu?";
        this.lnkForgot.ForeColor = Color.FromArgb(64, 217, 255);
        this.lnkForgot.LinkColor = Color.FromArgb(64, 217, 255);

        // btnLogin: editable from Visual Studio Designer.
        this.btnLogin.Name = "btnLogin";
        this.btnLogin.Location = new Point(35, 369);
        this.btnLogin.Size = new Size(430, 52);
        this.btnLogin.ForeColor = Color.FromArgb(246, 244, 255);
        this.btnLogin.TabIndex = 2;
        this.btnLogin.Text = "Đăng nhập";
        this.btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        this.btnLogin.CornerRadius = 12;
        this.btnLogin.BackColor = Color.FromArgb(49, 190, 235);
        this.btnLogin.GradientColor = Color.FromArgb(146, 86, 237);
        this.btnLogin.BorderColor = Color.FromArgb(173, 141, 255);

        // lblOr: editable from Visual Studio Designer.
        this.lblOr.Name = "lblOr";
        this.lblOr.Location = new Point(35, 445);
        this.lblOr.Size = new Size(236, 23);
        this.lblOr.AutoSize = false;
        this.lblOr.BackColor = Color.Transparent;
        this.lblOr.TabStop = false;
        this.lblOr.Text = "CHƯA CÓ TÀI KHOẢN?";
        this.lblOr.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblOr.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblOr.TextAlign = ContentAlignment.MiddleLeft;

        // btnRegister: editable from Visual Studio Designer.
        this.btnRegister.Name = "btnRegister";
        this.btnRegister.Location = new Point(267, 438);
        this.btnRegister.Size = new Size(198, 42);
        this.btnRegister.ForeColor = Color.FromArgb(246, 244, 255);
        this.btnRegister.TabIndex = 3;
        this.btnRegister.Text = "Đăng ký ngay";
        this.btnRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.btnRegister.CornerRadius = 12;
        this.btnRegister.BackColor = Color.FromArgb(49, 39, 93);
        this.btnRegister.GradientColor = Color.FromArgb(33, 29, 72);
        this.btnRegister.BorderColor = Color.FromArgb(101, 89, 158);

        // lblStatus: editable from Visual Studio Designer.
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Location = new Point(133, 625);
        this.lblStatus.Size = new Size(496, 27);
        this.lblStatus.AutoSize = false;
        this.lblStatus.BackColor = Color.Transparent;
        this.lblStatus.TabStop = false;
        this.lblStatus.Text = "";
        this.lblStatus.Font = new Font("Segoe UI", 9.3F, FontStyle.Regular);
        this.lblStatus.ForeColor = Color.FromArgb(241, 142, 223);
        this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;

        this.lnkForgot.Click += this.lnkForgot_Click;
        this.btnLogin.Click += this.btnLogin_Click;
        this.btnRegister.Click += this.btnRegister_Click;

        this.Controls.Add(this.logoBrand);
        this.Controls.Add(this.brandName);
        this.Controls.Add(this.brandSub);
        this.Controls.Add(this.cardMain);
        this.cardMain.Controls.Add(this.lblStep);
        this.cardMain.Controls.Add(this.lblTitle);
        this.cardMain.Controls.Add(this.lblSubtitle);
        this.cardMain.Controls.Add(this.lblUsername);
        this.cardMain.Controls.Add(this.boxUsername);
        this.boxUsername.Controls.Add(this.icoUsername);
        this.boxUsername.Controls.Add(this.txtUsername);
        this.cardMain.Controls.Add(this.lblPassword);
        this.cardMain.Controls.Add(this.boxPassword);
        this.boxPassword.Controls.Add(this.icoPassword);
        this.boxPassword.Controls.Add(this.txtPassword);
        this.cardMain.Controls.Add(this.lnkForgot);
        this.cardMain.Controls.Add(this.btnLogin);
        this.cardMain.Controls.Add(this.lblOr);
        this.cardMain.Controls.Add(this.btnRegister);
        this.Controls.Add(this.lblStatus);

        this.AutoScaleDimensions = new SizeF(96F, 96F);
        this.AutoScaleMode = AutoScaleMode.Dpi;
        this.ClientSize = new Size(760, 665);
        this.BackColor = Color.FromArgb(16, 16, 44);
        this.Text = "Caro Online  ·  Đăng nhập";
        this.Font = new Font("Segoe UI", 10F);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = true;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ResumeLayout(false);
    }
}
