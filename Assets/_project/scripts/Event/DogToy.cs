using UnityEngine;

namespace GFM2025
{
    public class DogToy : EventParent {
        private Rigidbody _rgbd;
        [SerializeField] private LayerMask _mask;

        [SerializeField] private DogToyDataBase _data;

        private int NumberBounce = 1;

        private void OnEnable() {
            TryGetComponent(out _rgbd);
            transform.position = new Vector3(transform.position.x, transform.position.y+5, transform.position.z);
            _rgbd.maxLinearVelocity = _data.maxVelocity;
        }

        private void Start() {
            _rgbd.linearVelocity = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((( 1 << collision.gameObject.layer) & _mask)  != 0)
            {
                NumberBounce++;
                _rgbd.AddForce(Vector3.Reflect(collision.transform.position - transform.position, collision.contacts[0].normal) * _data.bounce * NumberBounce* _data.scaleSpeed, ForceMode.Impulse);
            }
            if (collision.transform.tag == "Player")
            {
                _rgbd.AddForce(Vector3.Reflect(collision.transform.position - transform.position, collision.contacts[0].normal) * _data.bounce, ForceMode.Impulse);
                PlayerBehaviour.Instance.BouncePlayerBack();
            }
        }
    }
}