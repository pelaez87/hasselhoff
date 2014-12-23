using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace HasselhoffMaker.Helpers
{
    internal static class Wallpaper
    {
        internal const string BackupName = "WallpaperBackup.bmp";
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style
        {
            Tiled,
            Centered,
            Stretched
        }


        public static string Get()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            return key.GetValue(@"Wallpaper").ToString();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 action,
            UInt32 uParam, string vParam, UInt32 winIni);
        private static readonly UInt32 SPI_GETDESKWALLPAPER = 0x73;
        private static uint MAX_PATH = 260;

        public static string GetPath()
        {
            var wallpaper = new string('\0', (int)MAX_PATH);
            SystemParametersInfo(SPI_GETDESKWALLPAPER, MAX_PATH, wallpaper, 0);

            return wallpaper.Substring(0, wallpaper.IndexOf('\0'));
        }

        public static void Set(Style style, string wallpaperPath = null)
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "HasselhoffMaker.Resources.wallpaper.jpg";

            if (string.IsNullOrEmpty(wallpaperPath))
            {
                using (Stream imageStream = assembly.GetManifestResourceStream(resourceName))
                {
                    Image img = Image.FromStream(imageStream);
                    string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                    img.Save(tempPath, ImageFormat.Bmp);

                    SetBackground(style, tempPath);

                    File.Delete(tempPath);
                }
            }
            else
            {
                SetBackground(style, wallpaperPath);
            }
        }

        private static void SetBackground(Style style, string path)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            switch (style)
            {
                case Style.Stretched:
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                case Style.Centered:
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                    break;
                case Style.Tiled:
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 1.ToString());
                    break;
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}