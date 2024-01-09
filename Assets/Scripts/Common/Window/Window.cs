using System;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatternsMiniGames.Common
{
    public class Window : MonoBehaviour
    {
        private const string OPEN_ANIMPARAM = "Open";

        public event Action<Window> OnClose;

        [SerializeField] private WindowType _windowType;
        [SerializeField] private Button _closeButton;
        [SerializeField] protected Animator _animator;

        public WindowType WindowType => _windowType;
        public virtual bool Hideable => true;
        public virtual bool CloseableViaSubstrate => true;
        public bool IsOpened { get; private set; }

        private WindowData _data;

        public void Open(WindowData data)
        {
            _data = data;
            OnOpen(data);

            gameObject.SetActive(true);
            IsOpened = true;

            _animator?.SetTrigger(OPEN_ANIMPARAM);

            if (_closeButton != null)
                _closeButton.onClick.AddListener(Close);
        }

        protected virtual void OnOpen(WindowData data)
        {

        }

        public void Close()
        {
            Hide();
            OnClose?.Invoke(this);

            if (_data != null)
                _data.OnClose?.Invoke();
        }

        public void Hide()
        {
            OnPreHide();
            IsOpened = false;
            gameObject.SetActive(false);

            if (_closeButton != null)
                _closeButton.onClick.RemoveListener(Close);
        }

        protected virtual void OnPreHide()
        {

        }
    }
}