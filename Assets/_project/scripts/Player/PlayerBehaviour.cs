using CREMOT.GameplayUtilities;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GFM2025
{
    public class PlayerBehaviour : GenericSingleton<PlayerBehaviour>, IPlayerBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private InputActionReference _move;
        [SerializeField] private InputActionReference _rotate;
        [SerializeField] private InputActionReference _jump;

        [SerializeField] private InputActionReference _qteOne;
        [SerializeField] private InputActionReference _qteTwo;
        [SerializeField] private InputActionReference _qteThree;
        [SerializeField] private InputActionReference _qteFour;

        [SerializeField] private InputActionReference _pause;

        [Header("Datas")]
        [SerializeField] private PlayerDatas _data;

        [Header("Movements")]
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private bool _useHorizontalMovement = true;

        [Header("Camera")]
        [SerializeField] private Transform _cameraFollowTarget;

        [SerializeField] private Transform _cameraLookAtTarget;


        private float _moveVerticalValue;
        private float _moveHorizontalValue;
        private float _rotateValue;

        public PlayerDatas Data => _data;

        public Transform CameraFollowTarget => _cameraFollowTarget;

        public Transform CameraLookAtTarget => _cameraLookAtTarget;


        public event Action onPressPause;

        public event Action onPressQTEOne;
        public event Action onPressQTETwo;
        public event Action onPressQTEThree;
        public event Action onPressQTEFour;

        public void Init()
        {
            _move.action.started += UpdateMoveInput;
            _move.action.performed += UpdateMoveInput;
            _move.action.canceled += UpdateMoveInput;

            _rotate.action.started += UpdateRotateInput;
            _rotate.action.performed += UpdateRotateInput;
            _rotate.action.canceled += UpdateRotateInput;

            _jump.action.started += UpdateJumpInput;

            _qteOne.action.started += UpdateQTEInputOne;
            _qteTwo.action.started += UpdateQTEInputTwo;
            _qteThree.action.started += UpdateQTEInputThree;
            _qteFour.action.started += UpdateQTEInputFour;

            _pause.action.started += UpdatePauseInput;

            _rb.maxLinearVelocity = _data.MovementsMaxSpeed;
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

            _qteOne.action.started -= UpdateQTEInputOne;
            _qteTwo.action.started -= UpdateQTEInputTwo;
            _qteThree.action.started -= UpdateQTEInputThree;
            _qteFour.action.started -= UpdateQTEInputFour;

            _pause.action.started -= UpdatePauseInput;
        }

        private void UpdateMoveInput(InputAction.CallbackContext ctx)
        {
            _moveVerticalValue = ctx.ReadValue<float>();
        }

        private void UpdateRotateInput(InputAction.CallbackContext ctx)
        {
            _rotateValue = ctx.ReadValue<float>();
            _moveHorizontalValue = ctx.ReadValue<float>();
        }

        private void UpdateJumpInput(InputAction.CallbackContext ctx)
        {
            Jump();
        }

        private void UpdatePauseInput(InputAction.CallbackContext ctx)
        {
            onPressPause?.Invoke();
        }

        private void UpdateQTEInputOne(InputAction.CallbackContext ctx)
        {
            onPressQTEOne?.Invoke();
        }
        private void UpdateQTEInputTwo(InputAction.CallbackContext ctx)
        {
            onPressQTETwo?.Invoke();
        }
        private void UpdateQTEInputThree(InputAction.CallbackContext ctx)
        {
            onPressQTEThree?.Invoke();
        }
        private void UpdateQTEInputFour(InputAction.CallbackContext ctx)
        {
            onPressQTEFour?.Invoke();
        }

        private void FixedUpdate()
        {
            if (!CanUpdateMovements())
                return;

            UpdateMovement(Time.fixedDeltaTime);

            //UpdateMoveRotation(Time.fixedDeltaTime);
        }

        private bool CanUpdateMovements()
        {
            if (!GameManager.Exist)
                return true;

            if (GameManager.Instance.CurrentGameState == GAME_STATE.END_GAME || GameManager.Instance.CurrentGameState == GAME_STATE.PRE_GAME)
                return false;

            return true;
        }

        private void UpdateMovement(float deltaTime)
        {
            Vector3 dir = transform.forward * _moveVerticalValue;

            if (_useHorizontalMovement)
            {
                dir += transform.right * _moveHorizontalValue;
            }

            dir.Normalize();

            _rb.linearVelocity += dir * _data.MovementsAcceleration * deltaTime;

        }

        private void UpdateMoveRotation(float deltaTime)
        {
            _rb.angularVelocity += Vector3.up * _rotateValue * _data.RotationSpeed * deltaTime;

        }

        private void Jump()
        {
            _rb.AddForce(Vector3.up * _data.JumpForce, ForceMode.Impulse);
        }

        public PlayerBehaviour GetPlayerBehaviour()
        {
            return this;
        }
    }
}
