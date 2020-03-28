using System;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;

namespace Units
{
    public abstract class UnitState : State
    {
        protected Unit Unit;

        protected UnitState(Unit unit) : base(unit)
        {
            Unit = unit;
        }


        public bool CanGoToZamok()
        {
            Unit.UpdateCanGetToZAMOK();
            if (Unit.CanGetToZAMOK)
            {
                return true;
            }

            return false;
        }
    }
}