using System;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using DefaultNamespace;
using JetBrains.Annotations;
using Units;
using UnityEngine;

namespace Behaviour.Buildings
{
    public class SimpleColliderTower : TowerBuilding
    {
        protected override void InitializeStates()
        {
            LogicStateFunctions = new Dictionary<Type, State>
            {
                {typeof(ChillBuildingState), new ChillBuildingState(this)},
                {typeof(AttackingBuildingState), new AttackingBuildingState(this)},
                {typeof(SearchingTargetState), new SearchingTargetState(this)},
            };
        }
    }
}