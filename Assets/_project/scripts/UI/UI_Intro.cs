using CREMOT.GameplayUtilities;
using UnityEngine;
using UnityEngine.Events;

namespace GFM2025
{
    public class UI_Intro : GenericSingleton<UI_Intro>
    {
        private bool _hasPlayed;

        private void Start()
        {
            GameManager.Instance.onGameStateChanged += OnGameStateChanged;

            onStartGame?.Invoke();
        }

        private void OnDestroy()
        {
            GameManager.Instance.onGameStateChanged -= OnGameStateChanged;
        }


        public UnityEvent onStartGame;

        public UnityEvent onNotifyHideIndication;


        private void OnGameStateChanged(GAME_STATE state)
        {
            if (state == GAME_STATE.PRE_GAME)
                return;

            if (_hasPlayed)
                return;

            _hasPlayed = true;

            onNotifyHideIndication?.Invoke();
        }
    }
}
