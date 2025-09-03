using CREMOT.GameplayUtilities;
using UnityEngine;

namespace GFM2025
{
    public class GameManager : GenericSingleton<GameManager>
    {


        private void Start()
        {
            InitGame();
        }

        private void InitGame()
        {
            if (PlayerBehaviour.Exist)
            {
               PlayerBehaviour.Instance.Init();
            }
            else
            {
                Debug.LogError("Error : No PlayerBehaviour singleton found in scene ! Init won't work properly !", this);
            }

            if (CameraBehaviour.Exist)
            {
                CameraBehaviour.Instance.Init();
            }
            else
            {
                Debug.LogError("Error : No CameraBehaviour singleton found in scene ! Init won't work properly !", this);
            }
        }
    }
}
