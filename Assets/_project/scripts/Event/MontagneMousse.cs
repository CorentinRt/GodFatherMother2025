using UnityEngine;

namespace GFM2025
{
    public class MontagneMousse : EventParent
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player))
            {
                if (player.GetPlayerBehaviour().IsInShield)
                {
                    player.GetPlayerBehaviour().EndShieldBonus();
                    RequestDestroy();
                    return;
                }

                Debug.Log("lance QTE");

                if (UI_QTE.Instance.StartQTE(gameObject))
                {
                    _timePass = true;
                }
                //InputActionReference
            }
        }
    }
 }
