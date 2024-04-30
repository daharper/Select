using System;
using System.Windows.Forms;

namespace Select.Presentation.Utils;

/// <summary>
/// Useful extensions for controls.
/// </summary>
public static class ControlExtensions
{
    /// <summary>
    /// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
    /// </summary>
    /// <param name="control">the control for which the update is required</param>
    /// <param name="action">action to be performed on the control</param>
    public static void InvokeOnUiThreadIfRequired(this Control control, Action action)
    {
        if (control.Disposing || control.IsDisposed || !control.IsHandleCreated) return;

        if (control.InvokeRequired)
        {
            control.BeginInvoke(action);
        }
        else
        {
            action.Invoke();
        }
    }

    public static void AddToolTip(this Control control, string text, string title = "")
    {
        var toolTip = new ToolTip
        {
            ToolTipIcon = ToolTipIcon.None,
            IsBalloon = true,
            ShowAlways = true,
        };

        if (!string.IsNullOrEmpty(title))
        {
            toolTip.ToolTipTitle = title;
        }

        toolTip.SetToolTip(control, text);
    }

    public static T? GetTag<T>(this ListViewItem control)
        => control.Tag is not T tag ? default : tag;
    
    public static (string, string) GetTextTag(this ListViewItem control)
        => control.Tag is not string tag ? (control.Text, "") : (control.Text, tag);
    
    public static (string, T?) GetTextTag<T>(this ListViewItem control)
        => control.Tag is not T tag ? (control.Text, default) : (control.Text, tag);

    public static (string, string) GetSelectedTextTag(this ListView view)
    {
        if (view.SelectedItems.Count == 0)
        {
            return ("", "");
        }
        
        var item = view.SelectedItems[0];
        
        return item.Tag is not string tag ? (item.Text, "") : (item.Text, tag);
    }

    public static (string, T?) GetSelectedTextTag<T>(this ListView view)
    {
        if (view.SelectedItems.Count == 0)
        {
            return ("", default);
        }

        var item = view.SelectedItems[0];

        return item.Tag is not T tag ? (item.Text, default) : (item.Text, tag);
    }
}
