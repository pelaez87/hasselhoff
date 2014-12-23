using System;
using System.Collections.Generic;
using HasselhoffMaker.Systems;
using HasselhoffMaker.Systems.Core;

namespace HasselhoffMaker
{
    internal class HasselhoffFactory : IHasselhoffTweak
    {
        #region Members

        private static readonly Dictionary<eSystem, IHasselhoffTweak> _strategies = new Dictionary<eSystem, IHasselhoffTweak>();
        private eSystem _currentSystem;

        #endregion

        #region Constructors

        static HasselhoffFactory()
        {
            _strategies.Add(eSystem.Windows7, new Windows7());
            _strategies.Add(eSystem.Windows8, new Windows8());
            _strategies.Add(eSystem.Windows8_1, new Windows8());
            _strategies.Add(eSystem.Windows10, new Windows10());
        }

        public HasselhoffFactory(eSystem system)
        {
            if (_strategies.ContainsKey(system))
                _currentSystem = system;
            else
                throw new ArgumentException("Operating System");
        }

        #endregion

        #region Tweak Methods

        public void ChangeBackground()
        {
            _strategies[_currentSystem].ChangeBackground();
        }

        public void ChangeBackgroundWithDesktopCapture()
        {
            _strategies[_currentSystem].ChangeBackgroundWithDesktopCapture();
        }

        public void ChangeBackgroundWithCustomWallpaper()
        {
            _strategies[_currentSystem].ChangeBackgroundWithCustomWallpaper();
        }

        public void ChangeBootscreen()
        {
            _strategies[_currentSystem].ChangeBootscreen();
        }

        public void HideDesktopIcons()
        {
            _strategies[_currentSystem].HideDesktopIcons();
        }

        public void DisableDesktop()
        {
            _strategies[_currentSystem].DisableDesktop();
        }

        public void RotateScreen()
        {
            _strategies[_currentSystem].RotateScreen();
        }

        public void HideTaskbar()
        {
            _strategies[_currentSystem].HideTaskbar();
        }

        public void LockSession()
        {
            _strategies[_currentSystem].LockSession();
        }

        public void ChangeMouseButtonsConfiguration()
        {
            _strategies[_currentSystem].ChangeMouseButtonsConfiguration();
        }

        public void MakeDesktopScreenshot()
        {
            _strategies[_currentSystem].MakeDesktopScreenshot();
        }

        #endregion

        #region Security Methods

        public void BackupCurrentSettings()
        {
            _strategies[_currentSystem].BackupCurrentSettings();
        }

        public void RestoreSettings()
        {
            _strategies[_currentSystem].RestoreSettings();
        }

        #endregion
    }
}