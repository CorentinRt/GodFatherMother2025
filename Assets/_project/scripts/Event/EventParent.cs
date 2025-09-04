using UnityEngine;

namespace GFM2025
{
    public class EventParent : MonoBehaviour
    {
        [HideInInspector] public float lifeTime;
        private float _time = 0;

        void Update()
        {
            _time += Time.deltaTime;
            if (_time >= lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
