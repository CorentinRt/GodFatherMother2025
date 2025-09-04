using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "ScoreData", menuName = "ScriptableObjects/ScoreData", order = 1)]
    public class ScoreData : ScriptableObject
    {
        [Header("Score")]
        [SerializeField] private float _delayBetweenEachScore = 1f;

        [SerializeField] private int _amountByScore = 15;

        [Space]

        [SerializeField] private int _returnHomeScoreAmount = 50;

        public float DelayBetweenEachScore => _delayBetweenEachScore;

        public int AmountByScore => _amountByScore;

        public int ReturnHomeScoreAmount => _returnHomeScoreAmount;
    }
}
