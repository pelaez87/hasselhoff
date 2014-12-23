
namespace HasselhoffMaker
{
    internal interface IHasselhoffTweak
    {
        //Tweaks
        void ChangeBackground();
        void ChangeBackgroundWithDesktopCapture();
        void ChangeBackgroundWithCustomWallpaper();
        void ChangeBootscreen();
        void HideDesktopIcons();
        void DisableDesktop();
        void RotateScreen();
        void ChangeMouseButtonsConfiguration();
        void MakeDesktopScreenshot();
        void LockSession();
        void HideTaskbar();

        //Utilities
        void BackupCurrentSettings();
        void RestoreSettings();
    }
}