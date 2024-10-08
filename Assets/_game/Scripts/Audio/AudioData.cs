using UnityEngine;

namespace _D.Scripts.Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Data/Audio", order = 0)]
    public class AudioData : ScriptableObject
    {
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public float Volume { get; private set; } = 1f;
        [field: SerializeField] public bool Loop { get; private set; }
    }
}
