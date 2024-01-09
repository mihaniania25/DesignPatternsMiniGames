namespace DesignPatternsMiniGames.Common
{
    public static class AppContext
    {
        public static AppConfigs AppConfigs { get; private set; }
        public static SceneLoadingManager SceneLoadingManager { get; private set; }
        public static WindowsManager WindowsManager { get; private set; }
        public static MiniGameUI MiniGameUI { get; private set; }

        static AppContext()
        {
            SetUp = false;

            AppConfigs = new AppConfigs();
        }

        public static void Setup(AppContextComponents contextComponents)
        {
            SceneLoadingManager = contextComponents.SceneLoadingManager;
            WindowsManager = contextComponents.WindowsManager;
            MiniGameUI = contextComponents.MiniGameUI;

            SetUp = true;
        }
    }
}