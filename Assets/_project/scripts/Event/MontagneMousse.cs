using UnityEngine;

namespace GFM2025
{
    public class MontagneMousse : EventParent
    {
        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player)) {
                if (UI_QTE.Instance.StartQTE(gameObject))
                {
                    _timePass = true;
                }
            }
        }
    }
 }
