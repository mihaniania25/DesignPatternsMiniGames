using UnityEngine;
using UnityEngine.UI;

namespace DesignPatternsMiniGames.Common
{
    public class InfoWindow : Window
    {
        public override bool CloseableViaSubstrate => true;
        public override bool Hideable => true;

        [SerializeField] private Button _okButton;

        protected override void OnOpen(WindowData data)
        {
            _okButton.onClick.AddListener(Close);
        }

        protected override void OnPreHide()
        {
            _okButton.onClick.RemoveListener(Close);
        }
    }
}