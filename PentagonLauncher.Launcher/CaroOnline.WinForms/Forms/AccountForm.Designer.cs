using System;
using System.Drawing;
using System.Windows.Forms;
using CaroOnline.WinForms.Controls;

namespace CaroOnline.WinForms.Forms;

public partial class AccountForm
{
    private System.ComponentModel.IContainer? components = null;
    private CaroLogo logoBrand = null!;
    private Label brandName = null!;
    private Label brandSub = null!;
    private GlassPanel cardMain = null!;
    private Label lblTitle = null!;
    private CaroLogo logoAvatar = null!;
    private Label lblWelcome = null!;
    private Label lblUsername = null!;
    private GlassPanel divider = null!;
    private GlassPanel infoPanel = null!;
    private Label lblEmailHeader = null!;
    private Label lblEmail = null!;
    private GlassPanel infoDivider = null!;
    private Label lblDateHeader = null!;
    private Label lblCreated = null!;
    private NeonButton btnBack = null!;

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
        this.lblTitle = new Label();
        this.logoAvatar = new CaroLogo();
        this.lblWelcome = new Label();
        this.lblUsername = new Label();
        this.divider = new GlassPanel();
        this.infoPanel = new GlassPanel();
        this.lblEmailHeader = new Label();
        this.lblEmail = new Label();
        this.infoDivider = new GlassPanel();
        this.lblDateHeader = new Label();
        this.lblCreated = new Label();
        this.btnBack = new NeonButton();

        // logoBrand: editable from Visual Studio Designer.
        this.logoBrand.Name = "logoBrand";
        this.logoBrand.Location = new Point(151, 22);
        this.logoBrand.Size = new Size(68, 68);

        // brandName: editable from Visual Studio Designer.
        this.brandName.Name = "brandName";
        this.brandName.Location = new Point(236, 27);
        this.brandName.Size = new Size(365, 38);
        this.brandName.Text = "Caro Online";
        this.brandName.AutoSize = false;
        this.brandName.BackColor = Color.Transparent;
        this.brandName.Font = new Font("Segoe UI", 23F, FontStyle.Bold);
        this.brandName.ForeColor = Color.FromArgb(246, 244, 255);
        this.brandName.TextAlign = ContentAlignment.MiddleLeft;
        this.brandName.TabStop = false;

        // brandSub: editable from Visual Studio Designer.
        this.brandSub.Name = "brandSub";
        this.brandSub.Location = new Point(237, 63);
        this.brandSub.Size = new Size(420, 23);
        this.brandSub.Text = "Hồ sơ người chơi của bạn";
        this.brandSub.AutoSize = false;
        this.brandSub.BackColor = Color.Transparent;
        this.brandSub.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        this.brandSub.ForeColor = Color.FromArgb(170, 168, 205);
        this.brandSub.TextAlign = ContentAlignment.MiddleLeft;
        this.brandSub.TabStop = false;

        // cardMain: editable from Visual Studio Designer.
        this.cardMain.Name = "cardMain";
        this.cardMain.Location = new Point(130, 112);
        this.cardMain.Size = new Size(500, 434);
        this.cardMain.BackColor = Color.FromArgb(27, 27, 66);
        this.cardMain.GradientColor = Color.FromArgb(26, 25, 61);
        this.cardMain.BorderColor = Color.FromArgb(101, 87, 173);
        this.cardMain.CornerRadius = 20;

        // lblTitle: editable from Visual Studio Designer.
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Location = new Point(35, 24);
        this.lblTitle.Size = new Size(430, 50);
        this.lblTitle.Text = "Thông tin tài khoản";
        this.lblTitle.AutoSize = false;
        this.lblTitle.BackColor = Color.Transparent;
        this.lblTitle.Font = new Font("Segoe UI", 21.5F, FontStyle.Bold);
        this.lblTitle.ForeColor = Color.FromArgb(246, 244, 255);
        this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
        this.lblTitle.TabStop = false;

        // logoAvatar: editable from Visual Studio Designer.
        this.logoAvatar.Name = "logoAvatar";
        this.logoAvatar.Location = new Point(35, 87);
        this.logoAvatar.Size = new Size(92, 92);

        // lblWelcome: editable from Visual Studio Designer.
        this.lblWelcome.Name = "lblWelcome";
        this.lblWelcome.Location = new Point(150, 98);
        this.lblWelcome.Size = new Size(308, 23);
        this.lblWelcome.Text = "NGƯỜI CHƠI CARO";
        this.lblWelcome.AutoSize = false;
        this.lblWelcome.BackColor = Color.Transparent;
        this.lblWelcome.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblWelcome.ForeColor = Color.FromArgb(64, 217, 255);
        this.lblWelcome.TextAlign = ContentAlignment.MiddleLeft;
        this.lblWelcome.TabStop = false;

        // lblUsername: editable from Visual Studio Designer.
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Location = new Point(149, 125);
        this.lblUsername.Size = new Size(306, 48);
        this.lblUsername.Text = "demo";
        this.lblUsername.AutoSize = false;
        this.lblUsername.BackColor = Color.Transparent;
        this.lblUsername.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        this.lblUsername.ForeColor = Color.FromArgb(246, 244, 255);
        this.lblUsername.TextAlign = ContentAlignment.MiddleLeft;
        this.lblUsername.TabStop = false;

        // divider: editable from Visual Studio Designer.
        this.divider.Name = "divider";
        this.divider.Location = new Point(35, 194);
        this.divider.Size = new Size(430, 2);
        this.divider.BackColor = Color.FromArgb(76, 70, 128);
        this.divider.GradientColor = Color.FromArgb(76, 70, 128);
        this.divider.BorderColor = Color.FromArgb(76, 70, 128);
        this.divider.CornerRadius = 2;

