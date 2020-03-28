using System;
using UnityEngine;

namespace Infos.levels
{
    [Serializable]
    public class ObstacleBuildingLevel : BuildingLevel
    {
        public float health;
        public int cost;
        public Color color;
    }
}