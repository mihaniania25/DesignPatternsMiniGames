using System;
using UnityEngine;
using DesignPatternsMiniGames.Utility;

namespace DesignPatternsMiniGames.Common
{
    [Serializable]
    public class UserSettingsModel
    {
        [SerializeField] private PropagationField<bool> _isSoundOn;

        public PropagationField<bool> IsSoundOn => _isSoundOn;

        public void Init()
        {
            _isSoundOn ??= new PropagationField<bool>(true);
        }
    }
}