using System;
using System.Drawing;
using System.Windows.Forms;
using CaroOnline.WinForms.Controls;

namespace CaroOnline.WinForms.Forms;

public partial class ForgotPasswordForm
{
    private System.ComponentModel.IContainer? components = null;
    private CaroLogo logoBrand = null!;
    private Label brandName = null!;
    private Label brandSub = null!;
    private GlassPanel cardMain = null!;
    private Label lblStep = null!;
    private Label lblTitle = null!;
    private Label lblSubtitle = null!;
    private Label lblEmail = null!;
    private GlassPanel boxEmail = null!;
    private Label icoEmail = null!;
    private TextBox txtEmail = null!;
    private NeonButton btnSend = null!;
    private NeonButton btnBack = null!;
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
        this.lblEmail = new Label();
        this.boxEmail = new GlassPanel();
        this.icoEmail = new Label();
        this.txtEmail = new TextBox();
        this.btnSend = new NeonButton();
        this.btnBack = new NeonButton();
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
        this.cardMain.Size = new Size(500, 465);
        this.cardMain.BackColor = Color.FromArgb(27, 27, 66);
        this.cardMain.GradientColor = Color.FromArgb(27, 27, 66);
        this.cardMain.BorderColor = Color.FromArgb(101, 87, 173);
        this.cardMain.CornerRadius = 20;

        // lblStep: editable from Visual Studio Designer.
        this.lblStep.Name = "lblStep";
        this.lblStep.Location = new Point(35, 28);
        this.lblStep.Size = new Size(430, 26);
        this.lblStep.AutoSize = false;
        this.lblStep.BackColor = Color.Transparent;
        this.lblStep.TabStop = false;
        this.lblStep.Text = "01  /  03    KHÔI PHỤC TÀI KHOẢN";
        this.lblStep.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblStep.ForeColor = Color.FromArgb(64, 217, 255);
        this.lblStep.TextAlign = ContentAlignment.MiddleLeft;

        // lblTitle: editable from Visual Studio Designer.
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Location = new Point(35, 74);
        this.lblTitle.Size = new Size(430, 50);
        this.lblTitle.AutoSize = false;
        this.lblTitle.BackColor = Color.Transparent;
        this.lblTitle.TabStop = false;
        this.lblTitle.Text = "Quên mật khẩu?";
        this.lblTitle.Font = new Font("Segoe UI", 23F, FontStyle.Bold);
        this.lblTitle.ForeColor = Color.FromArgb(246, 244, 255);
        this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblSubtitle: editable from Visual Studio Designer.
        this.lblSubtitle.Name = "lblSubtitle";
        this.lblSubtitle.Location = new Point(35, 136);
        this.lblSubtitle.Size = new Size(430, 53);
        this.lblSubtitle.AutoSize = false;
        this.lblSubtitle.BackColor = Color.Transparent;
        this.lblSubtitle.TabStop = false;
        this.lblSubtitle.Text = "Nhập email đã đăng ký để nhận mã xác nhận.";
        this.lblSubtitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
        this.lblSubtitle.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblEmail: editable from Visual Studio Designer.
        this.lblEmail.Name = "lblEmail";
        this.lblEmail.Location = new Point(35, 224);
        this.lblEmail.Size = new Size(410, 26);
        this.lblEmail.AutoSize = false;
        this.lblEmail.BackColor = Color.Transparent;
        this.lblEmail.TabStop = false;
        this.lblEmail.Text = "ĐỊA CHỈ EMAIL";
        this.lblEmail.Font = new Font("Segoe UI", 9.3F, FontStyle.Bold);
        this.lblEmail.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblEmail.TextAlign = ContentAlignment.MiddleLeft;

        // boxEmail: editable from Visual Studio Designer.
        this.boxEmail.Name = "boxEmail";
        this.boxEmail.Location = new Point(35, 253);
        this.boxEmail.Size = new Size(430, 51);
        this.boxEmail.BackColor = Color.FromArgb(21, 22, 50);
        this.boxEmail.GradientColor = Color.FromArgb(21, 22, 50);
        this.boxEmail.BorderColor = Color.FromArgb(79, 86, 139);
        this.boxEmail.CornerRadius = 11;

