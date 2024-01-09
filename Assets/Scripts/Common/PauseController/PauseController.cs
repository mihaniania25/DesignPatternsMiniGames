using UnityEngine;

namespace DesignPatternsMiniGames.Common
{
    public class PauseController
    {
        private WindowsManager _windowsManager => AppContext.WindowsManager;

        public void Setup()
        {
            _windowsManager.OnWindowOpened += OnWindowOpened;
            _windowsManager.OnWindowClosed += OnWindowClosed;
        }

        private void OnWindowOpened(Window obj)
        {
            Pause();
        }

        private void OnWindowClosed(Window obj)
        {
            if (_windowsManager.ActiveWindowsCount == 0)
                UnPause();
        }

        private void Pause()
        {
            Time.timeScale = 0;
        }

        private void UnPause()
        {
            Time.timeScale = 1;
        }

        public void Dispose()
        {
            _windowsManager.OnWindowOpened -= OnWindowOpened;
            _windowsManager.OnWindowClosed -= OnWindowClosed;
        }
    }
}