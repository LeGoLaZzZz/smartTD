using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Spawners;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public abstract class Unit : StateMachine, IMapMoveable, IAttackable,
        ICanAttack
    {
        //TODO ZAMOK POS
        public static readonly Vector3 ZAMOK = new Vector3(11, 3, 11);

        public bool CanGetToZAMOK { get; set; }

        [field: SerializeField] public float GoldGain { get; set; }
        [field: SerializeField] public float CastleDamage { get; set; }

        public AttackSystem AttackSystem { get; private set; }
        public DefenseSystem DefenseSystem { get; private set; }
        public MapMoveSystem MapMoveSystem { get; private set; }
        public StateMachine StateMachine { get; private set; }


       
        
        private Collider _collider;
        private Transform _transform;

        private void OnDisable()
        {
            BuildingSpawner.GetInstance().MapChanched -= OnMapChangedEvent;
            DefenseSystem.ObjectKilled -= GainGoldForKill;
        }


        protected override void Awake()
        {
            base.Awake();

            AttackSystem = GetComponent<AttackSystem>();
            MapMoveSystem = GetComponent<MapMoveSystem>();
            MapMoveSystem.Agent = GetComponent<NavMeshAgent>();
            DefenseSystem = GetComponent<DefenseSystem>();
            StateMachine = GetComponent<StateMachine>();
            _transform = transform;
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            BuildingSpawner.GetInstance().MapChanched += OnMapChangedEvent;
            DefenseSystem.ObjectKilled += GainGoldForKill;
            ChangeLogicState(typeof(GoToFinishUnitState));
            UpdateCanGetToZAMOK();
        }

        //TODO: где эта логика должна быть?
        private void GainGoldForKill(IAttackable obj)
        {
            GoldManager.GetInstance().AddGold(GoldGain);
        }


        public virtual Vector3 GetNearestPoint(Vector3 position)
        {
            return _collider != null ? _collider.ClosestPoint(position) : _transform.position;
        }


        public GameObject GetGameObject()
        {
            return gameObject;
        }


        public bool IsGameObjectNull()
        {
            try
            {
                var o = gameObject;
            }
            catch (MissingReferenceException e)
            {
                return true;
            }

            return false;
        }


        protected override void InitializeStates()
        {
            LogicStateFunctions = new Dictionary<Type, State>
            {
                {typeof(GoToFinishUnitState), new GoToFinishUnitState(this)},
                {typeof(AttackingModeUnitState), new AttackingModeUnitState(this)},
                {typeof(NoTargetAndNoFinishUnitState), new NoTargetAndNoFinishUnitState(this)},
            };
        }


        public void OnMapChangedEvent()
        {
            log += "\n OnMapChangedEvent";
            UpdateCanGetToZAMOK();
            OnSomethingStateChanged();
        }

        public void UpdateCanGetToZAMOK()
        {
            CanGetToZAMOK = MapMoveSystem.CanGetToPosition(ZAMOK);
        }

        public Vector3 GetPosition()
        {
            return _transform.position;
        }
    }
}