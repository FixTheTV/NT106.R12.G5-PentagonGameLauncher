using System;
using System.Drawing;
using System.Windows.Forms;
using PentagonLauncher.Launcher.Controls;

namespace PentagonLauncher.Launcher.Forms;

public partial class ResetPasswordForm
{
    private System.ComponentModel.IContainer components = null;
    private CaroLogo logoBrand = null!;
    private Label brandName = null!;
    private Label brandSub = null!;
    private GlassPanel cardMain = null!;
    private Label lblStep = null!;
    private Label lblTitle = null!;
    private Label lblSubtitle = null!;
    private Label lblPassword = null!;
    private GlassPanel boxPassword = null!;
    private Label icoPassword = null!;
    private TextBox txtPassword = null!;
    private Label lblConfirm = null!;
    private GlassPanel boxConfirm = null!;
    private Label icoConfirm = null!;
    private TextBox txtConfirm = null!;
    private NeonButton btnReset = null!;
    private Label lblInfo = null!;
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
        this.lblPassword = new Label();
        this.boxPassword = new GlassPanel();
        this.icoPassword = new Label();
        this.txtPassword = new TextBox();
        this.lblConfirm = new Label();
        this.boxConfirm = new GlassPanel();
        this.icoConfirm = new Label();
        this.txtConfirm = new TextBox();
        this.btnReset = new NeonButton();
        this.lblInfo = new Label();
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
        this.cardMain.Size = new Size(500, 506);
        this.cardMain.BackColor = Color.FromArgb(27, 27, 66);
        this.cardMain.GradientColor = Color.FromArgb(27, 27, 66);
        this.cardMain.BorderColor = Color.FromArgb(101, 87, 173);
        this.cardMain.CornerRadius = 20;

        // lblStep: editable from Visual Studio Designer.
        this.lblStep.Name = "lblStep";
        this.lblStep.Location = new Point(35, 22);
        this.lblStep.Size = new Size(430, 26);
        this.lblStep.AutoSize = false;
        this.lblStep.BackColor = Color.Transparent;
        this.lblStep.TabStop = false;
        this.lblStep.Text = "03  /  03    ĐẶT LẠI MẬT KHẨU";
        this.lblStep.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblStep.ForeColor = Color.FromArgb(64, 217, 255);
        this.lblStep.TextAlign = ContentAlignment.MiddleLeft;

        // lblTitle: editable from Visual Studio Designer.
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Location = new Point(35, 64);
        this.lblTitle.Size = new Size(430, 45);
        this.lblTitle.AutoSize = false;
        this.lblTitle.BackColor = Color.Transparent;
        this.lblTitle.TabStop = false;
        this.lblTitle.Text = "Mật khẩu mới";
        this.lblTitle.Font = new Font("Segoe UI", 23F, FontStyle.Bold);
        this.lblTitle.ForeColor = Color.FromArgb(246, 244, 255);
        this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblSubtitle: editable from Visual Studio Designer.
        this.lblSubtitle.Name = "lblSubtitle";
        this.lblSubtitle.Location = new Point(35, 116);
        this.lblSubtitle.Size = new Size(430, 32);
        this.lblSubtitle.AutoSize = false;
        this.lblSubtitle.BackColor = Color.Transparent;
        this.lblSubtitle.TabStop = false;
        this.lblSubtitle.Text = "Hãy sử dụng mật khẩu từ 6 ký tự trở lên.";
        this.lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        this.lblSubtitle.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblPassword: editable from Visual Studio Designer.
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Location = new Point(35, 180);
        this.lblPassword.Size = new Size(410, 26);
        this.lblPassword.AutoSize = false;
        this.lblPassword.BackColor = Color.Transparent;
        this.lblPassword.TabStop = false;
        this.lblPassword.Text = "MẬT KHẨU MỚI";
        this.lblPassword.Font = new Font("Segoe UI", 9.3F, FontStyle.Bold);
        this.lblPassword.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblPassword.TextAlign = ContentAlignment.MiddleLeft;

        // boxPassword: editable from Visual Studio Designer.
        this.boxPassword.Name = "boxPassword";
        this.boxPassword.Location = new Point(35, 209);
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
        this.txtPassword.TabIndex = 0;
        this.txtPassword.PlaceholderText = "Nhập mật khẩu mới";
        this.txtPassword.UseSystemPasswordChar = true;
        this.txtPassword.TextAlign = HorizontalAlignment.Left;

        // lblConfirm: editable from Visual Studio Designer.
        this.lblConfirm.Name = "lblConfirm";
        this.lblConfirm.Location = new Point(35, 272);
        this.lblConfirm.Size = new Size(410, 26);
        this.lblConfirm.AutoSize = false;
        this.lblConfirm.BackColor = Color.Transparent;
        this.lblConfirm.TabStop = false;
        this.lblConfirm.Text = "NHẬP LẠI MẬT KHẨU";
        this.lblConfirm.Font = new Font("Segoe UI", 9.3F, FontStyle.Bold);
        this.lblConfirm.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblConfirm.TextAlign = ContentAlignment.MiddleLeft;

