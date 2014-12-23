using System;
using System.Drawing;
using System.IO;
using System.Linq;
using HasselhoffMaker.Helpers;
using System.Drawing.Imaging;

namespace HasselhoffMaker.Systems.Core
{
    internal abstract class BaseWindows : IHasselhoffTweak
    {
        protected const string CustomWallpaperName = "custom";

        public virtual void ChangeBackground()
        {
            Wallpaper.Set(Wallpaper.Style.Stretched);
        }

        public void ChangeBackgroundWithDesktopCapture()
        {
            ScreenCapture.CaptureDesktopToBackground();
        }

        public void ChangeBackgroundWithCustomWallpaper()
        {
            Wallpaper.Set(Wallpaper.Style.Stretched, FileLocation.GetCustomWallpaper(CustomWallpaperName));
        }

        public virtual void ChangeBootscreen()
        {
            //TODO
        }

        public virtual void HideDesktopIcons()
        {
            Desktop.ShowDesktopIcons(false);
        }

        public virtual void DisableDesktop()
        {
            Desktop.EnableDesktop(false);
        }

        public virtual void RotateScreen()
        {
            //TODO
        }

        public virtual void ChangeMouseButtonsConfiguration()
        {
            Mouse.SwapMouseButton(1);
        }

        public virtual void MakeDesktopScreenshot()
        {
            ScreenCapture.CaptureDesktopToFile("test.bmp", ImageFormat.Bmp);
        }

        public virtual void HideTaskbar()
        {
            Taskbar.HideTaskbar(true);
        }

        public void LockSession()
        {
            ConsoleCommand.ExecuteCommandSync("RunDll32.exe user32.dll,LockWorkStation");
        }

        public virtual void BackupCurrentSettings()
        {
            var currentBackupFolder = FileLocation.NewBackupPath;

            //Try to resolve wallpaper location
            var wallpaper = Wallpaper.Get();
            if (string.IsNullOrEmpty(wallpaper))
                wallpaper = Wallpaper.GetPath();

            if (!string.IsNullOrEmpty(wallpaper) && File.Exists(wallpaper))
            {
                FileLocation.CreateFolder(currentBackupFolder);
                Image img = Image.FromFile(wallpaper);
                img.Save(Path.Combine(currentBackupFolder, Wallpaper.BackupName), ImageFormat.Bmp);
            }
            else
                throw new Exception("Can not backup current settings");
            //TODO: Backup current boot screen
        }

        public virtual void RestoreSettings()
        {
            var lastBackupFolder = Directory.GetDirectories(FileLocation.BackupPath).ToList().LastOrDefault();
            if (!string.IsNullOrEmpty(lastBackupFolder))
            {
                var wallpaperFile = Path.Combine(lastBackupFolder, Wallpaper.BackupName);
                if (File.Exists(wallpaperFile))
                    Wallpaper.Set(Wallpaper.Style.Stretched, wallpaperFile);
            }

            //TODO: Restore current boot screen
            //TODO: Unrotate screen
            Desktop.ShowDesktopIcons(true);
            Desktop.EnableDesktop(true);
            Mouse.SwapMouseButton(0);
            Taskbar.HideTaskbar(false);
        }
    }
}