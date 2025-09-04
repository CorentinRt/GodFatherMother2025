using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GFM2025
{
    public class UI_WaterLevel : MonoBehaviour
    {
        [Header("Slider")]
        [SerializeField] private Slider _sliderWater;

        [Header("Shake effect")]
        [SerializeField] private GameObject _containerWaterSlide;

        private Tween _shakeTween;

        private bool _shouldShake;


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (MapBehaviour.Exist)
            {
                MapBehaviour.Instance.onUpdateWaterLevelPercent += UpdateSliderWater;
            }
            else
            {
                Debug.LogError("Error : No MapBehaviour singleton found in scene ! Init of it's UIs won't work properly !", this);
            }

            if (GameManager.Exist)
            {
                GameManager.Instance.onGameStateChanged += OnReceiveChangeGameState;
            }
            else
            {
                Debug.LogError("Error : No GameManager singleton found in scene ! Init of it's UIs won't work properly !", this);
            }

            _sliderWater.value = 1;
        }

        private void OnDestroy()
        {
            if (MapBehaviour.Exist)
            {
                MapBehaviour.Instance.onUpdateWaterLevelPercent -= UpdateSliderWater;
            }

            if (GameManager.Exist)
            {
                GameManager.Instance.onGameStateChanged -= OnReceiveChangeGameState;
            }

        }

        private void UpdateSliderWater(float percent)
        {
            _sliderWater.value = 1 - percent;
        }

        private void OnReceiveChangeGameState(GAME_STATE gameState)
        {
            if (gameState != GAME_STATE.WATER_DECREASE)
            {
                StopShake();
                return;
            }

            StartShake();
        }

        public void StartShake()
        {
            _shouldShake = true;
            ShakeLoop();
        }

        private void ShakeLoop()
        {
            if (!_shouldShake)
                return;

            _shakeTween = _containerWaterSlide.transform.DOShakePosition(0.5f, 10, 10, 90, false, true).OnComplete(() => ShakeLoop());
        }

        public void StopShake()
        {
            _shouldShake = false;

            if (_shakeTween != null)
            {
                _shakeTween.Kill();
            }

            _containerWaterSlide.transform.localPosition = Vector3.zero;
        }
    }
}
