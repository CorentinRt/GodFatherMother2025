using UnityEngine;

namespace GFM2025
{
    public class MutantSlime : EventParent
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player))
            {
                UI_Slime.Instance.OpendUiSlime();
            }
        }
    }
}
