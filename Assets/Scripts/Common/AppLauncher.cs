using UnityEngine;

namespace DesignPatternsMiniGames.Common
{
    public class AppLauncher : MonoBehaviour
    {
        private static AppLauncher _instance;

        [SerializeField] private SceneLoadingManager _sceneLoader;
        [SerializeField] private WindowsManager _windowsManager;
        [SerializeField] private MiniGameUI _miniGameUI;

        private MiniGamesDirector _miniGamesDirector = new MiniGamesDirector();
        private bool _setUp = false;

        private void Awake()
        {
            if (_instance == null)
            {
                Setup();

                _instance = this;
                DontDestroyOnLoad(this);
            }
            else
                DestroyImmediate(this);
        }

        private void Setup()
        {
            AppContext.Setup(new AppContextComponents
            {
                SceneLoadingManager = _sceneLoader,
                WindowsManager = _windowsManager,
                MiniGameUI = _miniGameUI
            });

            _miniGamesDirector.Setup();
            _setUp = true;
        }

        private void OnDestroy()
        {
            if (_setUp)
                Dispose();
        }

        private void Dispose()
        {
            _miniGamesDirector.Dispose();

            _setUp = false;
        }
    }
}