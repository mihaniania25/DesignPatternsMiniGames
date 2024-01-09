using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatternsMiniGames.Common
{
    public class MiniGameUI : MonoBehaviour
    {
        private readonly List<SceneID> NOT_MINI_GAME_SCENES = new List<SceneID> { SceneID.Menu };

        public event Action LeaveMiniGameCommand;

        private static MiniGameUI _instance;

        [SerializeField] private Button _pauseMenuButton;
        [SerializeField] private GameObject _panel;

        private bool _setUp = false;
        private MiniGamePauseWindow _pauseWindow;

        private WindowsManager _windowsManager => AppContext.WindowsManager;
        private SceneLoadingManager _sceneLoadingManager => AppContext.SceneLoadingManager;

        private void Awake()
        {
            if (_instance == null)
            {
                Setup();

                DontDestroyOnLoad(this);
                _instance = this;
            }
            else
                DestroyImmediate(gameObject);
        }

        private void Setup()
        {
            _pauseMenuButton.onClick.AddListener(InvokePauseWindow);
            _sceneLoadingManager.CurrentSceneID.Subscribe(OnSceneUpdated);

            _setUp = true;
        }

        private void InvokePauseWindow()
        {
            MiniGamePauseWindowData windowData = new MiniGamePauseWindowData
            {
                OnLeaveAccepted = OnMiniGameQuitAccept
            };

            _pauseWindow = _windowsManager.OpenWindow(WindowType.MiniGamePause, windowData) as MiniGamePauseWindow;
        }

        private void OnSceneUpdated(SceneID sceneID)
        {
            bool isMiniGameScene = NOT_MINI_GAME_SCENES.Contains(sceneID) == false;
            _panel.SetActive(isMiniGameScene);

            if (_pauseWindow != null )
            {
                _pauseWindow.Close();
                _pauseWindow = null;
            }
        }

        private void OnMiniGameQuitAccept()
        {
            LeaveMiniGameCommand?.Invoke();
        }

        private void OnDestroy()
        {
            if (_setUp)
                Dispose();
        }

        private void Dispose()
        {
            _pauseMenuButton.onClick.RemoveListener(InvokePauseWindow);
            _sceneLoadingManager.CurrentSceneID.Unsubscribe(OnSceneUpdated);

            _setUp = false;
        }
    }
}