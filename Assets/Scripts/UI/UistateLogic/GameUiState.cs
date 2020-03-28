using UnityEngine;

namespace UI.UiStateLogic
{
    [RequireComponent(typeof(Canvas))]
    public class GameUiState : UiState
    {
        [SerializeField] private GameObject pauseBackground;


        public override void StartAction()
        {
            base.StartAction();
            pauseBackground.SetActive(false);


            Time.timeScale = 1; //TODO: should it be here?
        }


        public override void EndAction()
        {
//        gameUiStateCanvas.enabled = false;
            pauseBackground.SetActive(true);


            Time.timeScale = 0; //TODO: should it be here?
        }


        public void OnPauseButtonClick()
        {
            changeState.Invoke(typeof(PauseMenuState));
        }

        public void Lose()
        {
            changeState.Invoke(typeof(LoseMenuState));
        }

        public void Win()
        {
            changeState.Invoke(typeof(WinMenuState));
        }
    }
}