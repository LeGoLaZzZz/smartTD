using Buildings;
using UnityEngine;

namespace Infos
{
    public abstract class BuildingInfo : AnyObjectInfo<Building, BuildingType>
    {
        public abstract BuildingLevel[] GetLevels();

        public string title;
        public Sprite shopIcon;
        public GameObject blueprintPrefab;
        public float spawnPrice;
    }
}