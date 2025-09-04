using UnityEngine;

namespace GFM2025
{
    public class UI_EndGame : MonoBehaviour
    {
        [SerializeField] private GameObject _endGameContainer;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            GameManager.Instance.onGameStateChanged += OnReceiveChangeGameState;

            HideUI();
        }

        private void OnDestroy()
        {
            GameManager.Instance.onGameStateChanged -= OnReceiveChangeGameState;

        }

        private void OnReceiveChangeGameState(GAME_STATE state)
        {
            if (state != GAME_STATE.END_GAME)
            {
                HideUI();
                return;
            }

            DisplayUI();
        }

        public void DisplayUI()
        {
            _endGameContainer.SetActive(true);
        }

        public void HideUI()
        {
            _endGameContainer.SetActive(false);
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
