using CREMOT.GameplayUtilities;
using UnityEngine;

namespace GFM2025
{
    public class SoundManager : GenericSingleton<SoundManager>
    {
        [Header("Sounds")]
        [SerializeField] private AudioSource _source;
        [SerializeField] private float _volumeScale = 1f;



        public void PlaySoundOneShot(AudioClip clip)
        {
            _source.PlayOneShot(clip, _volumeScale);
        }

    }
}
