using System;
using Units;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Collider))]
    public class Castle : MonoBehaviour
    {
        public static Transform CastleTargetTransform;
        public float lifes;
        public event Action ZeroLifes;


        [SerializeField] private Transform targetTransform;
        [SerializeField] private CastleCanvas castleCanvas;

        private static Castle instance;
        public static Castle GetInstance() => instance;


        private void Awake()
        {
            instance = this;
            CastleTargetTransform = targetTransform;
        }


        public void GetDamage(float damage)
        {
            if (lifes - damage > 0)
            {
                lifes -= damage;
                castleCanvas.SetLifes(lifes);
            }
            else
            {
                castleCanvas.SetLifes(0);
                OnZeroLifes();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            var unit = other.GetComponent<Unit>();
            if (unit)
            {
                GetDamage(unit.CastleDamage);
                unit.DefenseSystem.DestroyObject();
            }
        }

        protected virtual void OnZeroLifes()
        {
            ZeroLifes?.Invoke();
        }
    }
}