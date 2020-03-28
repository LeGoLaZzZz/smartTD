using System;

namespace Units
{
    public class NoTargetAndNoFinishUnitState : UnitState
    {
        public NoTargetAndNoFinishUnitState(Unit unit) : base(unit)
        {
        }

        protected override Type Tick()
        {
            throw new NotImplementedException();
        }

        public override Type Logic()
        {
            if (CanGoToZamok())
                return typeof(GoToFinishUnitState);


            return Unit.AttackSystem.FindTarget() != null
                ? typeof(AttackingModeUnitState)
                : typeof(NoTargetAndNoFinishUnitState);
        }

        public override Type StartAction()
        {
            return null;
        }
    }
}