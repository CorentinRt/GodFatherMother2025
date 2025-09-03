using UnityEngine;

public class DogToy : MonoBehaviour
{
    private Rigidbody _rgbd;

    [SerializeField] private float _lifeTime;
    private float _time = 0;

    private void OnEnable() {
        TryGetComponent(out _rgbd);
    }

    private void Start() {
        _rgbd.linearVelocity = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
    }

    void Update() {
        _time += Time.deltaTime;
        if (_time >= _lifeTime) {
            Destroy(gameObject);
        }
    }
}
