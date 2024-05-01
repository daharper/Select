using System.Windows.Forms;

namespace Select.Presentation.Dialogs;

public partial class OkCancelDialog : Form
{
    public OkCancelDialog() => InitializeComponent();

    public static bool ShowDialog(string caption, string text)
    {
        using var dialog = new OkCancelDialog();
       
        dialog.Text = caption;
        dialog.lblTitle.Text = caption;
        dialog.lblPrompt.Text = text;

        return dialog.ShowDialog() == DialogResult.OK;
    }
}
