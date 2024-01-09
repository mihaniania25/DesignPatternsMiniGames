using UnityEngine;

namespace DesignPatternsMiniGames.Common
{
    public class AppConfigs
    {
        private WindowsConfig _windowsConfig;
        public WindowsConfig WindowsConfig => _windowsConfig ??= Resources.Load<WindowsConfig>(AppResourcesPath.WINDOWS_CONFIG);
    }
}