using System;
using System.Collections;
using Units;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public abstract class DefenseSystem : MonoBehaviour
    {
        public IAttackable thisAttackable;
        public event Action<IAttackable> ObjectKilled;
        public event Action<IAttackable> ObjectDestroyed;
        public event EventHandler _ObjectDestroyed;

        public event Action DamagedEvent;

        [SerializeField] private float maxHealthPoints;
        [SerializeField] private float healthPoints;


        public float HealthPoints
        {
            get => healthPoints;
            private set => healthPoints = value;
        }

        public float MaxHealthPoints
        {
            get => maxHealthPoints;
            private set => maxHealthPoints = value;
        }


        private void Awake()
        {
            thisAttackable = GetComponent<IAttackable>();
        }

        private void Start()
        {
            HealthPoints = MaxHealthPoints;
        }

        public void SetMaxHealthPoints(float value)
        {
            MaxHealthPoints = value;
        }


        public virtual void GetDamage(float damage)
        {
            if (HealthPoints - damage > 0)
            {
                HealthPoints -= damage;
                OnDamagedEvent();
            }
            else
            {
                Die();
            }
        }

        protected virtual void OnObjectDestroyed()
        {
            ObjectDestroyed?.Invoke(thisAttackable);
        }

        protected virtual void OnObjectKilled()
        {
            ObjectKilled?.Invoke(thisAttackable);
        }

        private void Die()
        {
            OnObjectKilled();
            DestroyObject();
        }

        public void DestroyObject()
        {
            OnObjectDestroyed();
            Destroy(gameObject);
        }


        protected virtual void OnDamagedEvent()
        {
            DamagedEvent?.Invoke();
        }

        protected virtual void OnObjectDestroyed_()
        {
            _ObjectDestroyed?.Invoke(this, EventArgs.Empty);
        }
    }
}