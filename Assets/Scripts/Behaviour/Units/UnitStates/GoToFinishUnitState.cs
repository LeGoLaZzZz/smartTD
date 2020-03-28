using System;
using UnityEngine;

namespace Units
{
    public class GoToFinishUnitState : UnitState
    {
        public GoToFinishUnitState(Unit unit) : base(unit)
        {
        }

        protected override Type Tick()
        {
            throw new NotImplementedException();
        }

        public override Type Logic()
        {
            Unit.log += "\n GoToFinishUnitStateLogic";
            if (CanGoToZamok())
                return typeof(GoToFinishUnitState);


            return typeof(AttackingModeUnitState);
        }

        public override Type StartAction()
        {
            Unit.log += "\n GoToFinishUnitStateStartAction";
            Unit.AttackSystem.StopAttackOrder();
            Unit.MapMoveSystem.SetDestinationPosition(Unit.ZAMOK);
            return null;
        }
    }
}