using CREMOT.GameplayUtilities;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GFM2025
{
    public class SoundManager : GenericSingleton<SoundManager>
    {
        [Header("Sounds")]
        [SerializeField] private AudioSource _source;
        [SerializeField] private float _volumeScale = 1f;

        [SerializeField] private List<(AudioClip, float)> _sounds = new ();

        private void Update()
        {
            for (int i = 0; i < _sounds.Count; ++i)
            {
                (AudioClip, float) sound = _sounds[i];

                if (sound.Item2 <= 0)
                {
                    PlaySoundOneShot(sound.Item1);
                    _sounds.RemoveAt(i);
                    --i;
                }
                else
                {
                    sound.Item2 -= Time.deltaTime;
                }
            }
        }

        public void AddSoundToPlay(AudioClip clip, float delay)
        {
            _sounds.Add((clip, delay));
        }

        public void PlaySoundOneShot(AudioClip clip)
        {
            _source.PlayOneShot(clip, _volumeScale);
        }

    }
}
