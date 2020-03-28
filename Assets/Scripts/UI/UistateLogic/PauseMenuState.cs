using UnityEngine;

namespace UI.UiStateLogic
{
    [RequireComponent(typeof(Canvas))]
    public class PauseMenuState : UiState
    {
        public void OnResumeButtonClick()
        {
            changeState.Invoke(typeof(GameUiState));
        }

        public void OnQuitButtonClick()
        {
            SceneLoader.GetInstance().LoadSceneWithLoading("MainMenu");
        }
    }
}