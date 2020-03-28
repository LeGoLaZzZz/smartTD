//TODO TEMP CODE

using System;
using System.Collections.Generic;
using DefaultNamespace;
using Units;
using UnityEngine;

namespace Buildings
{
    [RequireComponent(typeof(StandartDefenseSystem))]
    public class SimpleWall : Building
    {
        public int uniqSimpleWallField;


        protected override void InitializeStates()
        {
            LogicStateFunctions = new Dictionary<Type, State>
            {
                {typeof(ChillBuildingState), new ChillBuildingState(this)},
            };
        }
    }
}