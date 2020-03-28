using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.UiStateLogic
{
    [RequireComponent(typeof(Canvas))]
    public class WinMenuState : UiState
    {
        public void OnAgainButtonClick()
        {
            SceneLoader.GetInstance().LoadSceneWithLoading(SceneManager.GetActiveScene().name);
        }

        public void OnQuitButtonClick()
        {
            SceneLoader.GetInstance().LoadSceneWithLoading("MainMenu");
        }
    }
}