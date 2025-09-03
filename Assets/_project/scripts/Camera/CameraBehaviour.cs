using CREMOT.GameplayUtilities;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace GFM2025
{
    public class CameraBehaviour : GenericSingleton<CameraBehaviour>
    {
        [Header("Datas")]
        [SerializeField] private CameraDatas _datas;

        private Transform _cameraFollowTarget;

        private Transform _cameraLookAtTarget;

        private Coroutine _cameraFollowCoroutine;

        public void Init()
        {
            _cameraFollowTarget = PlayerBehaviour.Instance.CameraFollowTarget;

            _cameraLookAtTarget = PlayerBehaviour.Instance.CameraLookAtTarget;

            transform.position = _cameraFollowTarget.position;

            transform.rotation = _cameraFollowTarget.rotation;

            StartCameraFollowCoroutine();
        }

        private void StartCameraFollowCoroutine()
        {
            StopCameraFollowCoroutine();

            _cameraFollowCoroutine = StartCoroutine(CameraFollowCoroutine());
        }
        
        private void StopCameraFollowCoroutine()
        {
            if (_cameraFollowCoroutine != null)
            {
                StopCoroutine(_cameraFollowCoroutine);
                _cameraFollowCoroutine = null;
            }
        }

        private IEnumerator CameraFollowCoroutine()
        {
            while (true)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_cameraLookAtTarget.forward, transform.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _datas.LookAtSmoothSpeed);

                Vector3 tempRot = transform.rotation.eulerAngles;
                tempRot.z = 0;
                transform.rotation = Quaternion.Euler(tempRot);

                Vector3 targetPosition = Vector3.Lerp(transform.position, _cameraFollowTarget.position, Time.deltaTime * _datas.FollowSmoothSpeed);

                transform.position = targetPosition;

                yield return null;
            }

            yield return null;
        }
    }
}
