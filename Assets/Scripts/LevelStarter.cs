using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(WaveSpawner))]
    public class LevelStarter : MonoBehaviour
    {
        private WaveSpawner _waveSpawner;

        private void Awake()
        {
            _waveSpawner = GetComponent<WaveSpawner>();
        }


        private void Start()
        {
            StartLevel();
        }

        public void StartLevel()
        {
            _waveSpawner.StartSpawn();
        }
    }
}