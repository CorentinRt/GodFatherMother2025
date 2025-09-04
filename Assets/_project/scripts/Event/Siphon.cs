using UnityEngine;

namespace GFM2025
{
    public class Siphon : EventParent
    {
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") {
                Debug.Log("TP jouer");
            }
        }
    }
}
