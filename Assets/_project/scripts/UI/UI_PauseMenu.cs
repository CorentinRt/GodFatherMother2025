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

            PlayerBehaviour.Instance.onPressPause += DisplayUI;
        }

        private void OnDestroy()
        {
            PlayerBehaviour.Instance.onPressPause -= DisplayUI;
        }

        private void ToggleUI()
        {
            if (_pauseContainer.activeSelf)
            {
                HideUI();
            }
            else
            {
                DisplayUI();
            }
        }

        public void DisplayUI()
        {
            _pauseContainer.SetActive(true);

            PauseGame();
        }

        public void HideUI()
        {
            _pauseContainer.SetActive(false);

            OnPressResumeGame();
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void OnPressResumeGame()
        {
            Time.timeScale = 1f;
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
