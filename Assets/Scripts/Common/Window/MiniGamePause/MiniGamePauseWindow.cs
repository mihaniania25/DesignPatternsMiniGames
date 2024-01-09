using System;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatternsMiniGames.Common
{
    public class MiniGamePauseWindow : Window
    {
        public override bool Hideable => false;
        public override bool CloseableViaSubstrate => false;

        [SerializeField] private Button _soundSwitchButton;
        [SerializeField] private Button _exitMiniGameButton;

        private Action _leavingMiniGameCallback;

        private WindowsManager _windowsManager => AppContext.WindowsManager;

        protected override void OnOpen(WindowData data)
        {
            MiniGamePauseWindowData windowData = data as MiniGamePauseWindowData;

            if (windowData == null)
                return;

            _leavingMiniGameCallback = windowData.OnLeaveAccepted;
            _soundSwitchButton.onClick.AddListener(SwitchSound);
            _exitMiniGameButton.onClick.AddListener(TryExitMiniGame);
        }

        private void SwitchSound()
        {
#warning SWITCH SOUND NOT IMPLEMENTED!
        }

        private void TryExitMiniGame()
        {
            _windowsManager.OpenWindow(WindowType.Dialogue, new DialogueWindowData
            {
                Title = "",
                Description = "",
                OnAccept = OnExitAccepted,
            });
        }

        private void OnExitAccepted()
        {
            _leavingMiniGameCallback?.Invoke();
        }

        protected override void OnPreHide()
        {
            _leavingMiniGameCallback = null;

            _soundSwitchButton.onClick.RemoveListener(SwitchSound);
            _exitMiniGameButton.onClick.RemoveListener(TryExitMiniGame);
        }
    }
}