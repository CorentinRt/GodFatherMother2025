using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "PlasticBallDataBase", menuName = "data/PlasticBallData", order = 1)]

    public class PlasticBallDataBase : ScriptableObject
    {
        public float bounce;

        [SerializeField] public List<Sprite> _ballSprites;


        public Sprite GetRandomBallSprite()
        {
            if (_ballSprites == null || _ballSprites.Count <= 0)
                return null;

            int count = _ballSprites.Count;

            int random = Random.Range(0, count);

            return _ballSprites[random];
        }
    }
}
