using System;
using System.Drawing;
using System.Windows.Forms;
using PentagonLauncher.Launcher.Controls;

namespace PentagonLauncher.Launcher.Forms;

public partial class OtpForm
{
    private System.ComponentModel.IContainer components = null;
    private CaroLogo logoBrand = null!;
    private Label brandName = null!;
    private Label brandSub = null!;
    private GlassPanel cardMain = null!;
    private Label lblStep = null!;
    private Label lblTitle = null!;
    private Label lblSubtitle = null!;
    private Label lblDescription = null!;
    private Label lblEmail = null!;
    private GlassPanel otpBox1 = null!;
    private TextBox txtOtp1 = null!;
    private GlassPanel otpBox2 = null!;
    private TextBox txtOtp2 = null!;
    private GlassPanel otpBox3 = null!;
    private TextBox txtOtp3 = null!;
    private GlassPanel otpBox4 = null!;
    private TextBox txtOtp4 = null!;
    private GlassPanel otpBox5 = null!;
    private TextBox txtOtp5 = null!;
    private GlassPanel otpBox6 = null!;
    private TextBox txtOtp6 = null!;
    private Label lblCountdown = null!;
    private NeonButton btnVerify = null!;
    private NeonButton btnResend = null!;
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
        this.lblDescription = new Label();
        this.lblEmail = new Label();
        this.otpBox1 = new GlassPanel();
        this.txtOtp1 = new TextBox();
        this.otpBox2 = new GlassPanel();
        this.txtOtp2 = new TextBox();
        this.otpBox3 = new GlassPanel();
        this.txtOtp3 = new TextBox();
        this.otpBox4 = new GlassPanel();
        this.txtOtp4 = new TextBox();
        this.otpBox5 = new GlassPanel();
        this.txtOtp5 = new TextBox();
        this.otpBox6 = new GlassPanel();
        this.txtOtp6 = new TextBox();
        this.lblCountdown = new Label();
        this.btnVerify = new NeonButton();
        this.btnResend = new NeonButton();
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
        this.lblStep.Size = new Size(430, 28);
        this.lblStep.AutoSize = false;
        this.lblStep.BackColor = Color.Transparent;
        this.lblStep.TabStop = false;
        this.lblStep.Text = "02  /  03    XÁC MINH EMAIL";
        this.lblStep.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblStep.ForeColor = Color.FromArgb(64, 217, 255);
        this.lblStep.TextAlign = ContentAlignment.MiddleLeft;

        // lblTitle: editable from Visual Studio Designer.
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Location = new Point(35, 61);
        this.lblTitle.Size = new Size(430, 44);
        this.lblTitle.AutoSize = false;
        this.lblTitle.BackColor = Color.Transparent;
        this.lblTitle.TabStop = false;
        this.lblTitle.Text = "Nhập mã xác nhận";
        this.lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        this.lblTitle.ForeColor = Color.FromArgb(246, 244, 255);
        this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblSubtitle: editable from Visual Studio Designer.
        this.lblSubtitle.Name = "lblSubtitle";
        this.lblSubtitle.Location = new Point(35, 111);
        this.lblSubtitle.Size = new Size(430, 28);
        this.lblSubtitle.AutoSize = false;
        this.lblSubtitle.BackColor = Color.Transparent;
        this.lblSubtitle.TabStop = false;
        this.lblSubtitle.Text = "Mã OTP gồm 6 số, có hiệu lực trong 5 phút.";
        this.lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        this.lblSubtitle.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;

        // lblDescription: editable from Visual Studio Designer.
        this.lblDescription.Name = "lblDescription";
        this.lblDescription.Location = new Point(35, 154);
        this.lblDescription.Size = new Size(155, 22);
        this.lblDescription.AutoSize = false;
        this.lblDescription.BackColor = Color.Transparent;
        this.lblDescription.TabStop = false;
        this.lblDescription.Text = "ĐÃ GỬI MÃ ĐẾN";
        this.lblDescription.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblDescription.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblDescription.TextAlign = ContentAlignment.MiddleLeft;

