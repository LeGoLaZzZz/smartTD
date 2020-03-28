using Buildings;

namespace Units
{
    public abstract class BuildingState : State
    {
        protected Building Building;

        protected BuildingState(Building building) : base(building)
        {
            Building = building;
        }
    }
}