using System;
using Units;
using UnityEngine;

namespace Infos
{
    //TODO Temp Code
    [CreateAssetMenu(menuName = "MyObjects/UnitParameters/SimpleUnit", fileName = "SimpleUnit1")]
    public class SimpleUnitInfo : UnitInfo
    {
        public UnitLevel[] unitLevels;


        public override void SetParametersTo(Unit objectScript, int level = 0)
        {
            if (!(objectScript is SimpleUnit unit))
                throw new ArgumentException("Expected SimpleUnit");

            if (level >= unitLevels.Length) level = unitLevels.Length - 1;
            objectScript.Level = level;

            var unitLevel = unitLevels[level];
            unit.GoldGain = unitLevel.goldGain;
            unit.CastleDamage = unitLevel.castleDamage;
            unit.DefenseSystem.SetMaxHealthPoints(unitLevel.health);
            unit.AttackSystem.SetUpParameters(unitLevel.damage, unitLevel.distance, unitLevel.cooldown);
        }
    }
}