using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
    public class PlayerDatas : ScriptableObject
    {
        [Header("Movements")]
        [SerializeField] private float _movementsMaxSpeed;
        [SerializeField] private float _movementsAcceleration;

        [Header("Rotation Obsolete")]
        [SerializeField] private float _rotationMaxSpeed;
        [SerializeField] private float _rotationSpeed;

        [Header("New Rotation")]
        [SerializeField] private float _timeToRotate;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;

        [Header("Bounce")]
        [SerializeField] private float _bounceForce = 3f;

        public float MovementsMaxSpeed => _movementsMaxSpeed;
        public float MovementsAcceleration => _movementsAcceleration;

        public float RotationMaxSpeed => _rotationMaxSpeed;
        public float RotationSpeed => _rotationSpeed;

        public float TimeToRotate => _timeToRotate;

        public float JumpForce => _jumpForce;

        public float BounceForce => _bounceForce;
    }
}
