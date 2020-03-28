using System;
using Buildings;
using DefaultNamespace;
using UnityEngine;

namespace Infos
{
    [CreateAssetMenu(menuName = "MyObjects/BuildingParameters/TowerBuildingInfo", fileName = "towerBuildingInfo")]
    public class TowerBuildingInfo : BuildingInfo
    {
        public float damage;
        public float distance;
        public float hp;
        public float cooldown;
        public GameObject projectile;

        public TowerLevel[] towerLevels;

        public override void SetParametersTo(Building objectScript, int level = 0)
        {
            //TODO дублирование кода хз чо делать
            if (level >= towerLevels.Length) level = towerLevels.Length - 1;
            objectScript.Level = level;

            var towerLevel = towerLevels[level];

            objectScript.sellCost = towerLevel.sellCost;
            objectScript.upgradeCost = towerLevel.upgradeCost;


            if (!(objectScript is ICanAttack tower))
                throw new ArgumentException("Expected TowerBuilding, object doesnt have ICanAttack");
            tower.AttackSystem.SetUpParameters(damage, distance, cooldown);
            ((StandartTowerAttackSystem) tower.AttackSystem).projectile = projectile;

            if (!(objectScript is IAttackable attackable))
                throw new ArgumentException("Expected TowerBuilding, object doesnt have IAttackable");

            attackable.DefenseSystem.SetMaxHealthPoints(hp);
        }

        public override BuildingLevel[] GetLevels()
        {
            return towerLevels;
        }
    }
}