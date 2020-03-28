using System.Collections.Generic;
using DefaultNamespace;
using Units;
using UnityEngine;

namespace Buildings
{
    public abstract class TowerBuilding : Building, ICanAttack
    {
        public AttackSystem AttackSystem { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            AttackSystem = GetComponent<AttackSystem>();
        }


        protected override void Start()
        {
            ChangeLogicState(typeof(AttackingBuildingState));
        }
    }
}