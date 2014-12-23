
namespace HasselhoffMaker.Helpers
{
    internal abstract class Options
    {
        //Grouped Modes
        internal const string Basic = "BASIC";
        internal const string Medium = "MEDIUM";
        internal const string Complete = "COMPLETE";
        internal const string Full = "FULL";

        //Basic Options
        internal const string PresetBackground = "PRESETBACKGROUND";
        internal const string DesktopBackground = "DESKTOPBACKGROUND";
        internal const string DesktopCustomBackground = "DESKTOPCUSTOMBACKGROUND";
        internal const string BootScreen = "BOOTSCREEN";
        internal const string HideIcons = "HIDEICONS";
        internal const string RotateScreen = "ROTATE";
        internal const string MouseButtons = "MOUSE";
        internal const string DesktopDisable = "DESKTOPDISABLE";
        internal const string DesktopScreenshot = "DESKTOPSCREENSHOT";
        internal const string Lock = "LOCK";
        internal const string HideTaskbar = "HIDETASKBAR";
#if GODMODE
        //Reserved Options
        internal const string ActivateGodMode = "GODON";
        internal const string DeactivateGodMode = "GODOFF";
#endif

        //Utilities
        internal const string Restore = "RESTORE";
    }
}