using UnityEngine;
using UnityEngine.Events;

namespace GFM2025
{
    public class MontagneMousse : EventParent
    {
        public UnityEvent OnMousse;

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player)) {
                if (UI_QTE.Instance.StartQTE(gameObject))
                {
                    OnMousse?.Invoke();
                    _timePass = true;
                }
            }
        }
    }
 }
