using UnityEngine;

namespace GFM2025
{

    [CreateAssetMenu(fileName = "SlimeDataBase", menuName = "data/SlimeData", order = 1)]
    public class SlimeDataBase : ScriptableObject
    {
        public float lifeTime;
        public float timeFadeIn;
    }
}
