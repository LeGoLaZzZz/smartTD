using System;
using UI.UiStateLogic;
using UnityEngine;

namespace DefaultNamespace
{
    public class WinLoseSystem : MonoBehaviour
    {
        public float levelCastleLifes;


        private void Start()
        {
            Castle.GetInstance().lifes = levelCastleLifes;
            Castle.GetInstance().ZeroLifes += Lose;
        }

        private void OnDisable()
        {
            Castle.GetInstance().ZeroLifes -= Lose;
        }

        public void Lose()
        {
            UiStateLogic.GetInstance().ChangeState(typeof(LoseMenuState));
        }
    }
}