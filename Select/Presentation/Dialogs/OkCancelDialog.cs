using System.Windows.Forms;

namespace Select.Presentation.Dialogs;

public partial class OkCancelDialog : Form
{
    public OkCancelDialog()
    {
        InitializeComponent();
    }

    public static bool ShowDialog(string caption, string text)
    {
        using var dialog = new OkCancelDialog
        {
            Text = caption,
            lblTitle = { Text = caption },
            lblPrompt = { Text = text },
        };

        return dialog.ShowDialog() == DialogResult.OK;
    }
}
