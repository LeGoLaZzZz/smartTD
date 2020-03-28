using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Units
{
    public class AttackingModeUnitState : UnitState
    {
        private List<IAttackable> _cantAttackables;
        private IAttackable _currentTarget;


        public AttackingModeUnitState(Unit unit) : base(unit)
        {
            _cantAttackables = new List<IAttackable>();
        }

        protected override Type Tick()
        {
            throw new NotImplementedException();
        }

        public bool TryAttackOrder(IAttackable target)
        {
            if (target == null)
            {
                return false;
            }

            if (Unit.AttackSystem.CanAttackOrder(target))
            {
                Unit.AttackSystem.TryAttackOrder(target);
                return true;
            }


            Unit.MapMoveSystem.SetDestinationPosition(
                target.GetNearestPoint(Unit.transform.position));
            return true;
        }


        public override Type Logic()
        {
            
            
            Unit.log += "\n AttackingModeUnitStateLogic";
            if (CanGoToZamok())
            {
                Unit.AttackSystem.StopAttackOrder();
                return typeof(GoToFinishUnitState);
            }


            if (Unit.AttackSystem.CurrentState == AttackSystem.AttackSystemState.NullTarget ||
                _currentTarget.IsGameObjectNull())
            {
                
                
                
                _currentTarget = Unit.AttackSystem.FindTarget();
                if (TryAttackOrder(_currentTarget))
                {
                    return null;
                }

                return typeof(NoTargetAndNoFinishUnitState);
            }


            if (Unit.MapMoveSystem.CurrentState == MapMoveSystem.MapMoveSystemStateType.ReachedThePoint)
            {
                if (Unit.AttackSystem.CanAttackOrder(_currentTarget))
                {
                    Unit.AttackSystem.TryAttackOrder(_currentTarget);
                    return null;
                }

                _cantAttackables.Add(_currentTarget);


                _currentTarget = Unit.AttackSystem.FindAnotherTarget(_cantAttackables);

                if (TryAttackOrder(_currentTarget))
                {
                    return null;
                }

                return typeof(NoTargetAndNoFinishUnitState);
            }


            if (Unit.AttackSystem.CurrentState == AttackSystem.AttackSystemState.OutOfDistance)
            {
                Unit.MapMoveSystem.SetDestinationPosition(
                    _currentTarget.GetNearestPoint(Unit.transform.position));
            }


            return null;
        }

        public override Type StartAction()
        {
            
            Unit.log += "\n AttackingModeUnitStateStartAction";
            _currentTarget = Unit.AttackSystem.FindTarget();
            if (TryAttackOrder(_currentTarget))
            {
                return null;
            }


            return typeof(NoTargetAndNoFinishUnitState);
        }
    }
}