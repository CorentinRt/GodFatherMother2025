using UnityEngine;
using CREMOT.GameplayUtilities;
using System.Collections;
using System;
using UnityEngine.Events;

namespace GFM2025
{
    public class ScoreManager : GenericSingleton<ScoreManager>
    {
        [Header("Datas")]
        [SerializeField] private ScoreData _data;

        private bool _playerInside;

        private int _currentScore;

        private Coroutine _scoreCoroutine;


        public bool PlayerInside => _playerInside;
        public int CurrentScore => _currentScore;


        public event Action<int> onScorePoints;

        public UnityEvent onScorePointsUnity;

        public void Init()
        {
            _currentScore = 0;


        }

        private void ScorePoints()
        {
            if (GameManager.Exist && GameManager.Instance.CurrentGameState != GAME_STATE.SCORING)
                return;
            
            _currentScore += _data.AmountByScore;

            onScorePoints?.Invoke(_currentScore);
            onScorePointsUnity?.Invoke();
        }

        public void ScoreReturnHomePoints()
        {
            if (GameManager.Exist && GameManager.Instance.CurrentGameState != GAME_STATE.SCORING)
                return;

            _currentScore += _data.ReturnHomeScoreAmount;

            onScorePoints?.Invoke(_currentScore);
            onScorePointsUnity?.Invoke();
        }

        public void PlayerEnterScoreZone()
        {
            _playerInside = true;

            StartScoreCoroutine();
        }

        public void PlayerExitScoreZone()
        {
            _playerInside = false;

            StopScoreCoroutine();
        }

        private void StartScoreCoroutine()
        {
            StopScoreCoroutine();

            _scoreCoroutine = StartCoroutine(ScoreCoroutine());
        }

        private void StopScoreCoroutine()
        {
            if (_scoreCoroutine != null)
            {
                StopCoroutine(_scoreCoroutine);
                _scoreCoroutine = null;
            }
        }

        private IEnumerator ScoreCoroutine()
        {
            yield return new WaitForSeconds(_data.DelayBetweenEachScore);

            while (true)
            {
                ScorePoints();

                yield return new WaitForSeconds(_data.DelayBetweenEachScore);
            }
        }

    }
}
