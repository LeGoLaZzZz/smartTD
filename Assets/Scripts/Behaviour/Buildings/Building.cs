using System;
using System.Collections.Generic;
using DefaultNamespace;
using Spawners;
using Units;
using UnityEngine;

namespace Buildings
{
    public abstract class Building : StateMachine, IAttackable
    {
        protected abstract override void InitializeStates();


        public DefenseSystem DefenseSystem { get; private set; }
        public BuildingType Type => type;

        public float sellCost;
        public float upgradeCost;

        [SerializeField] private BuildingType type;

        
        
        private Transform _transform;
        private Collider _collider;


        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
            DefenseSystem = GetComponent<DefenseSystem>();

            _collider = GetComponent<Collider>();
        }

        protected virtual void Start()
        {
            ChangeLogicState(typeof(ChillBuildingState));

            DefenseSystem.ObjectDestroyed += OnObjectDestroyed;
        }

        private void OnObjectDestroyed(IAttackable attackable)
        {
            BuildingSpawner.GetInstance().OnMapChanched();
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


        public virtual Vector3 GetNearestPoint(Vector3 position)
        {
            if (_collider != null)
                return _collider.ClosestPoint(position);
            else
                return _transform.position;
        }


        public Vector3 GetPosition()
        {
            return _transform.position;
        }


        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}