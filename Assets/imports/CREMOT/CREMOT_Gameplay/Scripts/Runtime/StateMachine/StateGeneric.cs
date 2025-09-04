using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CREMOT.GameplayUtilities
{
    public abstract class StateGeneric<T> where T : System.Enum
    {
        private StateMachine<T> _stateMachine;


        public StateMachine<T> StateMachine => _stateMachine;


        public virtual T GetStateID()
        {
            return default;
        }
        public virtual void StateInit(StateMachine<T> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void StateEnter(T lastState)
        {

        }

        public virtual void StateExit(T nextState)
        {

        }

        public virtual void StateUpdate(float deltaTime)
        {

        }

        public virtual void StateLateUpdate(float deltaTime)
        {

        }

        public virtual void StateFixedUpdate(float fixedDeltaTime)
        {

        }
    }
}
