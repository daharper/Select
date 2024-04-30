using System.Drawing;
using System.Windows.Forms;

namespace Select.Presentation.Utils;

/// <summary>
/// Used for enforcing our dark style upon menu items.
/// </summary>
public class MenuColorTable : ProfessionalColorTable
{
    public static readonly MenuColorTable Instance = new MenuColorTable();

    public override Color ToolStripDropDownBackground => Color.FromArgb(46, 46, 46);

    public override Color ImageMarginGradientBegin => Color.FromArgb(46, 46, 46);

    public override Color ImageMarginGradientMiddle => Color.FromArgb(46, 46, 46);

    public override Color ImageMarginGradientEnd => Color.FromArgb(46, 46, 46);

    public override Color MenuBorder => Color.FromArgb(112, 112, 112);

    public override Color MenuItemBorder => Color.FromArgb(112, 112, 112);

    public override Color MenuItemSelected => Color.FromArgb(61, 61, 61);

    public override Color MenuStripGradientBegin => Color.FromArgb(46, 46, 46);

    public override Color MenuStripGradientEnd => Color.FromArgb(46, 46, 46);

    public override Color MenuItemSelectedGradientBegin => Color.FromArgb(61, 61, 61);

    public override Color MenuItemSelectedGradientEnd => Color.FromArgb(61, 61, 61);

    public override Color MenuItemPressedGradientBegin => Color.FromArgb(46, 46, 46);

    public override Color MenuItemPressedGradientEnd => Color.FromArgb(46, 46, 46);
}

