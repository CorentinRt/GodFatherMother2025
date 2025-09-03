using CREMOT.GameplayUtilities;
using UnityEngine;
using UnityEngine.Events;

namespace GFM2025
{
    public class PlayerBehaviour : GenericSingleton<PlayerBehaviour>
    {
        [Header("Datas")]
        [SerializeField] private PlayerDatas _data;

        [Header("Movements")]
        [SerializeField] private Rigidbody _rb;

        [Header("Camera")]
        [SerializeField] private Transform _cameraFollowTarget;

        [SerializeField] private Transform _cameraLookAtTarget;

        public PlayerDatas Data => _data;

        public Transform CameraFollowTarget => _cameraFollowTarget;

        public Transform CameraLookAtTarget => _cameraLookAtTarget;

        public void Init()
        {
            
        }


        private void FixedUpdate()
        {
            UpdateMovement(Time.fixedDeltaTime);

            UpdateMoveRotation(Time.fixedDeltaTime);
        }

        private void UpdateMovement(float deltaTime)
        {



        }

        private void UpdateMoveRotation(float deltaTime)
        {

        }
    }
}