        // lblEmail: editable from Visual Studio Designer.
        this.lblEmail.Name = "lblEmail";
        this.lblEmail.Location = new Point(186, 150);
        this.lblEmail.Size = new Size(278, 31);
        this.lblEmail.AutoSize = false;
        this.lblEmail.BackColor = Color.Transparent;
        this.lblEmail.TabStop = false;
        this.lblEmail.Text = "email@example.com";
        this.lblEmail.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
        this.lblEmail.ForeColor = Color.FromArgb(64, 217, 255);
        this.lblEmail.TextAlign = ContentAlignment.MiddleLeft;

        // otpBox1: editable from Visual Studio Designer.
        this.otpBox1.Name = "otpBox1";
        this.otpBox1.Location = new Point(35, 200);
        this.otpBox1.Size = new Size(63, 68);
        this.otpBox1.BackColor = Color.FromArgb(21, 22, 50);
        this.otpBox1.GradientColor = Color.FromArgb(21, 22, 50);
        this.otpBox1.BorderColor = Color.FromArgb(109, 101, 180);
        this.otpBox1.CornerRadius = 10;

        // txtOtp1: editable from Visual Studio Designer.
        this.txtOtp1.Name = "txtOtp1";
        this.txtOtp1.Location = new Point(8, 20);
        this.txtOtp1.Size = new Size(47, 31);
        this.txtOtp1.BorderStyle = BorderStyle.None;
        this.txtOtp1.BackColor = Color.FromArgb(21, 22, 50);
        this.txtOtp1.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtOtp1.Font = new Font("Segoe UI", 11F);
        this.txtOtp1.TabIndex = 0;
        this.txtOtp1.PlaceholderText = "";
        this.txtOtp1.MaxLength = 1;
        this.txtOtp1.TextAlign = HorizontalAlignment.Center;

        // otpBox2: editable from Visual Studio Designer.
        this.otpBox2.Name = "otpBox2";
        this.otpBox2.Location = new Point(107, 200);
        this.otpBox2.Size = new Size(63, 68);
        this.otpBox2.BackColor = Color.FromArgb(21, 22, 50);
        this.otpBox2.GradientColor = Color.FromArgb(21, 22, 50);
        this.otpBox2.BorderColor = Color.FromArgb(109, 101, 180);
        this.otpBox2.CornerRadius = 10;

        // txtOtp2: editable from Visual Studio Designer.
        this.txtOtp2.Name = "txtOtp2";
        this.txtOtp2.Location = new Point(8, 20);
        this.txtOtp2.Size = new Size(47, 31);
        this.txtOtp2.BorderStyle = BorderStyle.None;
        this.txtOtp2.BackColor = Color.FromArgb(21, 22, 50);
        this.txtOtp2.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtOtp2.Font = new Font("Segoe UI", 11F);
        this.txtOtp2.TabIndex = 1;
        this.txtOtp2.PlaceholderText = "";
        this.txtOtp2.MaxLength = 1;
        this.txtOtp2.TextAlign = HorizontalAlignment.Center;

        // otpBox3: editable from Visual Studio Designer.
        this.otpBox3.Name = "otpBox3";
        this.otpBox3.Location = new Point(179, 200);
        this.otpBox3.Size = new Size(63, 68);
        this.otpBox3.BackColor = Color.FromArgb(21, 22, 50);
        this.otpBox3.GradientColor = Color.FromArgb(21, 22, 50);
        this.otpBox3.BorderColor = Color.FromArgb(109, 101, 180);
        this.otpBox3.CornerRadius = 10;

        // txtOtp3: editable from Visual Studio Designer.
        this.txtOtp3.Name = "txtOtp3";
        this.txtOtp3.Location = new Point(8, 20);
        this.txtOtp3.Size = new Size(47, 31);
        this.txtOtp3.BorderStyle = BorderStyle.None;
        this.txtOtp3.BackColor = Color.FromArgb(21, 22, 50);
        this.txtOtp3.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtOtp3.Font = new Font("Segoe UI", 11F);
        this.txtOtp3.TabIndex = 2;
        this.txtOtp3.PlaceholderText = "";
        this.txtOtp3.MaxLength = 1;
        this.txtOtp3.TextAlign = HorizontalAlignment.Center;

