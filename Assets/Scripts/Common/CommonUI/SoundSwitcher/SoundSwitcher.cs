using UnityEngine;
using UnityEngine.UI;

namespace DesignPatternsMiniGames.Common
{
    public class SoundSwitcher : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _enabledView;
        [SerializeField] private GameObject _disabledView;

        private PlayerModelHandler _modelHandler => AppContext.ModelHandler;
        private PlayerModel _model => _modelHandler.Model;
        private UserSettingsModel _settingsModel => _model.UserSettings;

        private void Awake()
        {
            _button.onClick.AddListener(SwitchSound);
            _settingsModel.IsSoundOn.Subscribe(OnSoundOnUpdated);
        }

        private void SwitchSound()
        {
            _settingsModel.IsSoundOn.Value = !_settingsModel.IsSoundOn.Value;
            _modelHandler.SaveModel();
        }

        private void OnSoundOnUpdated(bool isSoundOn)
        {
            _enabledView.SetActive(isSoundOn);
            _disabledView.SetActive(isSoundOn == false);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(SwitchSound);
            _settingsModel.IsSoundOn.Unsubscribe(OnSoundOnUpdated);
        }
    }
}