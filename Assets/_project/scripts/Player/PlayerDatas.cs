using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
    public class PlayerDatas : ScriptableObject
    {
        [Header("Movements")]
        [SerializeField] private float _movementsMaxSpeed;
        [SerializeField] private float _movementsAcceleration;

        [Header("Rotation")]
        [SerializeField] private float _rotationMaxSpeed;
        [SerializeField] private float _rotationSpeed;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;

        public float MovementsMaxSpeed => _movementsMaxSpeed;
        public float MovementsAcceleration => _movementsAcceleration;

        public float RotationMaxSpeed => _rotationMaxSpeed;
        public float RotationSpeed => _rotationSpeed;

        public float JumpForce => _jumpForce;
    }
}
