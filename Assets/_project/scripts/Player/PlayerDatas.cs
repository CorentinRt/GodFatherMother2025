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

        [Header("Mystery buoy bonuses")]
        [SerializeField] private float _speedBoostDuration = 5f;
        [SerializeField] private float _speedMalusDuration = 5f;
        [SerializeField] private float _shieldDuration = 5f;

        [Space]

        [SerializeField] private float _speedBoostMultiplier = 2f;
        [SerializeField] private float _speedMalusMultiplier = 0.5f;

        [Space]

        [SerializeField] private float _externalSiphonForceMultiplier = 2f;

        public float MovementsMaxSpeed => _movementsMaxSpeed;
        public float MovementsAcceleration => _movementsAcceleration;

        public float RotationMaxSpeed => _rotationMaxSpeed;
        public float RotationSpeed => _rotationSpeed;

        public float TimeToRotate => _timeToRotate;

        public float JumpForce => _jumpForce;

        public float BounceForce => _bounceForce;

        public float SpeedBoostDuration { get => _speedBoostDuration; set => _speedBoostDuration = value; }
        public float SpeedMalusDuration { get => _speedMalusDuration; set => _speedMalusDuration = value; }
        public float ShieldDuration { get => _shieldDuration; set => _shieldDuration = value; }
        public float SpeedBoostMultiplier { get => _speedBoostMultiplier; set => _speedBoostMultiplier = value; }
        public float SpeedMalusMultiplier { get => _speedMalusMultiplier; set => _speedMalusMultiplier = value; }
        public float ExternalSiphonForceMultiplier { get => _externalSiphonForceMultiplier; set => _externalSiphonForceMultiplier = value; }
    }
}
