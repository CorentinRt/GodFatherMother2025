using UnityEngine;
using CREMOT.GameplayUtilities;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "GA_PlaySound", menuName = "ScriptableObjects/Sound/GA_PlaySound", order = 1)]
    public class GA_PlaySound : CommandAction
    {
        [SerializeField] private AudioClip _soundToPlay;


        public override void Execute(MonoBehaviour caller)
        {
            if (SoundManager.Exist)
            {
                SoundManager.Instance.PlaySoundOneShot(_soundToPlay);
            }
            else
            {
                Debug.LogError("Error : No soundManager singleton dounf in scene ! Sound won't play !", this);
            }
        }
    }
}
