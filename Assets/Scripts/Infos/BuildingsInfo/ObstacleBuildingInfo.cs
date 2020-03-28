using System;
using Buildings;
using DefaultNamespace;
using Infos.levels;
using UnityEngine;

namespace Infos
{
    [CreateAssetMenu(menuName = "MyObjects/BuildingParameters/ObstacleBuildingInfo", fileName = "obstacleBuildingInfo")]
    public class ObstacleBuildingInfo : BuildingInfo
    {
        public ObstacleBuildingLevel[] obstacleLevels;


        public override void SetParametersTo(Building objectScript, int level = 0)
        {
            if (level >= obstacleLevels.Length) level = obstacleLevels.Length - 1;
            objectScript.Level = level;


            var obstacleLevel = obstacleLevels[level];

            objectScript.sellCost = obstacleLevel.sellCost;
            objectScript.upgradeCost = obstacleLevel.upgradeCost;

            if (!(objectScript is IAttackable attackable))
                throw new ArgumentException("Expected ObstacleBuilding, object doesnt have IAttackable");


            attackable.DefenseSystem.SetMaxHealthPoints(obstacleLevel.health);

            //TODO tempcode
            objectScript.GetComponent<MeshRenderer>().material.color = obstacleLevel.color;
        }

        public override BuildingLevel[] GetLevels()
        {
            return obstacleLevels;
        }
    }
}