using UnityEngine;

namespace GFM2025
{
    public class DogToy : EventParent {
        private Rigidbody _rgbd;

        private void OnEnable() {
            TryGetComponent(out _rgbd);
        }

        private void Start() {
            _rgbd.linearVelocity = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        }
    }
}