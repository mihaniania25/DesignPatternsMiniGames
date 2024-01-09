using System;

namespace DesignPatternsMiniGames.Common
{
    [Serializable]
    public class PlayerModel
    {
        public UserSettingsModel UserSettings;

        public void Init()
        {
            UserSettings ??= new UserSettingsModel();
            UserSettings.Init();
        }
    }
}