using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "MainGameData", menuName = "ScriptableObjects/MainGameData", order = 1)]
    public class MainGameData : ScriptableObject
    {
        [Header("Pre Game")]
        [SerializeField] private float _preGameCooldown;

        [Header("Scoring Phase")]
        [SerializeField] private float _scoringPhaseMinDuration;
        [SerializeField] private float _scoringPhaseMaxDuration;



        public float PreGameCooldown => _preGameCooldown;

        public float ScoringPhaseMinDuration => _scoringPhaseMinDuration;
        public float ScoringPhaseMaxDuration => _scoringPhaseMaxDuration;

    }
}
