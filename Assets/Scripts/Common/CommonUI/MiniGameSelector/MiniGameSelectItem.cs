using System;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatternsMiniGames.Common
{
    public class MiniGameSelectItem : MonoBehaviour
    {
        public event Action<MiniGameSelectItem> OnMiniGameSelected;

        [SerializeField] private Button _button;
        [SerializeField] private SceneID _sceneID;

        public SceneID SceneID => _sceneID;

        private void Awake()
        {
            _button.onClick.AddListener(OnSelectButtonClicked);
        }

        private void OnSelectButtonClicked()
        {
            OnMiniGameSelected?.Invoke(this);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnSelectButtonClicked);
        }
    }
}