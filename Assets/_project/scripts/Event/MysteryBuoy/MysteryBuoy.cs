using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace GFM2025
{
    public class MysteryBuoy : EventParent
    {
        public enum MYSTERY_BONUS
        {
            PLAYER_SPEED_INCREASE = 0,
            PLAYER_SPEED_DECREASE = 1,
        }

        [SerializeField] private Transform _visuals;


        public UnityEvent onDestroyByGet;

        private MYSTERY_BONUS _bonus;

        private Tween _rotateInfiniteTween;


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            PlayRotateInfinite();

            SetRandomMysteryBonus();
        }

        private void SetRandomMysteryBonus()
        {
            _bonus = (MYSTERY_BONUS)Random.Range(0, 1);
        }

        private void PlayRotateInfinite()
        {
            if (_rotateInfiniteTween != null)
            {
                _rotateInfiniteTween.Kill();
                _rotateInfiniteTween = null;
            }

            _rotateInfiniteTween = _visuals.DORotate(new Vector3(0f, 360f, 0f), 3f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(() => PlayRotateInfinite());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null || other.gameObject == null)
                return;

            if (!other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour playerProxy))
                return;

            PlayerBehaviour player = playerProxy.GetPlayerBehaviour();

            switch (_bonus)
            {
                case MYSTERY_BONUS.PLAYER_SPEED_INCREASE:
                    player.StartSpeedBoostBonus();
                    break;

                case MYSTERY_BONUS.PLAYER_SPEED_DECREASE:
                    player.StartSpeedMalus();
                    break;

                default:
                    break;
            }

            onDestroyByGet?.Invoke();

            RequestDestroy();
        }
    }
}
