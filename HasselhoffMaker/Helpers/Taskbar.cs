using System.Runtime.InteropServices;

namespace HasselhoffMaker.Helpers
{
    /// <summary>
    /// Provides functions to capture the entire screen, or a particular window, and save it to a file.
    /// </summary>
    internal static class Taskbar
    {
        [DllImport("user32.dll")]
        private static extern int FindWindowEx(int parent, int afterWindow, string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        public static void HideTaskbar(bool hide)
        {
            int option = hide ? SW_HIDE : SW_SHOW;
            // Start with the first child, then continue with windows of the same class after it
            int hWnd = 0;
            while ((hWnd = FindWindowEx(0, hWnd, "Shell_TrayWnd", "")) != 0)
            {
                ShowWindow(hWnd, option);
            }
        }
    }
}