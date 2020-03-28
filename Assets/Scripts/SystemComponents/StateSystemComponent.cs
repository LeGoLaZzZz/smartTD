using System;
using Units;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(StateMachine))]
    public abstract class StateSystemComponent<TSystemStateType> : MonoBehaviour where TSystemStateType : Enum
    {
        public event Action StateChanged;

        [SerializeField] private TSystemStateType currentState;

        private StateMachine _stateMachine;


        private void OnEnable()
        {
            _stateMachine = GetComponent<StateMachine>();
            StateChanged += _stateMachine.OnSomethingStateChanged;
        }

        private void OnDisable()
        {
            StateChanged -= _stateMachine.OnSomethingStateChanged;
        }

        protected virtual void OnStateChanged()
        {
            StateChanged?.Invoke();
        }

        public TSystemStateType CurrentState
        {
            get => currentState;
            private set => currentState = value;
        }


        public void ChangeState(TSystemStateType newState)
        {
            _stateMachine.log += "\n " + newState;
//            if (Equals(newState, CurrentState)) return;
            CurrentState = newState;
            OnStateChanged();
        }

        //TODO TEMP
        public void SetState(TSystemStateType newState)
        {
            CurrentState = newState;
        }
    }
}