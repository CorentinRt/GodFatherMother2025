using CREMOT.GameplayUtilities;
using UnityEngine;

namespace GFM2025
{
    public class GameManager : GenericSingleton<GameManager>
    {


        private void Start()
        {
            
        }

        private void InitGame()
        {
            if (PlayerBehaviour.Exist)
            {
               PlayerBehaviour.Instance.InitPlayer();
            }
            else
            {
                Debug.LogError("Error : No PlayerBehaviour singleton found in scene ! Init won't work properly !", this);
            }


        }
    }
}
