using System;
using _D.Scripts.Audio;
using DG.Tweening;
using UnityEngine;
using FMODUnity;

namespace _game.Scripts.Audio
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        
        [SerializeField] private float musicVolume = 1;
        [SerializeField] private float sfxVolume = 1;
        [SerializeField] private float fadeInDuration = 5;
        [SerializeField] private FMODEventPlayable mainMenuMusic;
        [Space(10)]
        [SerializeField] private RuntimeManager runtimeManager;
        [SerializeField] private StudioEventEmitter buttonClickSound;
        [SerializeField] private StudioEventEmitter musicEmitter;
        
        private Tween _musicFadeTween;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            //audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            musicEmitter.Play();
        }

        public void PlayButtonClickSound()
        {
            if(buttonClickSound == null) return;
            buttonClickSound.Play();
            //audioSource.PlayOneShot(buttonClickSound.AudioClip, buttonClickSound.Volume * sfxVolume);
        }

        public void SetVolume(float evtNewValue)
        {
            _musicFadeTween?.Kill();
            musicVolume = evtNewValue;
            //audioSource.volume = musicVolume;
        }
    }
}
