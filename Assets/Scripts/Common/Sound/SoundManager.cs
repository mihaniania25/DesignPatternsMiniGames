using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternsMiniGames.Common
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;

        [SerializeField] private AudioSource _sourceSample;
        [SerializeField] private Transform _sourcesContainer;
        [SerializeField] private int _sourcesCount = 40;

        private bool _setUp = false;
        private int _sourceIndex = 0;
        private List<AudioSource> _sources = new List<AudioSource>();

        private PlayerModel _model => AppContext.Model;

        private void Awake()
        {
            if (_instance == null )
            {
                Setup();

                _instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void Setup()
        {
            _sourceSample.gameObject.SetActive(false);
            CreateSources();

            _setUp = true;
        }

        private void CreateSources()
        {
            for (int i = 0; i < _sourcesCount; i++)
            {
                GameObject sourceGO = Instantiate(_sourceSample.gameObject, _sourcesContainer);
                sourceGO.SetActive(true);

                AudioSource source = sourceGO.GetComponent<AudioSource>();
                _sources.Add(source);
            }
        }

        public void PlaySoundFX(SoundFxData soundData)
        {
            AudioSource audioSource = GetNextAudioSource();
            PlaySoundFX(soundData, audioSource);
        }

        private AudioSource GetNextAudioSource()
        {
            AudioSource nextSource = _sources[_sourceIndex];

            _sourceIndex = (_sourceIndex + 1) % _sources.Count;

            return nextSource;
        }

        public void PlaySoundFX(SoundFxData soundData, AudioSource audioSource)
        {
            if (_model.UserSettings.IsSoundOn.Value == true)
            {
                audioSource.volume = soundData.Volume;
                audioSource.clip = soundData.Clip;

                audioSource.Play();
            }
        }

        private void OnDestroy()
        {
            if (_setUp)
                Dispose();
        }

        private void Dispose()
        {
            foreach (AudioSource source in _sources)
                Destroy(source.gameObject);
            _sources.Clear();

            _setUp = false;
        }
    }
}