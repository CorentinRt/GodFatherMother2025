using UnityEngine;

namespace GFM2025
{
    public class Siphon : EventParent
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other == null || other.gameObject == null)
                return;

            if (!other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player))
                return;

            
        }

        private void OnDestroy()
        {
            
        }
    }
}
