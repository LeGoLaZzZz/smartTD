using System;
using UnityEngine;

namespace UI.UiStateLogic
{
    public abstract class UiState : MonoBehaviour
    {
        public Action<Type> changeState;


        [SerializeField] private Canvas stateCanvas;


        protected virtual void Awake()
        {
            stateCanvas = GetComponent<Canvas>();
        }


        public virtual void StartAction()
        {
            stateCanvas.enabled = true;
        }

        public virtual void EndAction()
        {
            stateCanvas.enabled = false;
        }
    }
}