using System;
using System.Windows.Forms;

namespace Select.Presentation.Utils;

/// <summary>
/// Useful extensions for controls.
/// </summary>
public static class ControlExtensions
{
    /// <summary>
    /// Executes the Action on the UI thread.
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
}
