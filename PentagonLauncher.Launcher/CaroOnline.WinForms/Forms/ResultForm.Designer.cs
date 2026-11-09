using System;
using System.Drawing;
using System.Windows.Forms;
using CaroOnline.WinForms.Controls;

namespace CaroOnline.WinForms.Forms;

public partial class ResultForm
{
    private System.ComponentModel.IContainer? components = null;
    private GlassPanel resultPanel = null!;
    private GlassPanel iconPanel = null!;
    private Label lblIcon = null!;
    private Label lblHeadline = null!;
    private Label lblMessage = null!;
    private NeonButton btnAction = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.resultPanel = new GlassPanel();
        this.iconPanel = new GlassPanel();
        this.lblIcon = new Label();
        this.lblHeadline = new Label();
        this.lblMessage = new Label();
        this.btnAction = new NeonButton();

        // resultPanel: editable from Visual Studio Designer.
        this.resultPanel.Name = "resultPanel";
        this.resultPanel.Location = new Point(48, 45);
        this.resultPanel.Size = new Size(524, 355);
        this.resultPanel.BackColor = Color.FromArgb(27, 27, 66);
        this.resultPanel.GradientColor = Color.FromArgb(27, 27, 66);
        this.resultPanel.BorderColor = Color.FromArgb(100, 86, 159);
        this.resultPanel.CornerRadius = 20;

        // iconPanel: editable from Visual Studio Designer.
        this.iconPanel.Name = "iconPanel";
        this.iconPanel.Location = new Point(224, 25);
        this.iconPanel.Size = new Size(76, 76);
        this.iconPanel.BackColor = Color.FromArgb(36, 79, 72);
        this.iconPanel.GradientColor = Color.FromArgb(36, 79, 72);
        this.iconPanel.BorderColor = Color.FromArgb(66, 201, 154);
        this.iconPanel.CornerRadius = 38;

        // lblIcon: editable from Visual Studio Designer.
        this.lblIcon.Name = "lblIcon";
        this.lblIcon.Location = new Point(8, 8);
        this.lblIcon.Size = new Size(58, 58);
        this.lblIcon.AutoSize = false;
        this.lblIcon.BackColor = Color.Transparent;
        this.lblIcon.TabStop = false;
        this.lblIcon.Text = "✓";
        this.lblIcon.Font = new Font("Segoe UI", 29F, FontStyle.Bold);
        this.lblIcon.ForeColor = Color.FromArgb(116, 236, 182);
        this.lblIcon.TextAlign = ContentAlignment.MiddleCenter;

        // lblHeadline: editable from Visual Studio Designer.
        this.lblHeadline.Name = "lblHeadline";
        this.lblHeadline.Location = new Point(32, 122);
        this.lblHeadline.Size = new Size(460, 45);
        this.lblHeadline.AutoSize = false;
        this.lblHeadline.BackColor = Color.Transparent;
        this.lblHeadline.TabStop = false;
        this.lblHeadline.Text = "Đặt lại mật khẩu thành công!";
        this.lblHeadline.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        this.lblHeadline.ForeColor = Color.FromArgb(246, 244, 255);
        this.lblHeadline.TextAlign = ContentAlignment.MiddleCenter;

        // lblMessage: editable from Visual Studio Designer.
        this.lblMessage.Name = "lblMessage";
        this.lblMessage.Location = new Point(45, 188);
        this.lblMessage.Size = new Size(434, 60);
        this.lblMessage.AutoSize = false;
        this.lblMessage.BackColor = Color.Transparent;
        this.lblMessage.TabStop = false;
        this.lblMessage.Text = "Mật khẩu của bạn đã được cập nhật.";
        this.lblMessage.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
        this.lblMessage.ForeColor = Color.FromArgb(170, 168, 205);
        this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;

        // btnAction: editable from Visual Studio Designer.
        this.btnAction.Name = "btnAction";
        this.btnAction.Location = new Point(105, 273);
        this.btnAction.Size = new Size(314, 51);
        this.btnAction.ForeColor = Color.FromArgb(246, 244, 255);
        this.btnAction.TabIndex = 0;
        this.btnAction.Text = "Đăng nhập ngay";
        this.btnAction.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        this.btnAction.CornerRadius = 12;
        this.btnAction.BackColor = Color.FromArgb(49, 190, 235);
        this.btnAction.GradientColor = Color.FromArgb(146, 86, 237);
        this.btnAction.BorderColor = Color.FromArgb(173, 141, 255);

        this.btnAction.Click += this.btnAction_Click;

        this.Controls.Add(this.resultPanel);
        this.resultPanel.Controls.Add(this.iconPanel);
        this.iconPanel.Controls.Add(this.lblIcon);
        this.resultPanel.Controls.Add(this.lblHeadline);
        this.resultPanel.Controls.Add(this.lblMessage);
        this.resultPanel.Controls.Add(this.btnAction);

        this.AutoScaleDimensions = new SizeF(96F, 96F);
        this.AutoScaleMode = AutoScaleMode.Dpi;
        this.ClientSize = new Size(620, 450);
        this.BackColor = Color.FromArgb(16, 16, 44);
        this.Text = "Caro Online  ·  Kết quả đặt lại mật khẩu";
        this.Font = new Font("Segoe UI", 10F);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = true;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ResumeLayout(false);
    }
}
