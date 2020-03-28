using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public abstract class StateMachine : AnyObject
    {
        protected abstract void InitializeStates();


        public Type curStateType;
        public string curStateTypestring;
        public event Action LogicStateChanged;

        
        public string log;
        
        protected State CurrentStateLogic;
        protected Dictionary<Type, State> LogicStateFunctions;

        protected virtual void Awake()
        {
            InitializeStates();
        }


        protected virtual void OnLogicStateChanged()
        {
            LogicStateChanged?.Invoke();
        }


        public virtual void OnSomethingStateChanged()
        {
            var nextState = CurrentStateLogic.Logic();
            if (nextState != null)
            {
                ChangeLogicState(nextState);
            }
        }


        public void ChangeLogicState(Type mode)
        {
            CurrentStateLogic = LogicStateFunctions[mode];
            curStateType = mode;
            curStateTypestring = mode.ToString();
            var startAction = CurrentStateLogic.StartAction();
            if (startAction != null) ChangeLogicState(mode);
        }
    }
}