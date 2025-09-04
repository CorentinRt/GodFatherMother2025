using UnityEngine;

namespace GFM2025
{
    public class UI_PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseContainer;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            HideUI();
        }

        public void DisplayUI()
        {
            _pauseContainer.SetActive(true);
        }

        public void HideUI()
        {
            _pauseContainer.SetActive(false);
        }

        public void OnPressRestartGame()
        {
            GameManager.Instance.OpenGameScene();
        }

        public void OnPressGoToMenu()
        {
            GameManager.Instance.OpenMenuScene();
        }
    }
}