        // infoPanel: editable from Visual Studio Designer.
        this.infoPanel.Name = "infoPanel";
        this.infoPanel.Location = new Point(35, 212);
        this.infoPanel.Size = new Size(430, 122);
        this.infoPanel.BackColor = Color.FromArgb(23, 23, 57);
        this.infoPanel.GradientColor = Color.FromArgb(23, 23, 57);
        this.infoPanel.BorderColor = Color.FromArgb(65, 65, 117);
        this.infoPanel.CornerRadius = 13;

        // lblEmailHeader: editable from Visual Studio Designer.
        this.lblEmailHeader.Name = "lblEmailHeader";
        this.lblEmailHeader.Location = new Point(20, 16);
        this.lblEmailHeader.Size = new Size(108, 27);
        this.lblEmailHeader.Text = "EMAIL";
        this.lblEmailHeader.AutoSize = false;
        this.lblEmailHeader.BackColor = Color.Transparent;
        this.lblEmailHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblEmailHeader.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblEmailHeader.TextAlign = ContentAlignment.MiddleLeft;
        this.lblEmailHeader.TabStop = false;

        // lblEmail: editable from Visual Studio Designer.
        this.lblEmail.Name = "lblEmail";
        this.lblEmail.Location = new Point(129, 13);
        this.lblEmail.Size = new Size(284, 34);
        this.lblEmail.Text = "demo@caro.local";
        this.lblEmail.AutoSize = false;
        this.lblEmail.BackColor = Color.Transparent;
        this.lblEmail.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular);
        this.lblEmail.ForeColor = Color.FromArgb(246, 244, 255);
        this.lblEmail.TextAlign = ContentAlignment.MiddleLeft;
        this.lblEmail.AutoEllipsis = true;
        this.lblEmail.TabStop = false;

        // infoDivider: editable from Visual Studio Designer.
        this.infoDivider.Name = "infoDivider";
        this.infoDivider.Location = new Point(20, 60);
        this.infoDivider.Size = new Size(389, 2);
        this.infoDivider.BackColor = Color.FromArgb(56, 56, 101);
        this.infoDivider.GradientColor = Color.FromArgb(56, 56, 101);
        this.infoDivider.BorderColor = Color.FromArgb(56, 56, 101);
        this.infoDivider.CornerRadius = 2;

        // lblDateHeader: editable from Visual Studio Designer.
        this.lblDateHeader.Name = "lblDateHeader";
        this.lblDateHeader.Location = new Point(20, 76);
        this.lblDateHeader.Size = new Size(108, 27);
        this.lblDateHeader.Text = "NGÀY TẠO";
        this.lblDateHeader.AutoSize = false;
        this.lblDateHeader.BackColor = Color.Transparent;
        this.lblDateHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblDateHeader.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblDateHeader.TextAlign = ContentAlignment.MiddleLeft;
        this.lblDateHeader.TabStop = false;

        // lblCreated: editable from Visual Studio Designer.
        this.lblCreated.Name = "lblCreated";
        this.lblCreated.Location = new Point(129, 73);
        this.lblCreated.Size = new Size(284, 34);
        this.lblCreated.Text = "—";
        this.lblCreated.AutoSize = false;
        this.lblCreated.BackColor = Color.Transparent;
        this.lblCreated.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular);
        this.lblCreated.ForeColor = Color.FromArgb(246, 244, 255);
        this.lblCreated.TextAlign = ContentAlignment.MiddleLeft;
        this.lblCreated.TabStop = false;

        // btnBack: editable from Visual Studio Designer.
        this.btnBack.Name = "btnBack";
        this.btnBack.Location = new Point(35, 357);
        this.btnBack.Size = new Size(430, 51);
        this.btnBack.Text = "← Quay lại Launcher";
        this.btnBack.ForeColor = Color.FromArgb(246, 244, 255);
        this.btnBack.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        this.btnBack.CornerRadius = 12;
        this.btnBack.BackColor = Color.FromArgb(49, 190, 235);
        this.btnBack.GradientColor = Color.FromArgb(146, 86, 237);
        this.btnBack.BorderColor = Color.FromArgb(173, 141, 255);
        this.btnBack.TabIndex = 0;
        this.btnBack.Click += this.btnBack_Click;

        this.Controls.Add(this.logoBrand);
        this.Controls.Add(this.brandName);
        this.Controls.Add(this.brandSub);
        this.Controls.Add(this.cardMain);
        this.cardMain.Controls.Add(this.lblTitle);
        this.cardMain.Controls.Add(this.logoAvatar);
        this.cardMain.Controls.Add(this.lblWelcome);
        this.cardMain.Controls.Add(this.lblUsername);
        this.cardMain.Controls.Add(this.divider);
        this.cardMain.Controls.Add(this.infoPanel);
        this.infoPanel.Controls.Add(this.lblEmailHeader);
        this.infoPanel.Controls.Add(this.lblEmail);
        this.infoPanel.Controls.Add(this.infoDivider);
        this.infoPanel.Controls.Add(this.lblDateHeader);
        this.infoPanel.Controls.Add(this.lblCreated);
        this.cardMain.Controls.Add(this.btnBack);

        this.AutoScaleDimensions = new SizeF(96F, 96F);
        this.AutoScaleMode = AutoScaleMode.Dpi;
        this.ClientSize = new Size(760, 594);
        this.BackColor = Color.FromArgb(16, 16, 44);
        this.Text = "Caro Online  ·  Thông tin tài khoản";
        this.Font = new Font("Segoe UI", 10F);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = true;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ResumeLayout(false);
    }
}
