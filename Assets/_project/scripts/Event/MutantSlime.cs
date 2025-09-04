using UnityEngine;

namespace GFM2025
{
    public class MutantSlime : EventParent
    {

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player))
                return;

            UI_Slime.Instance.OpendUiSlime();

            player.GetPlayerBehaviour().StartInvertInput();
        }
    }
}
