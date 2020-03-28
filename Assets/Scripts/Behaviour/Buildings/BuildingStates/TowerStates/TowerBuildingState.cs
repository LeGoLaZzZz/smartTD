using System.Collections.Generic;
using Buildings;
using DefaultNamespace;

namespace Units
{
    public abstract class TowerBuildingState : BuildingState
    {
        protected TowerBuilding towerBuilding;

        protected TowerBuildingState(TowerBuilding building) : base(building)
        {
            towerBuilding = building;
        }
    }
}