        // otpBox4: editable from Visual Studio Designer.
        this.otpBox4.Name = "otpBox4";
        this.otpBox4.Location = new Point(251, 200);
        this.otpBox4.Size = new Size(63, 68);
        this.otpBox4.BackColor = Color.FromArgb(21, 22, 50);
        this.otpBox4.GradientColor = Color.FromArgb(21, 22, 50);
        this.otpBox4.BorderColor = Color.FromArgb(109, 101, 180);
        this.otpBox4.CornerRadius = 10;

        // txtOtp4: editable from Visual Studio Designer.
        this.txtOtp4.Name = "txtOtp4";
        this.txtOtp4.Location = new Point(8, 20);
        this.txtOtp4.Size = new Size(47, 31);
        this.txtOtp4.BorderStyle = BorderStyle.None;
        this.txtOtp4.BackColor = Color.FromArgb(21, 22, 50);
        this.txtOtp4.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtOtp4.Font = new Font("Segoe UI", 11F);
        this.txtOtp4.TabIndex = 3;
        this.txtOtp4.PlaceholderText = "";
        this.txtOtp4.MaxLength = 1;
        this.txtOtp4.TextAlign = HorizontalAlignment.Center;

        // otpBox5: editable from Visual Studio Designer.
        this.otpBox5.Name = "otpBox5";
        this.otpBox5.Location = new Point(323, 200);
        this.otpBox5.Size = new Size(63, 68);
        this.otpBox5.BackColor = Color.FromArgb(21, 22, 50);
        this.otpBox5.GradientColor = Color.FromArgb(21, 22, 50);
        this.otpBox5.BorderColor = Color.FromArgb(109, 101, 180);
        this.otpBox5.CornerRadius = 10;

        // txtOtp5: editable from Visual Studio Designer.
        this.txtOtp5.Name = "txtOtp5";
        this.txtOtp5.Location = new Point(8, 20);
        this.txtOtp5.Size = new Size(47, 31);
        this.txtOtp5.BorderStyle = BorderStyle.None;
        this.txtOtp5.BackColor = Color.FromArgb(21, 22, 50);
        this.txtOtp5.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtOtp5.Font = new Font("Segoe UI", 11F);
        this.txtOtp5.TabIndex = 4;
        this.txtOtp5.PlaceholderText = "";
        this.txtOtp5.MaxLength = 1;
        this.txtOtp5.TextAlign = HorizontalAlignment.Center;

        // otpBox6: editable from Visual Studio Designer.
        this.otpBox6.Name = "otpBox6";
        this.otpBox6.Location = new Point(395, 200);
        this.otpBox6.Size = new Size(63, 68);
        this.otpBox6.BackColor = Color.FromArgb(21, 22, 50);
        this.otpBox6.GradientColor = Color.FromArgb(21, 22, 50);
        this.otpBox6.BorderColor = Color.FromArgb(109, 101, 180);
        this.otpBox6.CornerRadius = 10;

        // txtOtp6: editable from Visual Studio Designer.
        this.txtOtp6.Name = "txtOtp6";
        this.txtOtp6.Location = new Point(8, 20);
        this.txtOtp6.Size = new Size(47, 31);
        this.txtOtp6.BorderStyle = BorderStyle.None;
        this.txtOtp6.BackColor = Color.FromArgb(21, 22, 50);
        this.txtOtp6.ForeColor = Color.FromArgb(246, 244, 255);
        this.txtOtp6.Font = new Font("Segoe UI", 11F);
        this.txtOtp6.TabIndex = 5;
        this.txtOtp6.PlaceholderText = "";
        this.txtOtp6.MaxLength = 1;
        this.txtOtp6.TextAlign = HorizontalAlignment.Center;

