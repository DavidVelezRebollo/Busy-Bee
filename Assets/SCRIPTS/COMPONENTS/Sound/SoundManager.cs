using GOM.Classes.Sound;
using GOM.Shared;
using UnityEngine.Audio;
using UnityEngine;
using System;

namespace GOM.Components.Sounds {
    public class SoundManager : MonoBehaviour {

        #region Public Fields

        [Tooltip("Instance of SoundManager, so it can be accessed from other classes.")]
        public static SoundManager Instance;

        [Tooltip("Group of general sounds. SerializeField: you can change it from the editor.")]
        [SerializeField] private AudioMixerGroup GeneralMixerGroup;
        [Tooltip("Group of Music type sounds. SerializeField: you can change it from the editor.")]
        [SerializeField] private AudioMixerGroup MusicMixerGroup;
        [Tooltip("Group of SoundEffect type sounds. SerializeField: you can change it from the editor.")]
        [SerializeField] private AudioMixerGroup SoundEffectsMixerGroup;
        [Tooltip("Array of sounds. SerializeField: you can change it from the editor.")]
        [SerializeField] private Sound[] Sounds;

        #endregion

        #region Unity Events

        private void Awake() {
            if (Instance != null) return;
            
            Instance = this;

            foreach (Sound s in Sounds) {
                s.Source = gameObject.AddComponent<AudioSource>();
                s.Source.clip = s.AudioClip;
                s.Source.loop = s.IsLoop;
                s.Source.volume = s.Volume;

                switch (s.AudioType) {
                    case SoundType.SoundEffect:
                        s.Source.outputAudioMixerGroup = SoundEffectsMixerGroup;
                        break;

                    case SoundType.Music:
                        s.Source.outputAudioMixerGroup = MusicMixerGroup;
                        break;
                }

                if (s.PlayOnAwake)
                    s.Source.Play();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// It checks if the settings of the mixers have been changed and if so, it assigns it values. 
        /// If not, it initializes the mixers volume.
        /// </summary>
        private void Start() {
            if (PlayerPrefs.HasKey("GeneralVolume") && PlayerPrefs.HasKey("SoundEffectsVolume") &&
                PlayerPrefs.HasKey("MusicVolume"))
                LoadVolume();
            else {
                PlayerPrefs.SetFloat("GeneralVolume", 0);
                PlayerPrefs.SetFloat("SoundEffectsVolume", 0);
                PlayerPrefs.SetFloat("MusicVolume", 0);
            }
        }

        /// <summary>
        /// Loads the volume of the mixers.
        /// </summary>
        private void LoadVolume() {
            GeneralMixerGroup.audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("GeneralVolume"));
            SoundEffectsMixerGroup.audioMixer.SetFloat("SoundEffects", PlayerPrefs.GetFloat("SoundEffectsVolume"));
            MusicMixerGroup.audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));
        }

        /// <summary>
        /// Finds the clip named "clipname" and plays it if it exists.
        /// </summary>
        /// <param name="clipName">Name of the clip to Play.</param>
        public void Play(string clipName) {
            Sound s = Array.Find(Sounds, dummySound => dummySound.ClipName == clipName);
            if (s == null) {
                Debug.LogError("Sound " + clipName + " not found");
                return;
            }
            s.Source.Play();
        }

        /// <summary>
        /// Checks if a sound clip is playing
        /// </summary>
        /// <param name="clipName">Name of the clip</param>
        /// <returns>True if is playing. False otherwise.</returns>
        public bool IsPlaying(string clipName) {
            Sound s = Array.Find(Sounds, dummySound => dummySound.ClipName == clipName);
            if (s != null)
                return s.Source.isPlaying;

            Debug.LogError("Sound " + clipName + " not found");
            return false;

        }

        /// <summary>
        /// Resumes a sound clip
        /// </summary>
        /// <param name="clipName">Name of the clip to resume</param>
        public void Resume(string clipName) {
            Sound s = Array.Find(Sounds, dummySound => dummySound.ClipName == clipName);
            if (s == null) {
                Debug.LogError("Sound " + clipName + " not found");
                return;
            }
            s.Source.UnPause();
        }

        /// <summary>
        /// Pauses a sound clip
        /// </summary>
        /// <param name="clipName">Name of the clip to pause</param>
        public void Pause(string clipName) {
            Sound s = Array.Find(Sounds, dummySound => dummySound.ClipName == clipName);
            if (s == null) {
                Debug.LogError("Sound " + clipName + " not found");
                return;
            }
            s.Source.Pause();
        }

        /// <summary>
        /// Finds the clip named "clipname" and stops it if it exists.
        /// </summary>
        /// <param name="clipName">Name of the clip to Stop.</param>
        public void Stop(string clipName) {
            Sound s = Array.Find(Sounds, dummySound => dummySound.ClipName == clipName);
            if (s == null) {
                Debug.LogError("Sound " + clipName + " not found");
                return;
            }
            s.Source.Stop();
        }

        #endregion
    }
}
