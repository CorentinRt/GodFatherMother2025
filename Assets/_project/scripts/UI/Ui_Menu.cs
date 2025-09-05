using UnityEngine;
using UnityEngine.SceneManagement;

namespace GFM2025
{
    public class Ui_Menu : MonoBehaviour
    {

        [SerializeField] private LevelData _levelData;

        public void Exit()
        {
            Application.Quit();
        }

        public void LunchGame()
        {
            SceneManager.LoadScene(_levelData.GameName);
        }
    }
}
