using DG.Tweening;
using UnityEngine;

namespace GFM2025
{
    public class MysteryBuoy : EventParent
    {
        public enum MYSTERY_BONUS
        {
            PLAYER_SPEED_INCREASE = 0,
            SHIELD = 1,
            MUSIC_EPIC = 2,
            PLAYER_SPEED_DECREASE = 3,
            MUSIC_TRASH = 4
        }

        [SerializeField] private Transform _visuals;

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
            _bonus = (MYSTERY_BONUS)Random.Range(0, 4);
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

                case MYSTERY_BONUS.SHIELD:
                    player.StartShieldBonus();
                    break;
                case MYSTERY_BONUS.MUSIC_EPIC:

                    break;

                case MYSTERY_BONUS.PLAYER_SPEED_DECREASE:
                    player.StartSpeedMalus();
                    break;

                case MYSTERY_BONUS.MUSIC_TRASH:

                    break;

                default:
                    break;
            }

            RequestDestroy();
        }
    }
}
