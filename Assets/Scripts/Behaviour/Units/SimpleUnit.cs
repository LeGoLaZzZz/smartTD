using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using DefaultNamespace;
using Spawners;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(MapMoveSystem))]
    [RequireComponent(typeof(StandartUnitAttackSystem))]
    [RequireComponent(typeof(StandartDefenseSystem))]
    [RequireComponent(typeof(Collider))]
    public class SimpleUnit : Unit
    {
 
    }
}