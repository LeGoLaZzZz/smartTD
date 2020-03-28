using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;

namespace DefaultNamespace
{
    public class StandartUnitAttackSystem : AttackSystem
    {
        //TODO корутин?? нужен ли
        private IEnumerator _attackLoopCoroutine;
        private List<IAttackable> _cantAttackables;


        //TODO ПОЧИНИТЬ FindTarget SpawnedBuildings
        public override IAttackable FindTarget()
        {
            return FindAnotherTarget(new List<IAttackable>());
        }

        public override IAttackable FindAnotherTarget(List<IAttackable> current)
        {
            var directionMagnitude = float.MaxValue;
            IAttackable closestObject = null;

            foreach (var spawnedMapObject in FindObjectsOfType<Building>())
            {
                var currentMagnitude = GetDirectionMagnitude(spawnedMapObject.transform);
                var curAttacable = (IAttackable) spawnedMapObject;
                if (directionMagnitude > currentMagnitude && !current.Contains(curAttacable))
                {
                    directionMagnitude = currentMagnitude;
                    closestObject = curAttacable;
                }
            }

            return closestObject;
        }


        private float GetDirectionMagnitude(Transform objectTransform)
        {
            return (objectTransform.position - transform.position).magnitude;
        }


        public override bool TryAttackOrder(IAttackable attackable)
        {
            if (attackable.IsGameObjectNull())
            {
                return false;
            }

            var succes = CanGetOnDistance(attackable);
            if (!succes) return false;

            SetTarget(attackable);
            SetState(AttackSystemState.Attacking);
            _attackLoopCoroutine = AttackLoop(attackable, AttackerTransform);
            StartCoroutine(_attackLoopCoroutine);
            return true;
        }


        public override void StopAttackOrder()
        {
            if (_attackLoopCoroutine != null) StopCoroutine(_attackLoopCoroutine);
        }


        //TODO переделать на делегаты и состояния 
        private IEnumerator AttackLoop(IAttackable attackable, Transform attackerTransform)
        {
            yield return null; // для пропуска кадра иначе stackoverflow хз почему
            while (true)
            {
                if (CanCoolDownAttack())
                {
                    if (attackable.IsGameObjectNull())
                    {
                        ChangeState(AttackSystemState.NullTarget);
                        yield break;
                    }


                    if (!CanGetOnDistance(attackable))
                    {
                        ChangeState(AttackSystemState.OutOfDistance);
                        yield break;
                    }

                    TransferDamage(attackable);
                    LastCooldownTime = Time.time;
                }

                yield return null;
            }
        }
    }
}