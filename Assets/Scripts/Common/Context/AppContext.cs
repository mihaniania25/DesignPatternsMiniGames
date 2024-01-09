namespace DesignPatternsMiniGames.Common
{
    public static class AppContext
    {
        public static AppConfigs AppConfigs { get; private set; }
        public static PlayerModelHandler ModelHandler { get; private set; }
        public static PlayerModel Model => ModelHandler.Model;

        public static SceneLoadingManager SceneLoadingManager { get; private set; }
        public static WindowsManager WindowsManager { get; private set; }
        public static MiniGameUI MiniGameUI { get; private set; }
        public static SoundManager SoundManager { get; private set; }

        static AppContext()
        {
            ModelHandler = new PlayerModelHandler();
            ModelHandler.Init();

            AppConfigs = new AppConfigs();
        }

        public static void Setup(AppContextComponents contextComponents)
        {
            SceneLoadingManager = contextComponents.SceneLoadingManager;
            WindowsManager = contextComponents.WindowsManager;
            MiniGameUI = contextComponents.MiniGameUI;
            SoundManager = contextComponents.SoundManager;
        }
    }
}