using System;
using System.Collections.Generic;
using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "PlasticBallDataBase", menuName = "data/PlasticBallData", order = 1)]

    public class PlasticBallDataBase : ScriptableObject
    {
        public float bounce;
    }
}
