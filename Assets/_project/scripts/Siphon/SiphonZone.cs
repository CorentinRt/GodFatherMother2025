using UnityEngine;

namespace GFM2025
{
    public class SiphonZone : MonoBehaviour
    {
        private void OnEnterPlayer()
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


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == null)
                return;

            if (!other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player))
                return;

            OnEnterPlayer();
        }
    }
}
