using CREMOT.GameplayUtilities;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GFM2025
{
    public class PlayerBehaviour : GenericSingleton<PlayerBehaviour>
    {
        [Header("Inputs")]
        [SerializeField] private InputActionReference _move;
        [SerializeField] private InputActionReference _rotate;
        [SerializeField] private InputActionReference _jump;

        [Header("Datas")]
        [SerializeField] private PlayerDatas _data;

        [Header("Movements")]
        [SerializeField] private Rigidbody _rb;

        [Header("Camera")]
        [SerializeField] private Transform _cameraFollowTarget;

        [SerializeField] private Transform _cameraLookAtTarget;


        private float _moveValue;
        private float _rotateValue;

        public PlayerDatas Data => _data;

        public Transform CameraFollowTarget => _cameraFollowTarget;

        public Transform CameraLookAtTarget => _cameraLookAtTarget;

        public void Init()
        {
            _move.action.started += UpdateMoveInput;
            _move.action.performed += UpdateMoveInput;
            _move.action.canceled += UpdateMoveInput;

            _rotate.action.started += UpdateRotateInput;
            _rotate.action.performed += UpdateRotateInput;
            _rotate.action.canceled += UpdateRotateInput;

            _jump.action.started += UpdateJumpInput;

            _rb.maxAngularVelocity = _data.MovementsMaxSpeed;
            _rb.maxAngularVelocity = _data.RotationMaxSpeed;
        }

        private void OnDestroy()
        {
            _move.action.started -= UpdateMoveInput;
            _move.action.performed -= UpdateMoveInput;
            _move.action.canceled -= UpdateMoveInput;

            _rotate.action.started -= UpdateRotateInput;
            _rotate.action.performed -= UpdateRotateInput;
            _rotate.action.canceled -= UpdateRotateInput;

            _jump.action.started -= UpdateJumpInput;
        }

        private void UpdateMoveInput(InputAction.CallbackContext ctx)
        {
            _moveValue = ctx.ReadValue<float>();
        }

        private void UpdateRotateInput(InputAction.CallbackContext ctx)
        {
            _rotateValue = ctx.ReadValue<float>();
        }

        private void UpdateJumpInput(InputAction.CallbackContext ctx)
        {
            Jump();
        }

        private void FixedUpdate()
        {
            UpdateMovement(Time.fixedDeltaTime);

            UpdateMoveRotation(Time.fixedDeltaTime);
        }

        private void UpdateMovement(float deltaTime)
        {
            _rb.linearVelocity += transform.forward * _moveValue * deltaTime;

        }

        private void UpdateMoveRotation(float deltaTime)
        {
            _rb.angularVelocity += Vector3.up * _rotateValue * deltaTime;

        }

        private void Jump()
        {
            _rb.AddForce(Vector3.up * _data.JumpForce, ForceMode.Impulse);
        }
    }
}
