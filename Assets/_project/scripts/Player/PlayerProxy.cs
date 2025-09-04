using UnityEngine;

namespace GFM2025
{
    public class PlayerProxy : MonoBehaviour, IPlayerBehaviour
    {
        [SerializeField] private PlayerBehaviour _playerBehaviour;

        public PlayerBehaviour GetPlayerBehaviour()
        {
            return _playerBehaviour;
        }
    }
}
