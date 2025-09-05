using UnityEngine;
using CREMOT.GameplayUtilities;
using System.Collections.Generic;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "GA_PlaySound", menuName = "ScriptableObjects/Sound/GA_PlaySound", order = 1)]
    public class GA_PlaySound : CommandAction
    {
        [SerializeField] private List<AudioClip> _soundToPlay;

        [SerializeField] private float _delay;

        public override void Execute(MonoBehaviour caller)
        {
            if (SoundManager.Exist)
            {
                if (_soundToPlay.Count <= 0)
                    return;

                AudioClip clip = _soundToPlay[Random.Range(0, _soundToPlay.Count)];
                
                SoundManager.Instance.AddSoundToPlay(clip, _delay);
            }
            else
            {
                Debug.LogError("Error : No soundManager singleton dounf in scene ! Sound won't play !", this);
            }
        }
    }
}
