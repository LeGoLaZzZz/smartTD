using System;
using System.Collections;
using Spawners;
using Units;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(UnitSpawner))]
    public class WaveSpawner : MonoBehaviour
    {
        public Wave[] waves;
        public int CurWaveNum { get; private set; }

        private UnitSpawner _spawner;

        public event Action WaveStarted;
        public event Action WaveEndSpawn;

        private void Awake()
        {
            _spawner = GetComponent<UnitSpawner>();
            CurWaveNum = -1;
        }


        public void StartSpawn()
        {
            CurWaveNum = -1;
            TryStartNextWave();
        }


        public bool IsThereNextWave()
        {
            return CurWaveNum != waves.Length - 1;
        }

        public bool TryStartNextWave()
        {
            if (CurWaveNum == waves.Length - 1)
            {
                Debug.Log("no more waves");
                return false;
            }

            StartCoroutine(StartWave(++CurWaveNum));
            OnWaveStarted();
            return true;
        }


//        private IEnumerator SpawnEnemy(UnitType type, float afterTime)
//        {
//            yield return new WaitForSeconds(afterTime);
//
//            _spawner.Spawn<Unit>(type);
//        }
//
//
//        private IEnumerator StartWave(int waveNum)
//        {
//            var timeSpawned = 0.0f;
//            foreach (var microWave in waves[waveNum].microWaves)
//            {
//                for (var i = 0; i < microWave.enemyCount; i++)
//                {
//                    yield return new WaitForSeconds(microWave.spawnInterval);
//                    StartCoroutine(SpawnEnemy(microWave.unitType, timeSpawned));
//                    timeSpawned += microWave.spawnInterval;
//                }
//
//                timeSpawned += microWave.waitAfterMicroWave;
//            }
//
//
//            yield break;
//        }


        private void SpawnEnemy(UnitType type)
        {
            _spawner.Spawn<Unit>(type);
        }


        private IEnumerator StartWave(int waveNum)
        {
            foreach (var microWave in waves[waveNum].microWaves)
            {
                for (var i = 0; i < microWave.enemyCount; i++)
                {
                    SpawnEnemy(microWave.unitType);
                    yield return new WaitForSeconds(microWave.spawnInterval);
                }
            }

            OnWaveEndSpawn();
        }

        protected virtual void OnWaveStarted()
        {
            WaveStarted?.Invoke();
        }

        protected virtual void OnWaveEndSpawn()
        {
            WaveEndSpawn?.Invoke();
        }
    }
}