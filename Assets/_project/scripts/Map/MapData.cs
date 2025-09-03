using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData", order = 1)]
    public class MapData : ScriptableObject
    {
        [Header("Water")]
        [SerializeField] private float _timeToEmpty = 20f;


        public float TimeToEmpty => _timeToEmpty;
    }
}
