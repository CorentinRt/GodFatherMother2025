using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CREMOT.GameplayUtilities
{
    public abstract class StateMachine<T> : MonoBehaviour where T : System.Enum
    {
        [SerializeField] private List<T> _statesInStateMachine;

        protected StateGeneric<T> _currentState;

        protected MonoBehaviour _caller;

        protected List<StateGeneric<T>> _allStates = new List<StateGeneric<T>>();

        public List<T> StatesInStateMachine => _statesInStateMachine;

        public MonoBehaviour Caller => _caller;

        public virtual void Init(MonoBehaviour caller)
        {
            _caller = caller;

            CreateObjectStates();

            InitAllStates();

        }

        protected virtual void Update()
        {
            if (_currentState != null)
            {
                _currentState.StateUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (_currentState != null)
            {
                _currentState.StateLateUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (_currentState != null)
            {
                _currentState.StateFixedUpdate(Time.fixedDeltaTime);
            }
        }

        protected StateGeneric<T> GetStateByStateType(T stateType)
        {
            foreach (StateGeneric<T> state in _allStates)
            {
                if (state == null)
                {
                    continue;
                }

                if (EqualityComparer<T>.Default.Equals(state.GetStateID(), stateType))
                {
                    return state;
                }
            }

            return null;
        }

        public StateGeneric<T> GetCurrentState()
        {
            return _currentState;
        }

        public T GetCurrentStateID()
        {
            if (_currentState == null)
            {
                return default;
            }

            return _currentState.GetStateID();
        }

        protected void InitAllStates()
        {
            foreach (StateGeneric<T> state in _allStates)
            {
                if (state == null)
                {
                    continue;
                }

                state.StateInit(this);
            }
        }

        public void ChangeState(T nextStateID)
        {
            StateGeneric<T> nextState = GetStateByStateType(nextStateID);

            if (nextState == null)
            {
                Debug.LogError($"Error : StateMachine Tried to switch to state {nextStateID.ToString()}, but it doesn't exist.", this);
                return;
            }

            if (_currentState != null)
            {
                _currentState.StateExit(nextStateID);
            }

            T lastState = _currentState != null ? _currentState.GetStateID() : default;

            _currentState = nextState;

            _currentState.StateEnter(lastState);
        }

        protected virtual void CreateObjectStates()
        {
            if (_allStates != null)
            {
                _allStates.Clear();
            }

            foreach (T stateID in _statesInStateMachine)
            {
                CreateStateByID(stateID);
            }
        }

        protected abstract void CreateStateByID(T stateID);
    }
}
