using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        [Header("Level Name")]
        [SerializeField] private string _menuName;
        [SerializeField] private string _gameName;

        public string MenuName => _menuName;
        public string GameName => _gameName;
    }
}
