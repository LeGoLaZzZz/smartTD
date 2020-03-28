using System;
using JetBrains.Annotations;

namespace Units
{
    public abstract class State
    {
        protected abstract Type Tick();

        //TODO TEMP
        [CanBeNull]
        public abstract Type Logic();

        [CanBeNull]
        public abstract Type StartAction();

        protected StateMachine ControlledGameObject;

        public State(StateMachine controlledGameObject)
        {
            ControlledGameObject = controlledGameObject;
        }
    }
}