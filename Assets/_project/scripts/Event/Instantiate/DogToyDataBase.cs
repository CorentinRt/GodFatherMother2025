using UnityEngine;

namespace GFM2025
{


    [CreateAssetMenu(fileName = "DogToyDataBase", menuName = "data/DogToyData", order = 1)]

    public class DogToyDataBase : ScriptableObject
    {
        public float bounce;
        public float scaleSpeed;
        public float maxVelocity;
    }

}