        // icoEmail: editable from Visual Studio Designer.
        this.icoEmail.Name = "icoEmail";
        this.icoEmail.Location = new Point(14, 8);
        this.icoEmail.Size = new Size(30, 32);
        this.icoEmail.AutoSize = false;
        this.icoEmail.BackColor = Color.Transparent;
        this.icoEmail.TabStop = false;
        this.icoEmail.Text = "✉";
        this.icoEmail.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        this.icoEmail.ForeColor = Color.FromArgb(64, 217, 255);
        this.icoEmail.TextAlign = ContentAlignment.MiddleCenter;

        // txtEmail: editable from Visual Studio Designer.
        this.txtEmail.Name = "txtEmail";
        this.txtEmail.Location = new Point(54, 15);
        this.txtEmail.Size = new Size(356, 27);
        this.txtEmail.BorderStyle = BorderStyle.None;
        this.txtEmail.BackColor = Color.FromArgb(21, 22, 50);
        this.txtEmail.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtEmail.Font = new Font("Segoe UI", 11F);
        this.txtEmail.TabIndex = 0;
        this.txtEmail.PlaceholderText = "email@example.com";
        this.txtEmail.MaxLength = 254;
        this.txtEmail.TextAlign = HorizontalAlignment.Left;

        // btnSend: editable from Visual Studio Designer.
        this.btnSend.Name = "btnSend";
        this.btnSend.Location = new Point(35, 335);
        this.btnSend.Size = new Size(430, 53);
        this.btnSend.ForeColor = Color.FromArgb(246, 244, 255);
        this.btnSend.TabIndex = 1;
        this.btnSend.Text = "Gửi mã xác nhận";
        this.btnSend.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        this.btnSend.CornerRadius = 12;
        this.btnSend.BackColor = Color.FromArgb(49, 190, 235);
        this.btnSend.GradientColor = Color.FromArgb(146, 86, 237);
        this.btnSend.BorderColor = Color.FromArgb(173, 141, 255);

        // btnBack: editable from Visual Studio Designer.
        this.btnBack.Name = "btnBack";
        this.btnBack.Location = new Point(130, 402);
        this.btnBack.Size = new Size(240, 40);
        this.btnBack.ForeColor = Color.FromArgb(246, 244, 255);
        this.btnBack.TabIndex = 2;
        this.btnBack.Text = "← Quay lại đăng nhập";
        this.btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.btnBack.CornerRadius = 12;
        this.btnBack.BackColor = Color.FromArgb(49, 39, 93);
        this.btnBack.GradientColor = Color.FromArgb(33, 29, 72);
        this.btnBack.BorderColor = Color.FromArgb(101, 89, 158);

        // lblStatus: editable from Visual Studio Designer.
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Location = new Point(133, 583);
        this.lblStatus.Size = new Size(496, 27);
        this.lblStatus.AutoSize = false;
        this.lblStatus.BackColor = Color.Transparent;
        this.lblStatus.TabStop = false;
        this.lblStatus.Text = "";
        this.lblStatus.Font = new Font("Segoe UI", 9.3F, FontStyle.Regular);
        this.lblStatus.ForeColor = Color.FromArgb(241, 142, 223);
        this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;

        this.btnSend.Click += this.btnSend_Click;
        this.btnBack.Click += this.btnBack_Click;

        this.Controls.Add(this.logoBrand);
        this.Controls.Add(this.brandName);
        this.Controls.Add(this.brandSub);
        this.Controls.Add(this.cardMain);
        this.cardMain.Controls.Add(this.lblStep);
        this.cardMain.Controls.Add(this.lblTitle);
        this.cardMain.Controls.Add(this.lblSubtitle);
        this.cardMain.Controls.Add(this.lblEmail);
        this.cardMain.Controls.Add(this.boxEmail);
        this.boxEmail.Controls.Add(this.icoEmail);
        this.boxEmail.Controls.Add(this.txtEmail);
        this.cardMain.Controls.Add(this.btnSend);
        this.cardMain.Controls.Add(this.btnBack);
        this.Controls.Add(this.lblStatus);

        this.AutoScaleDimensions = new SizeF(96F, 96F);
        this.AutoScaleMode = AutoScaleMode.Dpi;
        this.ClientSize = new Size(760, 615);
        this.BackColor = Color.FromArgb(16, 16, 44);
        this.Text = "Caro Online  ·  Quên mật khẩu";
        this.Font = new Font("Segoe UI", 10F);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = true;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ResumeLayout(false);
    }
}
