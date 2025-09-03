using CREMOT.GameplayUtilities;
using UnityEngine;

namespace GFM2025
{
    public class PlayerBehaviour : GenericSingleton<PlayerBehaviour>
    {
        [Header("Datas")]
        [SerializeField] private PlayerDatas _data;

        [Header("Movements")]
        [SerializeField] private Rigidbody _rb;

        public PlayerDatas Data => _data;


        public void InitPlayer()
        {
            
        }


        private void FixedUpdate()
        {
            UpdateMovement(Time.fixedDeltaTime);

            UpdateMoveRotation(Time.fixedDeltaTime);
        }

        private void UpdateMovement(float deltaTime)
        {



        }

        private void UpdateMoveRotation(float deltaTime)
        {

        }

    }
}
