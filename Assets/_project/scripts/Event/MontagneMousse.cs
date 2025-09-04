using UnityEngine;

namespace GFM2025
{
    public class MontagneMousse : EventParent
    {
        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player)) {
                Debug.Log("lance QTE");
                UI_QTE.Instance.StartQTE();
                //InputActionReference
            }
        }
    }
 }
