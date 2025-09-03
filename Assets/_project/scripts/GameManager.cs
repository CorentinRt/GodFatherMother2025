using CREMOT.GameplayUtilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GFM2025
{
    public enum GAME_STATE
    {
        PRE_GAME = 0,
        SCORING = 1,
        RETURN_HOME = 2,
        WATER_DECREASE = 3,
        END_GAME = 4
    }

    public class GameManager : GenericSingleton<GameManager>
    {
        [Header("Datas")]
        [SerializeField] private MainGameData _data;

        private GAME_STATE _currentGameState;

        private Coroutine _currentGameStateCoroutine;


        public GAME_STATE CurrentGameState => _currentGameState;


        public event Action<GAME_STATE> onGameStateChanged;
        public UnityEvent onGameStateChangedUnity;



        private void Start()
        {
            InitManagers();

            InitGameState();
        }

        #region Init
        private void InitManagers()
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

            if (ScoreManager.Exist)
            {
                ScoreManager.Instance.Init();
            }
            else
            {
                Debug.LogError("Error : No ScoreManager singleton found in scene ! Init won't work properly !", this);
            }

            if (MapBehaviour.Exist)
            {
                MapBehaviour.Instance.Init();
            }
            else
            {
                Debug.LogError("Error : No MapBehaviour singleton found in scene ! Init won't work properly !", this);
            }
        }
        #endregion

        #region Game State

        private void InitGameState()
        {
            SwitchGameState(GAME_STATE.PRE_GAME);
        }

        private void SwitchGameState(GAME_STATE gameState)
        {
            switch (gameState)
            {
                case GAME_STATE.PRE_GAME:
                    _currentGameState = GAME_STATE.PRE_GAME;
                    break;

                case GAME_STATE.SCORING:
                    _currentGameState = GAME_STATE.SCORING;
                    break;

                case GAME_STATE.RETURN_HOME:
                    _currentGameState = GAME_STATE.RETURN_HOME;
                    break;
                
                case GAME_STATE.WATER_DECREASE:
                    _currentGameState = GAME_STATE.WATER_DECREASE;
                    break;

                case GAME_STATE.END_GAME:
                    _currentGameState = GAME_STATE.END_GAME;
                    break;

                default:
                    break;
            }

            Debug.Log($"Switch to state {_currentGameState.ToString()}", this);

            StartCurrentStateCoroutine();

            onGameStateChanged?.Invoke(_currentGameState);
            onGameStateChangedUnity?.Invoke();
        }

        private void StartCurrentStateCoroutine()
        {
            StopCurrentStateCoroutine();

            switch (_currentGameState)
            {
                case GAME_STATE.PRE_GAME:
                    _currentGameStateCoroutine = StartCoroutine(PreGameStateCoroutine());
                    break;

                case GAME_STATE.SCORING:
                    _currentGameStateCoroutine = StartCoroutine(ScoringStateCoroutine());
                    break;

                default:
                    break;
            }
        }

        private void StopCurrentStateCoroutine()
        {
            if (_currentGameStateCoroutine != null)
            {
                StopCoroutine(_currentGameStateCoroutine);
                _currentGameStateCoroutine = null;
            }
        }

        private IEnumerator PreGameStateCoroutine()
        {
            yield return new WaitForSeconds(_data.PreGameCooldown);

            SwitchGameState(GAME_STATE.SCORING);

            yield return null;
        }

        private IEnumerator ScoringStateCoroutine()
        {
            float randomDuration = UnityEngine.Random.Range(_data.ScoringPhaseMinDuration, _data.ScoringPhaseMaxDuration);

            yield return new WaitForSeconds(randomDuration);

            SwitchGameState(GAME_STATE.WATER_DECREASE);

            yield return null;
        }

        #endregion

        #region Siphon
        public void PlayerEntersSiphonZone()
        {
            if (_currentGameState != GAME_STATE.WATER_DECREASE)
                return;

            SwitchGameState(GAME_STATE.RETURN_HOME);
        }
        #endregion

        #region Home / Score Zone
        public void PlayerEntersHomeZone()
        {
            if (_currentGameState != GAME_STATE.RETURN_HOME)
                return;

            SwitchGameState(GAME_STATE.SCORING);
        }
        #endregion

        public void WaterEmpty()
        {

        }

        private void EndGame()
        {
            if (_currentGameState == GAME_STATE.PRE_GAME || _currentGameState == GAME_STATE.END_GAME)
                return;

            SwitchGameState(GAME_STATE.END_GAME);
        }
    }
}
