using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GoldManager : MonoBehaviour
    {
        public event Action<float> GoldAmountChanged;

        public float Gold
        {
            get => gold;
            private set
            {
                gold = value;
                OnGoldAmountChanged(value);
            }
        }


        private static GoldManager _instance;
        [SerializeField] private float gold;
        public static GoldManager GetInstance() => _instance;


        private void Awake()
        {
            _instance = this;
            Gold = gold;
        }


        public bool TryTakeGold(float goldAmount)
        {
            if (Gold - goldAmount < 0)
            {
                return false;
            }
            else
            {
                Gold -= goldAmount;
                return true;
            }
        }

        public void AddGold(float addAmount)
        {
            Gold += addAmount;
        }


        protected virtual void OnGoldAmountChanged(float obj)
        {
            GoldAmountChanged?.Invoke(obj);
        }
    }
}