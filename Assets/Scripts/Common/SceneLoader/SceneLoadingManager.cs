using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using DesignPatternsMiniGames.Utility;

namespace DesignPatternsMiniGames.Common
{
    public class SceneLoadingManager : MonoBehaviour
    {
        public PropagationField<SceneID> CurrentSceneID = new PropagationField<SceneID>();

        private static SceneLoadingManager _instance;

        public event Action<SceneID> OnSceneLoadFinish;
        public event Action<SceneID> OnSceneLoadStart;
        public PropagationField<bool> LoadingAnimCompleted = new PropagationField<bool>();

        [SerializeField] private SceneLoadingView _loadingView;

        private SceneID _sceneToLoad;

        private void Awake()
        {
            if (_instance != null)
                DestroyImmediate(gameObject);
            else
                Setup();
        }

        private void Setup()
        {
            _instance = this;
            DontDestroyOnLoad(this);

            _loadingView.OnScreenOverlay += SwitchScene;
            _loadingView.OnLoadingAnimComplete += OnLoadingAnimComplete;
        }

        private void SwitchScene()
        {
            SceneManager.LoadScene((int)_sceneToLoad);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            OnSceneLoadFinish?.Invoke(_sceneToLoad);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            CurrentSceneID.Value = _sceneToLoad;
            _loadingView.Hide();
        }

        private void OnLoadingAnimComplete()
        {
            _loadingView.gameObject.SetActive(false);
            LoadingAnimCompleted.Value = true;
        }

        public void LoadScene(SceneID sceneID)
        {
            LoadingAnimCompleted.Value = false;
            OnSceneLoadStart?.Invoke(sceneID);

            _sceneToLoad = sceneID;
            _loadingView.gameObject.SetActive(true);
            _loadingView.Show();
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            _loadingView.OnScreenOverlay -= SwitchScene;
            _loadingView.OnLoadingAnimComplete -= OnLoadingAnimComplete;
        }
    }
}