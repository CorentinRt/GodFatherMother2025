using UnityEngine;

namespace GFM2025
{
    public class EventParent : MonoBehaviour
    {
        [HideInInspector] public float lifeTime;
        private float _time = 0;
        protected bool _timePass;

        private void Start()
        {
            _timePass = false;
        }

        void Update()
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            if (!_timePass)
            {
                _time += Time.deltaTime;
                if (_time >= lifeTime)
                {
                    RequestDestroy();
                }
            }
        }

        protected virtual void RequestDestroy()
        {
            Destroy(gameObject);
        }
    }
}
