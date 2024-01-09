using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternsMiniGames.Common
{
    public class MiniGameSelector : MonoBehaviour
    {
        [SerializeField] private List<MiniGameSelectItem> _selectItems;

        private SceneLoadingManager _sceneLoader => AppContext.SceneLoadingManager;

        private void Start()
        {
            _selectItems.ForEach(i => i.OnMiniGameSelected += LaunchMiniGame);
        }

        private void LaunchMiniGame(MiniGameSelectItem miniGameSelectItem)
        {
            _sceneLoader.LoadScene(miniGameSelectItem.SceneID);
        }

        private void OnDestroy()
        {
            _selectItems.ForEach(i => i.OnMiniGameSelected -= LaunchMiniGame);
        }
    }
}