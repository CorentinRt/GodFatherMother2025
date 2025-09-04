using UnityEngine;

namespace GFM2025
{
    public class PlasticBall : EventParent {
        private Rigidbody _rgbd;
        [SerializeField] private PlasticBallDataBase _data;

        private void OnEnable() {
            TryGetComponent(out _rgbd);
        }

        private void Start() {
            _rgbd.linearVelocity = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)).normalized;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "PlasticBall")
            {
                _rgbd.AddForce(Vector3.Reflect(collision.transform.position - transform.position, collision.contacts[0].normal) * _data.bounce, ForceMode.Impulse);
            }
            if (collision.transform.tag == "Player")
            {
                _rgbd.AddForce(Vector3.Reflect(collision.transform.position - transform.position, collision.contacts[0].normal) * _data.bounce, ForceMode.Impulse);
                PlayerBehaviour.Instance.BouncePlayerBack();
            }
        }
    }
}
