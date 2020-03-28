using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using JetBrains.Annotations;
using UnityEngine;

namespace DefaultNamespace
{
    public class StandartTowerAttackSystem : AttackSystem
    {
        public GameObject projectile;


        [SerializeField] private List<IAttackable> enemy;
        [SerializeField] private Vector2 speed1;
        [SerializeField] private Vector2 speed2;

        //TODO корутин?? нужен ли
        private IEnumerator _attackLoopCoroutine;

        protected override void Awake()
        {
            base.Awake();
            enemy = new List<IAttackable>();
        }


        protected override void Start()
        {
            base.Start();
            GetComponentInChildren<DistanceColliderForTower>().GetComponent<BoxCollider>().size =
                new Vector3(distance, distance, distance);
        }

        // TODO А ЕСЛИ ИЗ ОДНОЙ КОМАНДЫ СДЕЛАТЬ ПРОВЕРКИ
        public void NewTargetEnter(IAttackable attackable)
        {
            attackable.DefenseSystem.ObjectDestroyed += OnEnemyDestroy;
            enemy.Add(attackable);
            OnStateChanged();
        }


        public void NewTargetExit(IAttackable attackable)
        {
            enemy.Remove(attackable);
            attackable.DefenseSystem.ObjectDestroyed -= OnEnemyDestroy;
            OnStateChanged();
        }


        [CanBeNull]
        public IAttackable FindNearest()
        {
            if (enemy.Count == 0) return null;


            var mbtarget = enemy
                .OrderBy(enemy =>
                {
                    Vector3 position;
                    return ((position = transform.position) - enemy.GetNearestPoint(position)).magnitude;
                })
                .First();


            return mbtarget;
        }


        void OnEnemyDestroy(IAttackable enemy)
        {
            this.enemy.Remove(enemy);
        }


        public override IAttackable FindTarget()
        {
            return FindNearest();
        }

        public override IAttackable FindAnotherTarget(List<IAttackable> current)
        {
            throw new NotImplementedException();
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
            _attackLoopCoroutine = AttackLoop(attackable);
            StartCoroutine(_attackLoopCoroutine);
            return true;
        }


        public override void StopAttackOrder()
        {
            if (_attackLoopCoroutine != null) StopCoroutine(_attackLoopCoroutine);
        }


        //TODO переделать на делегаты и состояния 
        private IEnumerator AttackLoop(IAttackable attackable)
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


                    SpawnBomb(attackable);


                    LastCooldownTime = Time.time;
                }

                yield return null;
            }
        }


        public void SpawnBomb(IAttackable iattackable)
        {
            var newObj = Instantiate(projectile);
            newObj.transform.position = transform.position;
            var bulletComp = newObj.GetComponent<Projectile>();
            bulletComp.damage = damage;
            bulletComp.SetTarget(transform.position,
                iattackable.GetGameObject().transform,
                speed1, speed2);
        }
    }
}