using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Select.Presentation.Utils;

/// <summary>
/// Forces dark mode on a window.
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public static class WindowThemer
{
    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    private const int DwmwaUseImmersiveDarkModeBefore20H1 = 19;
    private const int DwmwaUseImmersiveDarkMode = 20;

    public static bool SetDarkModeByForce(IntPtr handle)
    {
        var enabled = 1;
        
        return DwmSetWindowAttribute(handle, DwmwaUseImmersiveDarkMode, ref enabled, sizeof(int)) == 0;
    }

    public static bool SetDarkMode(IntPtr handle)
    {
        if (IsWindows10OrGreater(17763))
        {
            var attribute = DwmwaUseImmersiveDarkModeBefore20H1;

            if (IsWindows10OrGreater(18985))
            {
                attribute = DwmwaUseImmersiveDarkMode;
            }

            var enabled = 1;
            return DwmSetWindowAttribute(handle, attribute, ref enabled, sizeof(int)) == 0;
        }

        return false;
    }

    private static bool IsWindows10OrGreater(int build = -1)
        => Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
}

