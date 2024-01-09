using System.Collections.Generic;
using UnityEngine;
using DesignPatternsMiniGames.Utility;

namespace DesignPatternsMiniGames.Common
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Config/WindowsConfig")]
    public class WindowsConfig : ScriptableObject
    {
        [SerializeField] private List<Window> _data;

        public GameObject GetWindowPrefab(WindowType windowType)
        {
            GameObject windowPrefab = null;

            Window prefabView = _data.Find(d => d != null && d.WindowType == windowType);

            if (prefabView != null)
                windowPrefab = prefabView.gameObject;

            if (windowPrefab == null)
                GameLog.Error("[WindowsConfig] failed to find prefab for " + windowType);

            return windowPrefab;
        }
    }
}