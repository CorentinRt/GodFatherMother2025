using CREMOT.GameplayUtilities;
using UnityEngine;

namespace GFM2025
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/CameraData", order = 1)]
    public class CameraDatas : ScriptableObject
    {
        [Header("parameters")]
        [SerializeField] private float _followSmoothSpeed;
        [SerializeField] private float _lookAtSmoothSpeed;


        public float FollowSmoothSpeed => _followSmoothSpeed;
        public float LookAtSmoothSpeed => _lookAtSmoothSpeed;
    }
}