        // lblCountdown: editable from Visual Studio Designer.
        this.lblCountdown.Name = "lblCountdown";
        this.lblCountdown.Location = new Point(35, 287);
        this.lblCountdown.Size = new Size(245, 30);
        this.lblCountdown.AutoSize = false;
        this.lblCountdown.BackColor = Color.Transparent;
        this.lblCountdown.TabStop = false;
        this.lblCountdown.Text = "◷  Còn lại: 05:00";
        this.lblCountdown.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        this.lblCountdown.ForeColor = Color.FromArgb(64, 217, 255);
        this.lblCountdown.TextAlign = ContentAlignment.MiddleLeft;

        // btnVerify: editable from Visual Studio Designer.
        this.btnVerify.Name = "btnVerify";
        this.btnVerify.Location = new Point(35, 337);
        this.btnVerify.Size = new Size(430, 52);
        this.btnVerify.ForeColor = Color.FromArgb(246, 244, 255);
        this.btnVerify.TabIndex = 6;
        this.btnVerify.Text = "Xác nhận OTP";
        this.btnVerify.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        this.btnVerify.CornerRadius = 12;
        this.btnVerify.BackColor = Color.FromArgb(49, 190, 235);
        this.btnVerify.GradientColor = Color.FromArgb(146, 86, 237);
        this.btnVerify.BorderColor = Color.FromArgb(173, 141, 255);

        // btnResend: editable from Visual Studio Designer.
        this.btnResend.Name = "btnResend";
        this.btnResend.Location = new Point(35, 410);
        this.btnResend.Size = new Size(430, 47);
        this.btnResend.ForeColor = Color.FromArgb(246, 244, 255);
        this.btnResend.TabIndex = 7;
        this.btnResend.Text = "Gửi lại mã (sau 60s)";
        this.btnResend.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.btnResend.CornerRadius = 12;
        this.btnResend.BackColor = Color.FromArgb(49, 39, 93);
        this.btnResend.GradientColor = Color.FromArgb(33, 29, 72);
        this.btnResend.BorderColor = Color.FromArgb(101, 89, 158);

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

        this.btnVerify.Click += this.btnVerify_Click;
        this.btnResend.Click += this.btnResend_Click;

        this.Controls.Add(this.logoBrand);
        this.Controls.Add(this.brandName);
        this.Controls.Add(this.brandSub);
        this.Controls.Add(this.cardMain);
        this.cardMain.Controls.Add(this.lblStep);
        this.cardMain.Controls.Add(this.lblTitle);
        this.cardMain.Controls.Add(this.lblSubtitle);
        this.cardMain.Controls.Add(this.lblDescription);
        this.cardMain.Controls.Add(this.lblEmail);
        this.cardMain.Controls.Add(this.otpBox1);
        this.otpBox1.Controls.Add(this.txtOtp1);
        this.cardMain.Controls.Add(this.otpBox2);
        this.otpBox2.Controls.Add(this.txtOtp2);
        this.cardMain.Controls.Add(this.otpBox3);
        this.otpBox3.Controls.Add(this.txtOtp3);
        this.cardMain.Controls.Add(this.otpBox4);
        this.otpBox4.Controls.Add(this.txtOtp4);
        this.cardMain.Controls.Add(this.otpBox5);
        this.otpBox5.Controls.Add(this.txtOtp5);
        this.cardMain.Controls.Add(this.otpBox6);
        this.otpBox6.Controls.Add(this.txtOtp6);
        this.cardMain.Controls.Add(this.lblCountdown);
        this.cardMain.Controls.Add(this.btnVerify);
        this.cardMain.Controls.Add(this.btnResend);
        this.Controls.Add(this.lblStatus);

        this.AutoScaleDimensions = new SizeF(96F, 96F);
        this.AutoScaleMode = AutoScaleMode.Dpi;
        this.ClientSize = new Size(760, 660);
        this.BackColor = Color.FromArgb(16, 16, 44);
        this.Text = "Caro Online  ·  Xác minh OTP";
        this.Font = new Font("Segoe UI", 10F);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = true;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ResumeLayout(false);
    }
}
