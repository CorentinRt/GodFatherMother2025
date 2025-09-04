using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace GFM2025
{
    public class Siphon : EventParent
    {
        [SerializeField] private MeshRenderer _siphonRenderer;
        [SerializeField] private LayerMask _mapMask;

        private Material _siphonShader;

        private Tween _siphonEffectTween;

        private Coroutine _delayDestroyCoroutine;

        private void Start()
        {
            _siphonShader = _siphonRenderer.material;

            ClampToGround();

            AppearEffect();
        }

        private void ClampToGround()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.up * -1, out hit, Mathf.Infinity, _mapMask))
            {
                transform.position = hit.point + new Vector3(0f, 0.1f, 0f);
            }
        }

        public float GetDistanceFromPlayer()
        {
            return (PlayerBehaviour.Instance.transform.position - transform.position).magnitude;
        }

        private void AppearEffect()
        {
            if (_siphonShader == null)
                return;

            if (_siphonEffectTween != null)
            {
                _siphonEffectTween.Kill();
                _siphonEffectTween = null;
            }

            _siphonEffectTween = _siphonShader.DOFloat(0f, "_round_Inner_Size", 1f);
        }

        private void DisappearEffect()
        {
            if (_siphonShader == null)
                return;

            if (_siphonEffectTween != null)
            {
                _siphonEffectTween.Kill();
                _siphonEffectTween = null;
            }

            _siphonEffectTween = _siphonShader.DOFloat(1f, "_round_Inner_Size", 1f);
        }


        protected override void RequestDestroy()
        {
            if (_delayDestroyCoroutine != null)
                return;

            _delayDestroyCoroutine = StartCoroutine(DelayDestroyCoroutine());
        }

        private IEnumerator DelayDestroyCoroutine()
        {
            DisappearEffect();

            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }

    }
}
