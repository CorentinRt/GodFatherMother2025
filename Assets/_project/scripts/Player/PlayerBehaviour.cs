using CREMOT.GameplayUtilities;
using DG.Tweening;
using System;
using System.Collections;
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
        [SerializeField] private Transform _rotationAnchor;

        [Space]

        [SerializeField] private LayerMask _mapLayerMask;
        [SerializeField] private float _groundedLength;
        [SerializeField] private float _jumpCooldown;

        [Space]

        [SerializeField] private GameObject _shieldVisual;

        [Space]

        [Header("Camera")]
        [SerializeField] private Transform _cameraFollowTarget;

        [SerializeField] private Transform _cameraLookAtTarget;

        [Space]

        [Header("Event position")]
        [SerializeField] private Transform _eventPosition;

        [SerializeField] private LayerMask _siphonLayerMask;

        private float _moveVerticalValue;
        private float _moveHorizontalValue;
        private float _rotateValue;

        private bool _hasJumped;
        private float _currentJumpCooldown;

        private bool _forceBlock;

        private bool _isInSpeedBoost;
        private bool _isInSpeedMalus;
        private bool _isInShield;

        private float _currentIsInSpeedBoost;
        private float _currentIsInSpeedMalus;
        private float _currentIsInShield;

        private Tween _rotateTween;

        private Coroutine _delayRotatePlayer;

        private Collider[] _bufferSiphonCollider;

        public PlayerDatas Data => _data;

        public Transform CameraFollowTarget => _cameraFollowTarget;

        public Transform CameraLookAtTarget => _cameraLookAtTarget;

        public Transform EventPosition => _eventPosition;

        public bool IsInShield => _isInShield;

        public event Action onPressPause;

        public event Action onPressQTEOne;
        public event Action onPressQTETwo;
        public event Action onPressQTEThree;
        public event Action onPressQTEFour;

        #region Init / Destroy
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

            GameManager.Instance.onGameStateChanged += ReceiveChangeGameState;

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

            GameManager.Instance.onGameStateChanged -= ReceiveChangeGameState;
        }
        #endregion

        #region Inputs
        private void UpdateMoveInput(InputAction.CallbackContext ctx)
        {
            _moveVerticalValue = ctx.ReadValue<float>();

            _moveVerticalValue = Mathf.Min(0f, _moveVerticalValue);
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
        #endregion

        private void Update()
        {
            if (_isInShield)
            {
                _currentIsInShield -= Time.deltaTime;

                if ( _currentIsInShield < 0)
                {
                    EndShieldBonus();
                }
            }

            if (_isInSpeedBoost)
            {
                _currentIsInSpeedBoost -= Time.deltaTime;

                if (_currentIsInSpeedBoost < 0)
                {
                    EndSpeedBoostBonus();
                }
            }

            if (_isInSpeedMalus)
            {
                _currentIsInSpeedMalus -= Time.deltaTime;

                if (_currentIsInSpeedMalus < 0)
                {
                    EndSpeedMalus();
                }
            }
        }

        private void FixedUpdate()
        {
            if (!CanUpdateMovements())
                return;

            if (IsGrounded() && _currentJumpCooldown <= 0f)
            {
                _hasJumped = false;
            }

            if (_currentJumpCooldown > 0f)
            {
                _currentJumpCooldown -= Time.fixedDeltaTime;
            }

            UpdateMovement(Time.fixedDeltaTime);

            transform.rotation = Quaternion.identity;

            //UpdateMoveRotation(Time.fixedDeltaTime);
        }

        #region Move
        private bool IsGrounded()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, _groundedLength, _mapLayerMask))
            {
                if (hit.transform == null)
                    return false;

                return true;
            }

            return false;
        }

        private bool CanUpdateMovements()
        {
            if (!GameManager.Exist)
                return true;

            if (GameManager.Instance.CurrentGameState == GAME_STATE.END_GAME || GameManager.Instance.CurrentGameState == GAME_STATE.PRE_GAME)
                return false;

            if (_forceBlock)
                return false;

            return true;
        }

        private void UpdateMovement(float deltaTime)
        {
            Vector3 dir = _rotationAnchor.forward * _moveVerticalValue;

            if (_useHorizontalMovement)
            {
                dir += _rotationAnchor.right * _moveHorizontalValue;
            }

            dir.Normalize();

            float multiplier = 1f;

            if (_isInSpeedBoost)
            {
                multiplier = _data.SpeedBoostMultiplier;
            }
            else if (_isInSpeedMalus)
            {
                multiplier = _data.SpeedMalusMultiplier;
            }

            _rb.linearVelocity += dir * _data.MovementsAcceleration * multiplier * deltaTime;

            Vector3 externalForce = Vector3.zero;


            if (GameManager.Instance.CurrentGameState == GAME_STATE.WATER_DECREASE)
            {
                externalForce += MapBehaviour.Instance.Data.SiphonForceOnPlayer * _rotationAnchor.forward;
            }
            else
            {
                externalForce += MapBehaviour.Instance.Data.BaseForceOnPlayer * _rotationAnchor.forward;
            }

            Vector3 siphonExternalForce = GetSiphonExternalForce();

            _rb.linearVelocity += siphonExternalForce + externalForce * deltaTime;

        }

        private void UpdateMoveRotation(float deltaTime)
        {
            _rb.angularVelocity += Vector3.up * _rotateValue * _data.RotationSpeed * deltaTime;

        }

        private void Jump()
        {
            if (_hasJumped)
                return;

            _hasJumped = true;

            _currentJumpCooldown = _jumpCooldown;

            _rb.AddForce(Vector3.up * _data.JumpForce, ForceMode.Impulse);
        }
        #endregion

        #region IPlayerBehaviour
        public PlayerBehaviour GetPlayerBehaviour()
        {
            return this;
        }
        #endregion

        #region RotatePlayer
        private void ReceiveChangeGameState(GAME_STATE gameState)
        {
            if (_rotateTween != null)
            {
                _rotateTween.Kill();
            }

            if (gameState == GAME_STATE.SCORING)
            {
                StartDelayRotatePlayer(0f);
            }
            else if (gameState == GAME_STATE.RETURN_HOME)
            {
                _rotateTween = _rotationAnchor.DORotate(new Vector3(0f, 180f, 0f), _data.TimeToRotate);
            }
        }

        private void StartDelayRotatePlayer(float rotationY)
        {
            StopDelayRotatePlayer();

            _delayRotatePlayer = StartCoroutine(DelayRotatePlayer(rotationY));
        }

        private void StopDelayRotatePlayer()
        {
            if (_delayRotatePlayer != null)
            {
                StopCoroutine(_delayRotatePlayer);
                _delayRotatePlayer = null;
            }
        }

        private IEnumerator DelayRotatePlayer(float rotationY)
        {
            yield return new WaitForSeconds(1f);

            _rotateTween = _rotationAnchor.DORotate(new Vector3(0f, rotationY, 0f), _data.TimeToRotate);
        }
        #endregion

        #region Bounce
        public void BouncePlayerBack()
        {
            _rb.AddForce(_rotationAnchor.forward * -1 * _data.BounceForce, ForceMode.Impulse);
        }
        #endregion

        #region Force Block
        public void StartForceBlockPlayer()
        {
            _forceBlock = true;

            _rb.linearVelocity = Vector3.zero;
        }

        public void StopForceBlockPlayer()
        {
            _forceBlock = false;
        }
        #endregion

        #region Siphon
        private Vector3 GetSiphonExternalForce()
        {
            Siphon siphon = GetClosestSiphon();

            if (siphon == null)
                return Vector3.zero;

            Vector3 force = siphon.transform.position - transform.position;

            force.y = 0f;

            force *= _data.ExternalSiphonForceMultiplier / force.magnitude;

            return force;
        }

        private Siphon GetClosestSiphon()
        {
            _bufferSiphonCollider = Physics.OverlapSphere(transform.position, 25f, _siphonLayerMask);

            if (_bufferSiphonCollider == null || _bufferSiphonCollider.Length <= 0)
                return null;

            float minDist = 1000000f;

            Siphon minDistSiphon = null;

            foreach (Collider collider in _bufferSiphonCollider)
            {
                if (collider == null)
                    continue;

                if (!collider.gameObject.TryGetComponent<Siphon>(out Siphon siphon))
                    return null;

                if (siphon.GetDistanceFromPlayer() < minDist)
                {
                    minDist = siphon.GetDistanceFromPlayer();
                    minDistSiphon = siphon;
                }
            }

            return minDistSiphon;
        }

        #endregion

        #region Bonus
        public void StartShieldBonus()
        {
            _isInShield = true;

            _currentIsInShield = _data.ShieldDuration;

            _shieldVisual.SetActive(true);
        }

        public void EndShieldBonus()
        {
            _isInShield = false;

            _shieldVisual.SetActive(false);
        }

        public void StartSpeedBoostBonus()
        {
            _isInSpeedBoost = true;

            _currentIsInSpeedBoost = _data.SpeedBoostDuration;

            EndSpeedMalus();
        }

        public void EndSpeedBoostBonus()
        {
            _isInSpeedBoost = false;
        }

        public void StartSpeedMalus()
        {
            _isInSpeedMalus = true;

            _currentIsInSpeedMalus = _data.SpeedMalusDuration;

            EndSpeedBoostBonus();
        }

        public void EndSpeedMalus()
        {
            _isInSpeedMalus = false;
        }

        #endregion
    }
}
