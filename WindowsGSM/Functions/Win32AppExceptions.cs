using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsGSM.Tools
{
    static class Win32AppExceptions
    {
        private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int pid);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        public static string FindError(Process process, string name)
        {
            string message = null;

            var handle = FindWindow(null, name);
            if (handle == IntPtr.Zero)
            {
                // Not Found
                return message;
            }

            var tid_cl = GetWindowThreadProcessId(handle, out var pid);
            if (process.Id != pid)
            {
                // Not casuse by current srcds.
                return message;
            }

            EnumChildWindows
            (
                handle,
                (hwnd, lparma) =>
                {
                    var sb = new StringBuilder(256);
                    var length = GetWindowTextLength(hwnd);
                    GetWindowText(hwnd, sb, length + 1);

                    if (sb.ToString().Equals("确定") || sb.ToString().Equals("OK"))
                    {
                        //Ignore this.
                        return true;
                    }

                    // save
                    message = sb.ToString();
                    return false;
                },
                IntPtr.Zero
            );

            return message;
        }
    }
}
