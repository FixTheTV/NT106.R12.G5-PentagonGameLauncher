using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PentagonLauncher.Launcher.Controls;

/// <summary>
/// A perfectly square 3x3 grid with pieces placed in cell-relative coordinates.
/// Resize in Visual Studio Designer without moving or cropping the X/O marks.
/// </summary>
[ToolboxItem(true)]
public class CaroLogo : Control
{
    private Color _xColor = Color.FromArgb(50, 214, 255);
    private Color _oColor = Color.FromArgb(245, 128, 222);
    private Color _gridColor = Color.FromArgb(103, 109, 206);

    [Category("Caro • Logo")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color XColor { get => _xColor; set { _xColor = value; Invalidate(); } }
    [Category("Caro • Logo")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color OColor { get => _oColor; set { _oColor = value; Invalidate(); } }
    [Category("Caro • Logo")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GridColor { get => _gridColor; set { _gridColor = value; Invalidate(); } }

    public CaroLogo()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        MinimumSize = new Size(28, 28);
        Size = new Size(64, 64);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        float side = Math.Max(1f, Math.Min(Width, Height) - 8f);
        float x = (Width - side) / 2f;
        float y = (Height - side) / 2f;
        float cell = side / 3f;
        float lineWidth = Math.Max(1f, side * .027f);
        using var board = NeonButton.Rounded(Rectangle.Round(new RectangleF(x, y, side, side)), Math.Max(3, (int)(side * .16f)));
        using var bg = new LinearGradientBrush(new RectangleF(x, y, side, side),
            Color.FromArgb(43, 38, 100), Color.FromArgb(19, 22, 59), LinearGradientMode.ForwardDiagonal);
        g.FillPath(bg, board);
        using (var edge = new Pen(Color.FromArgb(164, 118, 245), lineWidth)) g.DrawPath(edge, board);
        using (var gridPen = new Pen(GridColor, lineWidth * .8f))
        {
            for (int i = 1; i < 3; i++)
            {
                float s = i * cell;
                g.DrawLine(gridPen, x + s, y + lineWidth, x + s, y + side - lineWidth);
                g.DrawLine(gridPen, x + lineWidth, y + s, x + side - lineWidth, y + s);
            }
        }
        // Every piece is centered in an individual cell (never absolute pixel offsets).
        DrawX(g, x + cell * .5f, y + cell * .5f, cell * .44f, XColor);
        DrawO(g, x + cell * 2.5f, y + cell * .5f, cell * .28f, OColor);
        DrawX(g, x + cell * 1.5f, y + cell * 1.5f, cell * .41f, XColor);
        DrawO(g, x + cell * .5f, y + cell * 2.5f, cell * .28f, OColor);
    }

    private static void DrawX(Graphics g, float cx, float cy, float arm, Color color)
    {
        using var pen = new Pen(color, Math.Max(2f, arm * .27f))
        { StartCap = LineCap.Round, EndCap = LineCap.Round };
        float r = arm / 2f;
        g.DrawLine(pen, cx-r, cy-r, cx+r, cy+r);
        g.DrawLine(pen, cx+r, cy-r, cx-r, cy+r);
    }
    private static void DrawO(Graphics g, float cx, float cy, float radius, Color color)
    {
        using var pen = new Pen(color, Math.Max(2f, radius * .32f));
        g.DrawEllipse(pen, cx-radius, cy-radius, radius*2f, radius*2f);
    }
}
