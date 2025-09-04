using UnityEngine;

namespace GFM2025
{
    public class Siphon : EventParent
    {
        public float GetDistanceFromPlayer()
        {
            return (PlayerBehaviour.Instance.transform.position - transform.position).magnitude;
        }

    }
}
