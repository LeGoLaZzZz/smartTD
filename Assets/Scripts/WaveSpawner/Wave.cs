using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<MicroWave> microWaves;

    public float waitAfterWave;


    [System.Serializable]
    public class MicroWave
    {
        public UnitType unitType;
        public float spawnInterval = 2;
        public int enemyCount = 20;
        public float waitAfterMicroWave;
    }
}