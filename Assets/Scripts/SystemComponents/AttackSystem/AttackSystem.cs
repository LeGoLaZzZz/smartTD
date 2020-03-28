using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class AttackSystem : StateSystemComponent<AttackSystem.AttackSystemState>
    {
        public abstract IAttackable FindTarget();
        public abstract IAttackable FindAnotherTarget(List<IAttackable> current);

        public abstract bool TryAttackOrder(IAttackable attackable);

        public abstract void StopAttackOrder();


        [SerializeField] protected float damage;
        [SerializeField] protected float distance;
        [SerializeField] protected float cooldown;


        protected float LastCooldownTime;
        protected Transform AttackerTransform;

        public IAttackable Target { get; private set; }

        public enum AttackSystemState
        {
            Sleep,
            Attacking,
            OutOfDistance,
            NullTarget,
        }


        protected virtual void Awake()
        {
            AttackerTransform = transform;
        }


        protected virtual void Start()
        {
            SetState(AttackSystemState.Sleep);
        }
 

        protected void SetTarget(IAttackable target)
        {
            Target = target;
            if (Target != null) Target.DefenseSystem.ObjectDestroyed += OnTargetDead;
        }

        public void OnTargetDead(IAttackable target)
        {
            target.DefenseSystem.ObjectDestroyed -= OnTargetDead;
            ChangeState(AttackSystemState.NullTarget);
        }


        public void SetUpParameters(float damage, float distance, float cooldown)
        {
            this.damage = damage;
            this.distance = distance;
            this.cooldown = cooldown;
        }


        protected virtual void TransferDamage(IAttackable attackable)
        {
            attackable.DefenseSystem.GetDamage(damage);
        }


        public virtual bool CanGetOnDistance(IAttackable attackable)
        {
            if (AttackerTransform == null) AttackerTransform = transform;
            var position = AttackerTransform.position;
            return distance >= (position - attackable.GetNearestPoint(position))
                   .magnitude;
        }


        public bool CanAttackOrder(IAttackable attackable)
        {
            if (attackable.IsGameObjectNull())
            {
                return false;
            }

            var succes = CanGetOnDistance(attackable);
            return succes;
        }

        protected bool CanCoolDownAttack()
        {
            return Time.time - LastCooldownTime > cooldown;
        }
    }
}