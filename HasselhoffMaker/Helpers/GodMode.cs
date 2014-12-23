using Microsoft.Win32;

namespace HasselhoffMaker.Helpers
{
    internal static class GodMode
    {
        static readonly RegistryKey RegistryKeyPath = Registry.CurrentUser.OpenSubKey(KeyPath, true);
        private const string KeyPath = @"Software\HasselhoffMaker";
        private const string KeyName = "GodMode";

        public static bool IsInstalled
        {
            get { return RegistryKeyPath != null && RegistryKeyPath.GetValue(KeyName) != null; }
        }

        public static void Install()
        {
            var registryKey = Registry.CurrentUser.CreateSubKey(KeyPath);
            if (registryKey != null)
                registryKey.SetValue(KeyName, true, RegistryValueKind.DWord);
        }

        public static void Uninstall()
        {
            if (IsInstalled)
                Registry.CurrentUser.DeleteSubKeyTree(KeyPath);
        }
    }
}