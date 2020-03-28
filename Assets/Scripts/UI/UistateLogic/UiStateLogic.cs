using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.UiStateLogic
{
    public class UiStateLogic : MonoBehaviour
    {
        private UiState _activeState;
        [SerializeField] private Dictionary<Type, UiState> statePanels = new Dictionary<Type, UiState>();


        private static UiStateLogic _instance;
        public static UiStateLogic GetInstance() => _instance;


        private void Awake()
        {
            _instance = this;
            var statePanelsMas = GetComponentsInChildren<UiState>();

            foreach (var statePanel in statePanelsMas)
            {
                statePanels.Add(statePanel.GetType(), statePanel);
                statePanel.changeState = ChangeState;
                statePanel.EndAction();
            }

            ChangeState(typeof(GameUiState));
        }


//    public enum MenuStates
//    {
//        Pause,
//        Lose,
//        Win,
//        Game
//    }

        public void ChangeState(Type newState)
        {
            var uiStatePanel = statePanels[newState];

            _activeState?.EndAction();
            uiStatePanel.StartAction();
            _activeState = uiStatePanel;
        }
    }
}