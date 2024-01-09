using System;
using UnityEngine;

namespace DesignPatternsMiniGames.Common
{
    [Serializable]
    public class SoundFxData
    {
        public AudioClip Clip;

        [Range(0f, 1f)]
        private float _volume;

        public float Volume => _volume;
    }
}