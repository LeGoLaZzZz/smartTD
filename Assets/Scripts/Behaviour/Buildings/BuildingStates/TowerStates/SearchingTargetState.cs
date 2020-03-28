using System;
using Buildings;

namespace Units
{
    public class SearchingTargetState : TowerBuildingState
    {
        public SearchingTargetState(TowerBuilding building) : base(building)
        {
        }

        protected override Type Tick()
        {
            throw new NotImplementedException();
        }

        public override Type Logic()
        {
            return null;
        }

        public override Type StartAction()
        {
            return null;
        }
    }
}