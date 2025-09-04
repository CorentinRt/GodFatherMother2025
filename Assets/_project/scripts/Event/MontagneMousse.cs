using UnityEngine;

namespace GFM2025
{
    public class MontagneMousse : EventParent
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                Debug.Log("lance QTE");
            }
        }
    }
}
