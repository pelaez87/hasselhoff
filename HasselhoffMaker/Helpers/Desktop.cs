using System;
using System.Runtime.InteropServices;

namespace HasselhoffMaker.Helpers
{
    internal static class Desktop
    {
        private const int GW_CHILD = 5;
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;


        [DllImport("user32.dll", EntryPoint = "FindWindowA")]
        private static extern long FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern long GetWindow(long hwnd, long wCmd);

        [DllImport("user32.dll")]
        private static extern long ShowWindow(long hwnd, long nCmdShow);

        [DllImport("user32.dll")]
        private static extern long EnableWindow(long hwnd, long fEnable);

        public static void ShowDesktopIcons(bool bVisible)
        {
            long hWnd_DesktopIcons = GetWindow(FindWindow("Progman", "Program Manager"), GW_CHILD);
            if (bVisible)
            {
                ShowWindow(hWnd_DesktopIcons, SW_SHOW);
            }
            else
            {
                ShowWindow(hWnd_DesktopIcons, SW_HIDE);
            }
        }

        public static void EnableDesktop(bool bEnable)
        {
            long hWnd_Desktop = GetWindow(FindWindow("Progman", "Program Manager"), GW_CHILD);
            EnableWindow(hWnd_Desktop, Convert.ToInt64(bEnable));
        }
    }
}