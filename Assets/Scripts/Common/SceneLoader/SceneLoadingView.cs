using System;
using UnityEngine;

namespace DesignPatternsMiniGames.Common
{
    public class SceneLoadingView : MonoBehaviour
    {
        private const string SHOW_ANIMPARAM = "Show";
        private const string HIDE_ANIMPARAM = "Hide";

        public event Action OnScreenOverlay;
        public event Action OnLoadingAnimComplete;

        [SerializeField] private GameObject _screenLocker;
        [SerializeField] private Animator _animator;

        public void Show()
        {
            _screenLocker.SetActive(true);

            _animator.SetTrigger(SHOW_ANIMPARAM);
        }

        public void ShowComplete()
        {
            OnScreenOverlay?.Invoke();
        }

        public void Hide()
        {
            _animator.SetTrigger(HIDE_ANIMPARAM);
            _screenLocker.SetActive(false);
        }

        public void HideComplete()
        {
            OnLoadingAnimComplete?.Invoke();
        }
    }
}