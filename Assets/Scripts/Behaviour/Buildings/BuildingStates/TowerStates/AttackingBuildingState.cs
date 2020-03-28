using System;
using Buildings;
using UnityEngine;

namespace Units
{
    public class AttackingBuildingState : TowerBuildingState
    {
        public AttackingBuildingState(TowerBuilding building) : base(building)
        {
        }

        protected override Type Tick()
        {
            throw new NotImplementedException();
        }

        public override Type Logic()
        {
            var attackable = towerBuilding.AttackSystem.FindTarget();
            if (attackable != null)
            {
                towerBuilding.AttackSystem.TryAttackOrder(attackable);
            }


            return null;
        }

        public override Type StartAction()
        {
            var attackable = towerBuilding.AttackSystem.FindTarget();
            if (attackable != null)
            {
                towerBuilding.AttackSystem.TryAttackOrder(attackable);
            }

            return null;
        }
    }
}