        // boxConfirm: editable from Visual Studio Designer.
        this.boxConfirm.Name = "boxConfirm";
        this.boxConfirm.Location = new Point(35, 301);
        this.boxConfirm.Size = new Size(430, 51);
        this.boxConfirm.BackColor = Color.FromArgb(21, 22, 50);
        this.boxConfirm.GradientColor = Color.FromArgb(21, 22, 50);
        this.boxConfirm.BorderColor = Color.FromArgb(79, 86, 139);
        this.boxConfirm.CornerRadius = 11;

        // icoConfirm: editable from Visual Studio Designer.
        this.icoConfirm.Name = "icoConfirm";
        this.icoConfirm.Location = new Point(14, 8);
        this.icoConfirm.Size = new Size(30, 32);
        this.icoConfirm.AutoSize = false;
        this.icoConfirm.BackColor = Color.Transparent;
        this.icoConfirm.TabStop = false;
        this.icoConfirm.Text = "✓";
        this.icoConfirm.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        this.icoConfirm.ForeColor = Color.FromArgb(64, 217, 255);
        this.icoConfirm.TextAlign = ContentAlignment.MiddleCenter;

        // txtConfirm: editable from Visual Studio Designer.
        this.txtConfirm.Name = "txtConfirm";
        this.txtConfirm.Location = new Point(54, 15);
        this.txtConfirm.Size = new Size(356, 27);
        this.txtConfirm.BorderStyle = BorderStyle.None;
        this.txtConfirm.BackColor = Color.FromArgb(21, 22, 50);
        this.txtConfirm.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtConfirm.Font = new Font("Segoe UI", 11F);
        this.txtConfirm.TabIndex = 1;
        this.txtConfirm.PlaceholderText = "Nhập lại mật khẩu mới";
        this.txtConfirm.UseSystemPasswordChar = true;
        this.txtConfirm.TextAlign = HorizontalAlignment.Left;

        // btnReset: editable from Visual Studio Designer.
        this.btnReset.Name = "btnReset";
        this.btnReset.Location = new Point(35, 391);
        this.btnReset.Size = new Size(430, 52);
        this.btnReset.ForeColor = Color.FromArgb(246, 244, 255);
        this.btnReset.TabIndex = 2;
        this.btnReset.Text = "Đặt lại mật khẩu";
        this.btnReset.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        this.btnReset.CornerRadius = 12;
        this.btnReset.BackColor = Color.FromArgb(49, 190, 235);
        this.btnReset.GradientColor = Color.FromArgb(146, 86, 237);
        this.btnReset.BorderColor = Color.FromArgb(173, 141, 255);

        // lblInfo: editable from Visual Studio Designer.
        this.lblInfo.Name = "lblInfo";
        this.lblInfo.Location = new Point(35, 455);
        this.lblInfo.Size = new Size(430, 26);
        this.lblInfo.AutoSize = false;
        this.lblInfo.BackColor = Color.Transparent;
        this.lblInfo.TabStop = false;
        this.lblInfo.Text = "Bảo mật: mã OTP sẽ không thể tái sử dụng.";
        this.lblInfo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        this.lblInfo.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblInfo.TextAlign = ContentAlignment.MiddleLeft;

        // lblStatus: editable from Visual Studio Designer.
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Location = new Point(133, 627);
        this.lblStatus.Size = new Size(496, 27);
        this.lblStatus.AutoSize = false;
        this.lblStatus.BackColor = Color.Transparent;
        this.lblStatus.TabStop = false;
        this.lblStatus.Text = "";
        this.lblStatus.Font = new Font("Segoe UI", 9.3F, FontStyle.Regular);
        this.lblStatus.ForeColor = Color.FromArgb(241, 142, 223);
        this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;

        this.btnReset.Click += this.btnReset_Click;

        this.Controls.Add(this.logoBrand);
        this.Controls.Add(this.brandName);
        this.Controls.Add(this.brandSub);
        this.Controls.Add(this.cardMain);
        this.cardMain.Controls.Add(this.lblStep);
        this.cardMain.Controls.Add(this.lblTitle);
        this.cardMain.Controls.Add(this.lblSubtitle);
        this.cardMain.Controls.Add(this.lblPassword);
        this.cardMain.Controls.Add(this.boxPassword);
        this.boxPassword.Controls.Add(this.icoPassword);
        this.boxPassword.Controls.Add(this.txtPassword);
        this.cardMain.Controls.Add(this.lblConfirm);
        this.cardMain.Controls.Add(this.boxConfirm);
        this.boxConfirm.Controls.Add(this.icoConfirm);
        this.boxConfirm.Controls.Add(this.txtConfirm);
        this.cardMain.Controls.Add(this.btnReset);
        this.cardMain.Controls.Add(this.lblInfo);
        this.Controls.Add(this.lblStatus);

        this.AutoScaleDimensions = new SizeF(96F, 96F);
        this.AutoScaleMode = AutoScaleMode.Dpi;
        this.ClientSize = new Size(760, 660);
        this.BackColor = Color.FromArgb(16, 16, 44);
        this.Text = "Caro Online  ·  Đặt lại mật khẩu";
        this.Font = new Font("Segoe UI", 10F);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = true;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ResumeLayout(false);
    }
}
