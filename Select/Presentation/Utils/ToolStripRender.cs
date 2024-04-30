using System.Drawing;
using System.Windows.Forms;

namespace Select.Presentation.Utils;

/// <summary>
/// Forces our dark style upon tool strips.
/// </summary>
public class ToolStripRender : ToolStripProfessionalRenderer
{
    public static readonly ToolStripProfessionalRenderer Instance = new ToolStripRender();

    private static readonly SolidBrush Brush = new(Color.FromArgb(61, 61, 61));
    private static readonly Color BackgroundColor = Color.FromArgb(31, 31, 31);
    private static readonly Pen Pen = new(Color.FromArgb(121, 121, 121));

    public ToolStripRender()
    {
        RoundedEdges = false;
    }

    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        // base.OnRenderToolStripBorder(e);
    }

    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
    {
        if (!e.Item.Selected)
        {
            base.OnRenderButtonBackground(e);
        }
        else
        {
            var rectangle = new Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1);
            e.Graphics.FillRectangle(Brush, rectangle);
            e.Graphics.DrawRectangle(Pen, rectangle);
        }
    }

    public static void Configure(ToolStrip ts)
    {
        ts.BackColor = BackgroundColor;
        ts.Renderer = Instance;
    }
}
