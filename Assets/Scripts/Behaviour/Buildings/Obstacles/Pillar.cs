using System;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Buildings
{
//TODO TEMP CODE
    public class Pillar : Building
    {
        public int uniqPillar;
        protected override void InitializeStates()
        {
            LogicStateFunctions = new Dictionary<Type, State>
            {
                {typeof(ChillBuildingState), new ChillBuildingState(this)},
            };
        }

        
        
        
        
    }
}