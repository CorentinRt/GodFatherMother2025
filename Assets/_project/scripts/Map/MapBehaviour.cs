using CREMOT.GameplayUtilities;
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


        public void Init()
        {

        }


    }
}
