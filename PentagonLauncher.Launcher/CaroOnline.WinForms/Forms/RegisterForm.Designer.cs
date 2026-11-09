using System;
using System.Drawing;
using System.Windows.Forms;
using CaroOnline.WinForms.Controls;

namespace CaroOnline.WinForms.Forms;

public partial class RegisterForm
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
    private Label lblEmail = null!;
    private GlassPanel boxEmail = null!;
    private Label icoEmail = null!;
    private TextBox txtEmail = null!;
    private GlassPanel otpPanel = null!;
    private Label lblOtpTitle = null!;
    private Label lblOtpState = null!;
    private TextBox txtOtp1 = null!;
    private TextBox txtOtp2 = null!;
    private TextBox txtOtp3 = null!;
    private TextBox txtOtp4 = null!;
    private TextBox txtOtp5 = null!;
    private TextBox txtOtp6 = null!;
    private NeonButton btnSendOtp = null!;
    private NeonButton btnVerifyOtp = null!;
    private Label lblCountdown = null!;
    private Label lblOtpStatus = null!;
    private Label lblPassword = null!;
    private GlassPanel boxPassword = null!;
    private Label icoPassword = null!;
    private TextBox txtPassword = null!;
    private Label lblConfirm = null!;
    private GlassPanel boxConfirm = null!;
    private Label icoConfirm = null!;
    private TextBox txtConfirm = null!;
    private NeonButton btnRegister = null!;
    private LinkLabel lnkBack = null!;
    private Label lblStatus = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        logoBrand = new CaroLogo();
        brandName = new Label();
        brandSub = new Label();
        cardMain = new GlassPanel();
        lblStep = new Label();
        lblTitle = new Label();
        lblSubtitle = new Label();
        lblUsername = new Label();
        boxUsername = new GlassPanel();
        icoUsername = new Label();
        txtUsername = new TextBox();
        lblEmail = new Label();
        boxEmail = new GlassPanel();
        icoEmail = new Label();
        txtEmail = new TextBox();
        otpPanel = new GlassPanel();
        lblOtpTitle = new Label();
        lblOtpState = new Label();
        txtOtp1 = new TextBox();
        txtOtp2 = new TextBox();
        txtOtp3 = new TextBox();
        txtOtp4 = new TextBox();
        txtOtp5 = new TextBox();
        txtOtp6 = new TextBox();
        btnSendOtp = new NeonButton();
        btnVerifyOtp = new NeonButton();
        lblCountdown = new Label();
        lblOtpStatus = new Label();
        lblPassword = new Label();
        boxPassword = new GlassPanel();
        icoPassword = new Label();
        txtPassword = new TextBox();
        lblConfirm = new Label();
        boxConfirm = new GlassPanel();
        icoConfirm = new Label();
        txtConfirm = new TextBox();
        btnRegister = new NeonButton();
        lnkBack = new LinkLabel();
        lblStatus = new Label();
        cardMain.SuspendLayout();
        boxUsername.SuspendLayout();
        boxEmail.SuspendLayout();
        otpPanel.SuspendLayout();
        boxPassword.SuspendLayout();
        boxConfirm.SuspendLayout();
        SuspendLayout();
        // 
        // logoBrand
        // 
        logoBrand.BackColor = Color.Transparent;
        logoBrand.GridColor = Color.FromArgb(103, 109, 206);
        logoBrand.Location = new Point(198, 18);
        logoBrand.Margin = new Padding(4, 4, 4, 4);
        logoBrand.MinimumSize = new Size(35, 35);
        logoBrand.Name = "logoBrand";
        logoBrand.OColor = Color.FromArgb(245, 128, 222);
        logoBrand.Size = new Size(70, 70);
        logoBrand.TabIndex = 0;
        logoBrand.XColor = Color.FromArgb(50, 214, 255);
        // 
        // brandName
        // 
        brandName.BackColor = Color.Transparent;
        brandName.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
        brandName.ForeColor = Color.FromArgb(246, 244, 255);
        brandName.Location = new Point(289, 20);
        brandName.Margin = new Padding(4, 0, 4, 0);
        brandName.Name = "brandName";
        brandName.Size = new Size(460, 40);
        brandName.TabIndex = 1;
        brandName.Text = "Caro Online";
        brandName.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // brandSub
        // 
        brandSub.BackColor = Color.Transparent;
        brandSub.Font = new Font("Segoe UI", 9.5F);
        brandSub.ForeColor = Color.FromArgb(170, 168, 205);
        brandSub.Location = new Point(290, 61);
        brandSub.Margin = new Padding(4, 0, 4, 0);
        brandSub.Name = "brandSub";
        brandSub.Size = new Size(485, 28);
        brandSub.TabIndex = 2;
        brandSub.Text = "Chơi Caro trực tuyến cùng bạn bè";
        brandSub.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cardMain
        // 
        cardMain.BackColor = Color.FromArgb(27, 27, 66);
        cardMain.BorderColor = Color.FromArgb(103, 83, 175);
        cardMain.Controls.Add(lblStep);
        cardMain.Controls.Add(lblTitle);
        cardMain.Controls.Add(lblSubtitle);
        cardMain.Controls.Add(lblUsername);
        cardMain.Controls.Add(boxUsername);
        cardMain.Controls.Add(lblEmail);
        cardMain.Controls.Add(boxEmail);
        cardMain.Controls.Add(otpPanel);
        cardMain.Controls.Add(lblPassword);
        cardMain.Controls.Add(boxPassword);
        cardMain.Controls.Add(lblConfirm);
        cardMain.Controls.Add(boxConfirm);
        cardMain.Controls.Add(btnRegister);
        cardMain.Controls.Add(lnkBack);
        cardMain.Controls.Add(lblStatus);
        cardMain.CornerRadius = 20;
        cardMain.GradientColor = Color.FromArgb(25, 25, 59);
        cardMain.Location = new Point(162, 102);
        cardMain.Margin = new Padding(4, 4, 4, 4);
        cardMain.Name = "cardMain";
        cardMain.Padding = new Padding(1);
        cardMain.Size = new Size(625, 821);
        cardMain.TabIndex = 3;
        // 
        // lblStep
        // 
        lblStep.BackColor = Color.Transparent;
        lblStep.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblStep.ForeColor = Color.FromArgb(64, 217, 255);
        lblStep.Location = new Point(42, 16);
        lblStep.Margin = new Padding(4, 0, 4, 0);
        lblStep.Name = "lblStep";
        lblStep.Size = new Size(540, 24);
        lblStep.TabIndex = 0;
        lblStep.Text = "02 / TẠO TÀI KHOẢN";
        lblStep.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTitle
        // 
        lblTitle.BackColor = Color.Transparent;
        lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(246, 244, 255);
        lblTitle.Location = new Point(42, 45);
        lblTitle.Margin = new Padding(4, 0, 4, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(540, 49);
        lblTitle.TabIndex = 1;
        lblTitle.Text = "Đăng ký tài khoản";
        lblTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblSubtitle
        // 
        lblSubtitle.BackColor = Color.Transparent;
        lblSubtitle.Font = new Font("Segoe UI", 9.5F);
        lblSubtitle.ForeColor = Color.FromArgb(170, 168, 205);
        lblSubtitle.Location = new Point(44, 95);
        lblSubtitle.Margin = new Padding(4, 0, 4, 0);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(538, 28);
        lblSubtitle.TabIndex = 2;
        lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
        lblSubtitle.Click += lblSubtitle_Click;
        // 
        // lblUsername
        // 
        lblUsername.BackColor = Color.Transparent;
        lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblUsername.ForeColor = Color.FromArgb(170, 168, 205);
        lblUsername.Location = new Point(44, 130);
        lblUsername.Margin = new Padding(4, 0, 4, 0);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(538, 22);
        lblUsername.TabIndex = 3;
        lblUsername.Text = "USERNAME  (3–20 KÝ TỰ)";
        lblUsername.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // boxUsername
        // 
        boxUsername.BackColor = Color.FromArgb(21, 22, 50);
        boxUsername.BorderColor = Color.FromArgb(79, 86, 139);
        boxUsername.Controls.Add(icoUsername);
        boxUsername.Controls.Add(txtUsername);
        boxUsername.CornerRadius = 9;
        boxUsername.GradientColor = Color.FromArgb(21, 22, 50);
        boxUsername.Location = new Point(44, 156);
        boxUsername.Margin = new Padding(4, 4, 4, 4);
        boxUsername.Name = "boxUsername";
        boxUsername.Padding = new Padding(1);
        boxUsername.Size = new Size(538, 49);
        boxUsername.TabIndex = 4;
        // 
        // icoUsername
        // 
        icoUsername.BackColor = Color.Transparent;
        icoUsername.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        icoUsername.ForeColor = Color.FromArgb(64, 217, 255);
        icoUsername.Location = new Point(12, 8);
        icoUsername.Margin = new Padding(4, 0, 4, 0);
        icoUsername.Name = "icoUsername";
        icoUsername.Size = new Size(34, 34);
        icoUsername.TabIndex = 0;
        icoUsername.Text = "@";
        icoUsername.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // txtUsername
        // 
        txtUsername.BackColor = Color.FromArgb(21, 22, 50);
        txtUsername.BorderStyle = BorderStyle.None;
        txtUsername.Font = new Font("Segoe UI", 10.5F);
        txtUsername.ForeColor = Color.FromArgb(246, 244, 255);
        txtUsername.Location = new Point(58, 11);
        txtUsername.Margin = new Padding(4, 4, 4, 4);
        txtUsername.MaxLength = 20;
        txtUsername.Name = "txtUsername";
        txtUsername.PlaceholderText = "Ví dụ: admin123";
        txtUsername.Size = new Size(458, 24);
        txtUsername.TabIndex = 0;
        txtUsername.TextChanged += txtUsername_TextChanged;
        // 
        // lblEmail
        // 
        lblEmail.BackColor = Color.Transparent;
        lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblEmail.ForeColor = Color.FromArgb(170, 168, 205);
        lblEmail.Location = new Point(44, 214);
        lblEmail.Margin = new Padding(4, 0, 4, 0);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(538, 22);
        lblEmail.TabIndex = 5;
        lblEmail.Text = "EMAIL";
        lblEmail.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // boxEmail
        // 
        boxEmail.BackColor = Color.FromArgb(21, 22, 50);
        boxEmail.BorderColor = Color.FromArgb(79, 86, 139);
        boxEmail.Controls.Add(icoEmail);
        boxEmail.Controls.Add(txtEmail);
        boxEmail.CornerRadius = 9;
        boxEmail.GradientColor = Color.FromArgb(21, 22, 50);
        boxEmail.Location = new Point(44, 240);
        boxEmail.Margin = new Padding(4, 4, 4, 4);
        boxEmail.Name = "boxEmail";
        boxEmail.Padding = new Padding(1);
        boxEmail.Size = new Size(538, 49);
        boxEmail.TabIndex = 6;
        // 
        // icoEmail
        // 
        icoEmail.BackColor = Color.Transparent;
        icoEmail.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        icoEmail.ForeColor = Color.FromArgb(64, 217, 255);
        icoEmail.Location = new Point(12, 8);
        icoEmail.Margin = new Padding(4, 0, 4, 0);
        icoEmail.Name = "icoEmail";
        icoEmail.Size = new Size(34, 34);
        icoEmail.TabIndex = 0;
        icoEmail.Text = "@";
        icoEmail.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // txtEmail
        // 
        txtEmail.BackColor = Color.FromArgb(21, 22, 50);
        txtEmail.BorderStyle = BorderStyle.None;
        txtEmail.Font = new Font("Segoe UI", 10.5F);
        txtEmail.ForeColor = Color.FromArgb(246, 244, 255);
        txtEmail.Location = new Point(58, 11);
        txtEmail.Margin = new Padding(4, 4, 4, 4);
        txtEmail.MaxLength = 254;
        txtEmail.Name = "txtEmail";
        txtEmail.PlaceholderText = "email@example.com";
        txtEmail.Size = new Size(458, 24);
        txtEmail.TabIndex = 1;
        // 
        // otpPanel
        // 
        otpPanel.BackColor = Color.FromArgb(29, 26, 69);
        otpPanel.BorderColor = Color.FromArgb(134, 96, 221);
        otpPanel.Controls.Add(lblOtpTitle);
        otpPanel.Controls.Add(lblOtpState);
        otpPanel.Controls.Add(txtOtp1);
        otpPanel.Controls.Add(txtOtp2);
        otpPanel.Controls.Add(txtOtp3);
        otpPanel.Controls.Add(txtOtp4);
        otpPanel.Controls.Add(txtOtp5);
        otpPanel.Controls.Add(txtOtp6);
        otpPanel.Controls.Add(btnSendOtp);
        otpPanel.Controls.Add(btnVerifyOtp);
        otpPanel.Controls.Add(lblCountdown);
        otpPanel.Controls.Add(lblOtpStatus);
        otpPanel.CornerRadius = 10;
        otpPanel.GradientColor = Color.FromArgb(31, 28, 75);
        otpPanel.Location = new Point(44, 302);
        otpPanel.Margin = new Padding(4, 4, 4, 4);
        otpPanel.Name = "otpPanel";
        otpPanel.Padding = new Padding(1);
        otpPanel.Size = new Size(538, 164);
        otpPanel.TabIndex = 7;
        // 
        // lblOtpTitle
        // 
        lblOtpTitle.BackColor = Color.Transparent;
        lblOtpTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblOtpTitle.ForeColor = Color.FromArgb(201, 199, 231);
        lblOtpTitle.Location = new Point(16, 9);
        lblOtpTitle.Margin = new Padding(4, 0, 4, 0);
        lblOtpTitle.Name = "lblOtpTitle";
        lblOtpTitle.Size = new Size(250, 26);
        lblOtpTitle.TabIndex = 0;
        lblOtpTitle.Text = "XÁC MINH EMAIL";
        lblOtpTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblOtpState
        // 
        lblOtpState.BackColor = Color.Transparent;
        lblOtpState.Font = new Font("Segoe UI", 8.7F, FontStyle.Bold);
        lblOtpState.ForeColor = Color.FromArgb(247, 164, 179);
        lblOtpState.Location = new Point(302, 9);
        lblOtpState.Margin = new Padding(4, 0, 4, 0);
        lblOtpState.Name = "lblOtpState";
        lblOtpState.Size = new Size(214, 26);
        lblOtpState.TabIndex = 1;
        lblOtpState.Text = "Chưa xác minh";
        lblOtpState.TextAlign = ContentAlignment.MiddleRight;
        // 
        // txtOtp1
        // 
        txtOtp1.BackColor = Color.FromArgb(21, 22, 50);
        txtOtp1.BorderStyle = BorderStyle.FixedSingle;
        txtOtp1.Enabled = false;
        txtOtp1.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        txtOtp1.ForeColor = Color.FromArgb(246, 244, 255);
        txtOtp1.Location = new Point(16, 49);
        txtOtp1.Margin = new Padding(4, 4, 4, 4);
        txtOtp1.MaxLength = 1;
        txtOtp1.Name = "txtOtp1";
        txtOtp1.Size = new Size(42, 36);
        txtOtp1.TabIndex = 3;
        txtOtp1.TextAlign = HorizontalAlignment.Center;
        // 
        // txtOtp2
        // 
        txtOtp2.BackColor = Color.FromArgb(21, 22, 50);
        txtOtp2.BorderStyle = BorderStyle.FixedSingle;
        txtOtp2.Enabled = false;
        txtOtp2.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        txtOtp2.ForeColor = Color.FromArgb(246, 244, 255);
        txtOtp2.Location = new Point(65, 49);
        txtOtp2.Margin = new Padding(4, 4, 4, 4);
        txtOtp2.MaxLength = 1;
        txtOtp2.Name = "txtOtp2";
        txtOtp2.Size = new Size(42, 36);
        txtOtp2.TabIndex = 4;
        txtOtp2.TextAlign = HorizontalAlignment.Center;
        // 
        // txtOtp3
        // 
        txtOtp3.BackColor = Color.FromArgb(21, 22, 50);
        txtOtp3.BorderStyle = BorderStyle.FixedSingle;
        txtOtp3.Enabled = false;
        txtOtp3.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        txtOtp3.ForeColor = Color.FromArgb(246, 244, 255);
        txtOtp3.Location = new Point(114, 49);
        txtOtp3.Margin = new Padding(4, 4, 4, 4);
        txtOtp3.MaxLength = 1;
        txtOtp3.Name = "txtOtp3";
        txtOtp3.Size = new Size(42, 36);
        txtOtp3.TabIndex = 5;
        txtOtp3.TextAlign = HorizontalAlignment.Center;
        // 
        // txtOtp4
        // 
        txtOtp4.BackColor = Color.FromArgb(21, 22, 50);
        txtOtp4.BorderStyle = BorderStyle.FixedSingle;
        txtOtp4.Enabled = false;
        txtOtp4.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        txtOtp4.ForeColor = Color.FromArgb(246, 244, 255);
        txtOtp4.Location = new Point(162, 49);
        txtOtp4.Margin = new Padding(4, 4, 4, 4);
        txtOtp4.MaxLength = 1;
        txtOtp4.Name = "txtOtp4";
        txtOtp4.Size = new Size(42, 36);
        txtOtp4.TabIndex = 6;
        txtOtp4.TextAlign = HorizontalAlignment.Center;
        // 
        // txtOtp5
        // 
        txtOtp5.BackColor = Color.FromArgb(21, 22, 50);
        txtOtp5.BorderStyle = BorderStyle.FixedSingle;
        txtOtp5.Enabled = false;
        txtOtp5.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        txtOtp5.ForeColor = Color.FromArgb(246, 244, 255);
        txtOtp5.Location = new Point(211, 49);
        txtOtp5.Margin = new Padding(4, 4, 4, 4);
        txtOtp5.MaxLength = 1;
        txtOtp5.Name = "txtOtp5";
        txtOtp5.Size = new Size(42, 36);
        txtOtp5.TabIndex = 7;
        txtOtp5.TextAlign = HorizontalAlignment.Center;
        // 
        // txtOtp6
        // 
        txtOtp6.BackColor = Color.FromArgb(21, 22, 50);
        txtOtp6.BorderStyle = BorderStyle.FixedSingle;
        txtOtp6.Enabled = false;
        txtOtp6.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        txtOtp6.ForeColor = Color.FromArgb(246, 244, 255);
        txtOtp6.Location = new Point(260, 49);
        txtOtp6.Margin = new Padding(4, 4, 4, 4);
        txtOtp6.MaxLength = 1;
        txtOtp6.Name = "txtOtp6";
        txtOtp6.Size = new Size(42, 36);
        txtOtp6.TabIndex = 8;
        txtOtp6.TextAlign = HorizontalAlignment.Center;
        // 
        // btnSendOtp
        // 
        btnSendOtp.BackColor = Color.FromArgb(42, 163, 230);
        btnSendOtp.BorderColor = Color.FromArgb(128, 139, 245);
        btnSendOtp.CornerRadius = 8;
        btnSendOtp.FlatStyle = FlatStyle.Flat;
        btnSendOtp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSendOtp.ForeColor = Color.FromArgb(246, 244, 255);
        btnSendOtp.GradientColor = Color.FromArgb(96, 77, 224);
        btnSendOtp.Location = new Point(319, 46);
        btnSendOtp.Margin = new Padding(4, 4, 4, 4);
        btnSendOtp.Name = "btnSendOtp";
        btnSendOtp.Size = new Size(199, 44);
        btnSendOtp.TabIndex = 2;
        btnSendOtp.Text = "Gửi mã";
        btnSendOtp.UseVisualStyleBackColor = false;
        btnSendOtp.Click += btnSendOtp_Click;
        // 
        // btnVerifyOtp
        // 
        btnVerifyOtp.BackColor = Color.FromArgb(89, 59, 192);
        btnVerifyOtp.BorderColor = Color.FromArgb(140, 106, 236);
        btnVerifyOtp.CornerRadius = 8;
        btnVerifyOtp.Enabled = false;
        btnVerifyOtp.FlatStyle = FlatStyle.Flat;
        btnVerifyOtp.Font = new Font("Segoe UI", 8.7F, FontStyle.Bold);
        btnVerifyOtp.ForeColor = Color.FromArgb(246, 244, 255);
        btnVerifyOtp.GradientColor = Color.FromArgb(129, 70, 220);
        btnVerifyOtp.Location = new Point(319, 96);
        btnVerifyOtp.Margin = new Padding(4, 4, 4, 4);
        btnVerifyOtp.Name = "btnVerifyOtp";
        btnVerifyOtp.Size = new Size(199, 39);
        btnVerifyOtp.TabIndex = 9;
        btnVerifyOtp.Text = "Xác minh mã";
        btnVerifyOtp.UseVisualStyleBackColor = false;
        btnVerifyOtp.Click += btnVerifyOtp_Click;
        // 
        // lblCountdown
        // 
        lblCountdown.BackColor = Color.Transparent;
        lblCountdown.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCountdown.ForeColor = Color.FromArgb(64, 217, 255);
        lblCountdown.Location = new Point(16, 102);
        lblCountdown.Margin = new Padding(4, 0, 4, 0);
        lblCountdown.Name = "lblCountdown";
        lblCountdown.Size = new Size(295, 29);
        lblCountdown.TabIndex = 10;
        lblCountdown.Text = "Mã có hiệu lực: 05:00";
        lblCountdown.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblOtpStatus
        // 
        lblOtpStatus.BackColor = Color.Transparent;
        lblOtpStatus.Font = new Font("Segoe UI", 8.3F);
        lblOtpStatus.ForeColor = Color.FromArgb(170, 168, 205);
        lblOtpStatus.Location = new Point(18, 139);
        lblOtpStatus.Margin = new Padding(4, 0, 4, 0);
        lblOtpStatus.Name = "lblOtpStatus";
        lblOtpStatus.Size = new Size(500, 21);
        lblOtpStatus.TabIndex = 11;
        lblOtpStatus.Text = "Nhấn Gửi mã để bắt đầu xác minh.";
        lblOtpStatus.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblPassword
        // 
        lblPassword.BackColor = Color.Transparent;
        lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPassword.ForeColor = Color.FromArgb(170, 168, 205);
        lblPassword.Location = new Point(44, 476);
        lblPassword.Margin = new Padding(4, 0, 4, 0);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(538, 22);
        lblPassword.TabIndex = 8;
        lblPassword.Text = "MẬT KHẨU  (TỐI THIỂU 6 KÝ TỰ)";
        lblPassword.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // boxPassword
        // 
        boxPassword.BackColor = Color.FromArgb(21, 22, 50);
        boxPassword.BorderColor = Color.FromArgb(79, 86, 139);
        boxPassword.Controls.Add(icoPassword);
        boxPassword.Controls.Add(txtPassword);
        boxPassword.CornerRadius = 9;
        boxPassword.GradientColor = Color.FromArgb(21, 22, 50);
        boxPassword.Location = new Point(44, 501);
        boxPassword.Margin = new Padding(4, 4, 4, 4);
        boxPassword.Name = "boxPassword";
        boxPassword.Padding = new Padding(1);
        boxPassword.Size = new Size(538, 49);
        boxPassword.TabIndex = 9;
        // 
        // icoPassword
        // 
        icoPassword.BackColor = Color.Transparent;
        icoPassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        icoPassword.ForeColor = Color.FromArgb(64, 217, 255);
        icoPassword.Location = new Point(12, 8);
        icoPassword.Margin = new Padding(4, 0, 4, 0);
        icoPassword.Name = "icoPassword";
        icoPassword.Size = new Size(34, 34);
        icoPassword.TabIndex = 0;
        icoPassword.Text = "*";
        icoPassword.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // txtPassword
        // 
        txtPassword.BackColor = Color.FromArgb(21, 22, 50);
        txtPassword.BorderStyle = BorderStyle.None;
        txtPassword.Font = new Font("Segoe UI", 10.5F);
        txtPassword.ForeColor = Color.FromArgb(246, 244, 255);
        txtPassword.Location = new Point(58, 11);
        txtPassword.Margin = new Padding(4, 4, 4, 4);
        txtPassword.Name = "txtPassword";
        txtPassword.PlaceholderText = "Nhập mật khẩu";
        txtPassword.Size = new Size(458, 24);
        txtPassword.TabIndex = 10;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // lblConfirm
        // 
        lblConfirm.BackColor = Color.Transparent;
        lblConfirm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblConfirm.ForeColor = Color.FromArgb(170, 168, 205);
        lblConfirm.Location = new Point(44, 558);
        lblConfirm.Margin = new Padding(4, 0, 4, 0);
        lblConfirm.Name = "lblConfirm";
        lblConfirm.Size = new Size(538, 22);
        lblConfirm.TabIndex = 10;
        lblConfirm.Text = "NHẬP LẠI MẬT KHẨU";
        lblConfirm.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // boxConfirm
        // 
        boxConfirm.BackColor = Color.FromArgb(21, 22, 50);
        boxConfirm.BorderColor = Color.FromArgb(79, 86, 139);
        boxConfirm.Controls.Add(icoConfirm);
        boxConfirm.Controls.Add(txtConfirm);
        boxConfirm.CornerRadius = 9;
        boxConfirm.GradientColor = Color.FromArgb(21, 22, 50);
        boxConfirm.Location = new Point(44, 582);
        boxConfirm.Margin = new Padding(4, 4, 4, 4);
        boxConfirm.Name = "boxConfirm";
        boxConfirm.Padding = new Padding(1);
        boxConfirm.Size = new Size(538, 49);
        boxConfirm.TabIndex = 11;
        // 
        // icoConfirm
        // 
        icoConfirm.BackColor = Color.Transparent;
        icoConfirm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        icoConfirm.ForeColor = Color.FromArgb(64, 217, 255);
        icoConfirm.Location = new Point(12, 8);
        icoConfirm.Margin = new Padding(4, 0, 4, 0);
        icoConfirm.Name = "icoConfirm";
        icoConfirm.Size = new Size(34, 34);
        icoConfirm.TabIndex = 0;
        icoConfirm.Text = "*";
        icoConfirm.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // txtConfirm
        // 
        txtConfirm.BackColor = Color.FromArgb(21, 22, 50);
        txtConfirm.BorderStyle = BorderStyle.None;
        txtConfirm.Font = new Font("Segoe UI", 10.5F);
        txtConfirm.ForeColor = Color.FromArgb(246, 244, 255);
        txtConfirm.Location = new Point(58, 11);
        txtConfirm.Margin = new Padding(4, 4, 4, 4);
        txtConfirm.Name = "txtConfirm";
        txtConfirm.PlaceholderText = "Xác nhận mật khẩu";
        txtConfirm.Size = new Size(458, 24);
        txtConfirm.TabIndex = 11;
        txtConfirm.UseSystemPasswordChar = true;
        // 
        // btnRegister
        // 
        btnRegister.BackColor = Color.FromArgb(37, 187, 232);
        btnRegister.BorderColor = Color.FromArgb(172, 143, 255);
        btnRegister.CornerRadius = 11;
        btnRegister.FlatStyle = FlatStyle.Flat;
        btnRegister.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnRegister.ForeColor = Color.FromArgb(246, 244, 255);
        btnRegister.GradientColor = Color.FromArgb(146, 85, 235);
        btnRegister.Location = new Point(44, 651);
        btnRegister.Margin = new Padding(4, 4, 4, 4);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(538, 58);
        btnRegister.TabIndex = 12;
        btnRegister.Text = "Đăng ký";
        btnRegister.UseVisualStyleBackColor = false;
        btnRegister.Click += btnRegister_Click;
        // 
        // lnkBack
        // 
        lnkBack.ActiveLinkColor = Color.FromArgb(246, 244, 255);
        lnkBack.BackColor = Color.Transparent;
        lnkBack.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lnkBack.LinkBehavior = LinkBehavior.HoverUnderline;
        lnkBack.LinkColor = Color.FromArgb(64, 217, 255);
        lnkBack.Location = new Point(150, 722);
        lnkBack.Margin = new Padding(4, 0, 4, 0);
        lnkBack.Name = "lnkBack";
        lnkBack.Size = new Size(325, 29);
        lnkBack.TabIndex = 13;
        lnkBack.TabStop = true;
        lnkBack.Text = "Đã có tài khoản? Đăng nhập";
        lnkBack.TextAlign = ContentAlignment.MiddleCenter;
        lnkBack.VisitedLinkColor = Color.FromArgb(64, 217, 255);
        lnkBack.LinkClicked += lnkBack_Click;
        // 
        // lblStatus
        // 
        lblStatus.BackColor = Color.Transparent;
        lblStatus.Font = new Font("Segoe UI", 9F);
        lblStatus.ForeColor = Color.FromArgb(255, 180, 179);
        lblStatus.Location = new Point(44, 761);
        lblStatus.Margin = new Padding(4, 0, 4, 0);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(538, 41);
        lblStatus.TabIndex = 14;
        lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // RegisterForm
        // 
        AutoScaleDimensions = new SizeF(120F, 120F);
        AutoScaleMode = AutoScaleMode.Dpi;
        AutoScroll = true;
        BackColor = Color.FromArgb(16, 16, 44);
        ClientSize = new Size(950, 951);
        Controls.Add(logoBrand);
        Controls.Add(brandName);
        Controls.Add(brandSub);
        Controls.Add(cardMain);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Margin = new Padding(4, 4, 4, 4);
        MaximizeBox = false;
        Name = "RegisterForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Caro Online · Đăng ký / Xác minh email";
        cardMain.ResumeLayout(false);
        boxUsername.ResumeLayout(false);
        boxUsername.PerformLayout();
        boxEmail.ResumeLayout(false);
        boxEmail.PerformLayout();
        otpPanel.ResumeLayout(false);
        otpPanel.PerformLayout();
        boxPassword.ResumeLayout(false);
        boxPassword.PerformLayout();
        boxConfirm.ResumeLayout(false);
        boxConfirm.PerformLayout();
        ResumeLayout(false);
    }
}
