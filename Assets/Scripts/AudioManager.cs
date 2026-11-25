using System;
using System.Collections.Generic;
using Dino.Utility.Audio;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using AudioType = Dino.Utility.Audio.AudioType;

namespace Dino.Utility.Audio
{
    /// <summary>
    /// Last update 27/04/2025 Dino
    /// A class that allows you to manage audio.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        #region Singleton
        public static AudioManager Instance { get; private set; }
        #endregion
        
        #region serialized fields

        
        
        [Header("Audio Manager Data")] 
        [SerializeField]
        private AudioManagerData _audioManagerData;

        
        [Header("Audio Test")]
        public string _soundNameTest;

        #endregion

        #region private fields

        private GameObject _soundsContainer;
        private List<AudioSource> _audioSources = new List<AudioSource>();

        private List<AudioData> _sfxAudioData = new List<AudioData>();
        private List<AudioData> _musicAudioData = new List<AudioData>();
        private bool _musicPaused;
        private const string MusicVolumePreferenceKey = "MusicVolume";

        #endregion

        public AudioManagerData AudioManagerData
        {
            get => _audioManagerData;
            set => _audioManagerData = value;
        }


        #region unity methods

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            bool shouldPauseMusic = Time.timeScale <= 0f;
            if (shouldPauseMusic == _musicPaused)
            {
                return;
            }

            _musicPaused = shouldPauseMusic;
            foreach (AudioData music in _musicAudioData)
            {
                AudioSource audioSource = FindAudioSource(music.clip);
                if (audioSource == null) continue;

                if (_musicPaused) audioSource.Pause();
                else audioSource.UnPause();
            }
        }

        
        #endregion

        #region private methods
        
       

        private AudioSource FindAudioSource(AudioClip clip)
        {
            return _audioSources.Find(x => x.clip == clip);
        }

        private void Initialize()
        {
            _soundsContainer = new GameObject("SoundsContainer");
            _audioSources = new List<AudioSource>();
            _soundsContainer.transform.SetParent(transform);
            PrepareLists();

            if (PlayerPrefs.HasKey(MusicVolumePreferenceKey))
            {
                UpdateAudioMixerGroupVolume(PlayerPrefs.GetFloat(MusicVolumePreferenceKey), AudioType.Music);
            }

            if (_musicAudioData.Count > 0)
            {
                PlaySound(_musicAudioData[0].name);
            }
        }

        

        private void PrepareLists()
        {
            _sfxAudioData = _audioManagerData.audioData.FindAll(x => x.audioType == AudioType.SFX);
            _musicAudioData = _audioManagerData.audioData.FindAll(x => x.audioType == AudioType.Music);
        }

        private void SetAudioInMixerGroup(AudioSource audioSource, AudioType audioType)
        {
            switch (audioType)
            {
                case AudioType.Music:
                    audioSource.outputAudioMixerGroup = _audioManagerData.MusicAudioMixerGroup;
                    break;
                case AudioType.SFX:
                    audioSource.outputAudioMixerGroup = _audioManagerData.SfxAudioMixerGroup;
                    break;
                case AudioType.Ambience:
                    break;
            }
        }
        
        #endregion


        #region public methdos

        public void PlaySound(string soundName)
        {
            AudioData audioData = _audioManagerData.audioData.Find(x => x.name == soundName);
            if (audioData == null)
            {
                Debug.LogWarning("Sound: " + soundName + " not found!");
                return;
            }

            AudioSource audioSource = FindAudioSource(audioData.clip);

            if (audioSource == null)
            {
                GameObject go = new GameObject("AS : " + audioData.name);
                audioSource = go.AddComponent<AudioSource>();
                audioSource.transform.SetParent(_soundsContainer.transform);
                audioSource.clip = audioData.clip;
                audioSource.loop = audioData.loop;
                audioSource.volume = audioData.volume;
                audioSource.pitch = audioData.pitch;
                SetAudioInMixerGroup(audioSource, audioData.audioType);
                _audioSources.Add(audioSource);
                audioSource.Play();
            }
            else
            {
                audioSource.Play();
            }
        }

        public void StopSound(string soundName)
        {
            AudioData audioData = _audioManagerData.audioData.Find(x => x.name == soundName);
            if (audioData == null)
            {
                Debug.LogWarning("Sound: " + soundName + " not found!");
                return;
            }

            AudioSource audioSource = FindAudioSource(audioData.clip);
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
        
        public void UpdateAudioMixerGroupVolume(float value, AudioType audioType)
        {
            float volume = value;

            switch (audioType)
            {
                case AudioType.Music:
                    _audioManagerData.MusicAudioMixerGroup.audioMixer.SetFloat(_audioManagerData.MusicVolumeParameterName, Mathf.Lerp(-80f, 20f, volume));
                    PlayerPrefs.SetFloat(MusicVolumePreferenceKey, volume);
                    break;
                case AudioType.SFX:
                    _audioManagerData.SfxAudioMixerGroup.audioMixer.SetFloat(_audioManagerData.SfxVolumeParameterName, Mathf.Lerp(-80f, 20f, volume));
                    break;
                case AudioType.Ambience:
                    _audioManagerData.AmbienceAudioMixerGroup.audioMixer.SetFloat(_audioManagerData.AmbienceVolumeParameterName, Mathf.Lerp(-80f, 20f, volume));
                    break;
                
            }
        }

        public float GetMusicVolume()
        {
            if (PlayerPrefs.HasKey(MusicVolumePreferenceKey))
            {
                return PlayerPrefs.GetFloat(MusicVolumePreferenceKey);
            }

            if (_audioManagerData.MusicAudioMixerGroup.audioMixer.GetFloat(
                    _audioManagerData.MusicVolumeParameterName, out float volumeDb))
            {
                return Mathf.InverseLerp(-80f, 20f, volumeDb);
            }

            return 1f;
        }

        #endregion
        
        [Button]
        void TestSound()
        {
            PlaySound(_soundNameTest);
            Debug.Log("Playing sound: " + _soundNameTest);
            
        }
    }
}

namespace Dino.Utility.Audio
{
    public enum AudioType
    {
        Music,
        SFX,
        Ambience
    }


  

}
