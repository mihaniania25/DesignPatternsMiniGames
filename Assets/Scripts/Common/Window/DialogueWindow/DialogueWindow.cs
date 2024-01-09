using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatternsMiniGames.Common
{
    public class DialogueWindow : Window
    {
        public override bool CloseableViaSubstrate => true;
        public override bool Hideable => true;

        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private TextMeshProUGUI _titleLabel;
        [SerializeField] private TextMeshProUGUI _descriptionLabel;

        private Action _cancelCallback;
        private DialogueWindowData _winData;

        protected override void OnOpen(WindowData data)
        {
            _winData = data as DialogueWindowData;

            _titleLabel.text = _winData.Title;
            _descriptionLabel.text = _winData.Description;

            _yesButton.onClick.AddListener(OnYesButtonClick);
            _cancelCallback = _winData.OnCancel;
            _noButton.onClick.AddListener(Close);
        }

        private void OnYesButtonClick()
        {
            _winData.OnAccept?.Invoke();
            _cancelCallback = null;
            Close();
        }

        protected override void OnPreHide()
        {
            _cancelCallback?.Invoke();
            _cancelCallback = null;
            _yesButton.onClick.RemoveListener(OnYesButtonClick);
            _noButton.onClick.RemoveListener(Close);
        }
    }
}