using CREMOT.GameplayUtilities;
using System;
using UnityEngine;

namespace GFM2025
{
    public class MapBehaviour : GenericSingleton<MapBehaviour>
    {
        [Header("Data")]
        [SerializeField] private MapData _data;

        [Header("Water")]
        [SerializeField] private Transform _waterLevel;
        [SerializeField] private Transform _waterEmptyLevel;

        private Vector3 _waterFillPosition;
        private Vector3 _waterEmptyPosition;

        private float _currentEmptiedTime;

        public event Action<float> onUpdateWaterLevelPercent;

        public void Init()
        {
            _waterFillPosition = _waterLevel.position;

            _waterEmptyPosition = _waterEmptyLevel.position;
        }

        private void Update()
        {
            if (!GameManager.Exist)
                return;

            if (GameManager.Instance.CurrentGameState != GAME_STATE.WATER_DECREASE)
                return;

            UpdateWaterlevel(Time.deltaTime);
        }

        private void UpdateWaterlevel(float deltaTime)
        {
            if (_currentEmptiedTime >= _data.TimeToEmpty)
            {
                GameManager.Instance.WaterEmpty();
                return;
            }

            _currentEmptiedTime += deltaTime;

            _currentEmptiedTime = Mathf.Min(_currentEmptiedTime, _data.TimeToEmpty);

            float currentTimePercent = _currentEmptiedTime / _data.TimeToEmpty;

            currentTimePercent = Mathf.Clamp01(currentTimePercent);

            Vector3 targetPosition = _waterFillPosition + ((_waterEmptyPosition - _waterFillPosition) * currentTimePercent);

            _waterLevel.position = targetPosition;

            onUpdateWaterLevelPercent?.Invoke(currentTimePercent);
        }
    }
}
