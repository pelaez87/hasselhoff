using System;
using System.IO;
using System.Linq;

namespace HasselhoffMaker.Helpers
{
    internal static class FileLocation
    {
        public static string AppPath
        {
            get { return Path.Combine(Path.GetTempPath(), "HasselhoffMaker"); }
        }

        public static string BackupPath
        {
            get { return Path.Combine(AppPath, "Backups"); }
        }

        public static string NewBackupPath
        {
            get
            {
                var date = DateTime.Now;
                return Path.Combine(BackupPath, string.Format("{0}{1}{2}_{3}{4}{5}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second));
            }
        }

        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        public static string GetCustomWallpaper(string wallpaperName)
        {
            var allowedExtensions = new[] { ".bmp", ".jpg", ".jpeg", ".png", ".gif", ".tiff" };

            return Directory.EnumerateFiles(@".", "*.*", SearchOption.TopDirectoryOnly).FirstOrDefault(s => allowedExtensions.Contains(Path.GetExtension(s)));
        }
    }
}