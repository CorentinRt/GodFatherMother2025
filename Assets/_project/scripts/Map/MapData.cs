using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData", order = 1)]
    public class MapData : ScriptableObject
    {
        [Header("Water")]
        [SerializeField] private float _timeToEmpty = 20f;

        [Header("Force")]
        [SerializeField] private float _siphonForceOnPlayer;
        [SerializeField] private float _baseForceOnPlayer;


        public float TimeToEmpty => _timeToEmpty;

        public float SiphonForceOnPlayer => _siphonForceOnPlayer;
        public float BaseForceOnPlayer => _baseForceOnPlayer;
    }
}
