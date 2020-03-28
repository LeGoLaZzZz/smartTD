using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GoldInfoPanel : MonoBehaviour
    {
        public Text goldText;

        private void Start()
        {
            GoldManager.GetInstance().GoldAmountChanged += GoldChanged;
        }

        private void GoldChanged(float goldAmount)
        {
            goldText.text = goldAmount.ToString();
        }
    }
}