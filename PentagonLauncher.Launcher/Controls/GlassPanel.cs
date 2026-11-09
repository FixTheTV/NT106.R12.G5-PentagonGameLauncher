using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PentagonLauncher.Launcher.Controls;

[ToolboxItem(true)]
public class GlassPanel : Panel
{
    private int _cornerRadius = 18;
    private Color _borderColor = Color.FromArgb(67, 67, 113);
    private Color _gradientColor = Color.FromArgb(24, 26, 50);

    [Category("Caro • Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

    [Category("Caro • Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientColor { get => _gradientColor; set { _gradientColor = value; Invalidate(); } }

    [Category("Caro • Appearance")]
    [DefaultValue(18)]
    public int CornerRadius { get => _cornerRadius; set { _cornerRadius = Math.Max(2, value); Invalidate(); } }

    public GlassPanel()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.FromArgb(24, 26, 50);
        Padding = new Padding(1);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var back = new SolidBrush(Parent?.BackColor ?? Color.FromArgb(12, 14, 30)))
            e.Graphics.FillRectangle(back, ClientRectangle);
        var rect = new Rectangle(1, 1, Math.Max(1, Width - 3), Math.Max(1, Height - 3));
        using var path = NeonButton.Rounded(rect, CornerRadius);
        using (var fill = new LinearGradientBrush(rect, BackColor, GradientColor, LinearGradientMode.Vertical))
            e.Graphics.FillPath(fill, path);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var rect = new Rectangle(1, 1, Math.Max(1, Width - 3), Math.Max(1, Height - 3));
        using var path = NeonButton.Rounded(rect, CornerRadius);
        using var line = new Pen(BorderColor, 1F);
        e.Graphics.DrawPath(line, path);
    }
}
