using _D.Scripts.Audio;
using DG.Tweening;
using UnityEngine;

namespace _game.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        
        [SerializeField] private float musicVolume = 1;
        [SerializeField] private float sfxVolume = 1;
        [SerializeField] private float fadeInDuration = 5;
        [SerializeField] private AudioClip mainMenuMusic;
        [SerializeField] private AudioSource audioSource;
        [Space(10)]
        [SerializeField] private AudioData buttonClickSound;
        
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
            
            audioSource = GetComponent<AudioSource>();
        }
        
        public void PlayButtonClickSound()
        {
            audioSource.PlayOneShot(buttonClickSound.AudioClip, buttonClickSound.Volume * sfxVolume);
        }

        public void SetVolume(float evtNewValue)
        {
            _musicFadeTween?.Kill();
            musicVolume = evtNewValue;
            audioSource.volume = musicVolume;
        }
    }
}
