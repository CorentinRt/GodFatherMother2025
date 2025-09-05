using UnityEngine;

namespace GFM2025
{
    public class SiphonZone : MonoBehaviour
    {
        private void OnStayPlayer()
        {
            if (GameManager.Exist)
            {
                GameManager.Instance.PlayerEntersSiphonZone();
            }
            else
            {
                Debug.LogError("Error : No GameManager singleton found in scene ! Score won't work !", this);
            }
        }


        private void OnTriggerStay(Collider other)
        {
            if (!GameManager.Exist)
                return;

            if (GameManager.Instance.CurrentGameState != GAME_STATE.WATER_DECREASE)
                return;

            if (other.gameObject == null)
                return;

            if (!other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player))
                return;

            OnStayPlayer();
        }
    }
}
