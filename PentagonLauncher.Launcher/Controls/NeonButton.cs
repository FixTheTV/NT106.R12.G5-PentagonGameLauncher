using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PentagonLauncher.Launcher.Controls;

[ToolboxItem(true)]
public class NeonButton : Button
{
    private int _cornerRadius = 13;
    private Color _borderColor = Color.FromArgb(150, 121, 255);
    private Color _gradientColor = Color.FromArgb(91, 53, 209);
    private bool _hovered;

    [Category("Caro • Appearance")]
    [DefaultValue(13)]
    public int CornerRadius { get => _cornerRadius; set { _cornerRadius = Math.Max(2, value); Invalidate(); } }

    [Category("Caro • Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

    [Category("Caro • Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientColor { get => _gradientColor; set { _gradientColor = value; Invalidate(); } }

    public NeonButton()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        BackColor = Color.FromArgb(119, 82, 246);
        ForeColor = Color.FromArgb(246, 245, 255);
        Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        Cursor = Cursors.Hand;
        Size = new Size(190, 48);
    }

    protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hovered = true; Invalidate(); }
    protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hovered = false; Invalidate(); }
    protected override void OnEnabledChanged(EventArgs e) { base.OnEnabledChanged(e); Invalidate(); }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = new(1, 1, Math.Max(1, Width - 3), Math.Max(1, Height - 3));
        using var path = Rounded(rect, _cornerRadius);
        Color top = Enabled ? BackColor : Color.FromArgb(53, 53, 76);
        Color bottom = Enabled ? GradientColor : Color.FromArgb(45, 44, 66);
        if (_hovered && Enabled)
        {
            top = ControlPaint.Light(top, 0.16f);
            bottom = ControlPaint.Light(bottom, 0.10f);
        }
        using (var grad = new LinearGradientBrush(rect, top, bottom, LinearGradientMode.Horizontal))
            e.Graphics.FillPath(grad, path);
        using (var stroke = new Pen(Enabled ? BorderColor : Color.FromArgb(65, 63, 87), 1.2F))
            e.Graphics.DrawPath(stroke, path);
        TextRenderer.DrawText(e.Graphics, Text, Font, rect,
            Enabled ? ForeColor : Color.FromArgb(140, 139, 165),
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        if (Focused && ShowFocusCues)
            ControlPaint.DrawFocusRectangle(e.Graphics,
                new Rectangle(6, 6, Math.Max(0, Width - 12), Math.Max(0, Height - 12)));
    }

    internal static GraphicsPath Rounded(Rectangle r, int radius)
    {
        var p = new GraphicsPath();
        if (r.Width <= 0 || r.Height <= 0) return p;
        int d = Math.Max(2, Math.Min(radius * 2, Math.Min(r.Width, r.Height)));
        p.AddArc(r.X, r.Y, d, d, 180, 90);
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        p.CloseFigure();
        return p;
    }
}
