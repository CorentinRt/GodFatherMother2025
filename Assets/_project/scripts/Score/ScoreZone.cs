using System.Collections;
using UnityEngine;

namespace GFM2025
{
    public class ScoreZone : MonoBehaviour
    {

        private void OnEnterPlayer()
        {
            if (ScoreManager.Exist)
            {
                ScoreManager.Instance.PlayerEnterScoreZone();
            }
            else
            {
                Debug.LogError("Error : No ScoreManager singleton found in scene ! Score won't work !", this);
            }
        }

        private void OnExitPlayer()
        {
            if (ScoreManager.Exist)
            {
                ScoreManager.Instance.PlayerExitScoreZone();
            }
            else
            {
                Debug.LogError("Error : No ScoreManager singleton found in scene ! Score won't work !", this);
            }
        }

        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == null)
                return;

            if (!other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player))
                return;

            OnEnterPlayer();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == null)
                return;

            if (!other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player))
                return;

            OnExitPlayer();
        }
    }
}
