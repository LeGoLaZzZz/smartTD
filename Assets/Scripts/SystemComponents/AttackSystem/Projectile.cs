using System;
using System.Collections;
using System.Collections.Generic;
using beizerTest;
using DefaultNamespace;
using Units;
using UnityEngine;

public class Projectile : BeizerSpawnedObject
{
    public float damage;


    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Unit>();
        if (unit != null)
        {
            unit.DefenseSystem.GetDamage(damage);
            Destroy(gameObject);
        }
    }
}