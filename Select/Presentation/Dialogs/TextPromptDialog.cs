using System.Windows.Forms;

namespace Select.Presentation.Dialogs;
public partial class TextPromptDialog : Form
{
    public TextPromptDialog()
    {
        InitializeComponent();
    }

    public static (bool ok, string value) ShowDialog(string caption, string text, string value = "")
    {
        using var dialog = new TextPromptDialog();
        
        dialog.Text = caption;
        dialog.lblPrompt.Text = text;
        dialog.txtPrompt.Text = value;

        var result = dialog.ShowDialog();
        var prompt = dialog.txtPrompt.Text.Trim();

        return result == DialogResult.OK && prompt.Length > 0
            ? (true, prompt)
            : (false, "");
    }
}
