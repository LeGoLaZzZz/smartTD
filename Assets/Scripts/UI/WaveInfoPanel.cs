using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class WaveInfoPanel : MonoBehaviour
    {
        [SerializeField] private Text waveNumText;
        [SerializeField] private Text waveStatusText;
        [SerializeField] private Button startNextWaveButton;
        [SerializeField] private WaveSpawner waveSpawner;


        [SerializeField] private string waveNumString = "Wave num:";
        [SerializeField] private string waveStatusString = "Wave status:";


        private bool _canSpawnNextWave;

        public bool CanSpawnNextWave
        {
            get => _canSpawnNextWave;
            set
            {
                _canSpawnNextWave = value;
                startNextWaveButton.interactable = value;
                if (!(bool) waveSpawner?.IsThereNextWave())
                {
                    startNextWaveButton.interactable = false;
                    startNextWaveButton.GetComponentInChildren<Text>().text = "No more waves";
                    _canSpawnNextWave = false;
                }
            }
        }


        private void Start()
        {
            if (waveSpawner)
            {
                waveSpawner.WaveStarted += SetStartedWaveInfo;
                waveSpawner.WaveEndSpawn += SetStoppedSpawnWaveInfo;
                CanSpawnNextWave = true;
            }
        }


        private void OnDisable()
        {
            if (waveSpawner)
            {
                waveSpawner.WaveStarted -= SetStartedWaveInfo;
                waveSpawner.WaveEndSpawn -= SetStoppedSpawnWaveInfo;
            }
        }


        public void StartWaveButton()
        {
            if (CanSpawnNextWave)
            {
                waveSpawner?.TryStartNextWave();

                CanSpawnNextWave = false;
            }
        }


        private void SetStartedWaveInfo()
        {
            waveNumText.text = waveNumString + waveSpawner.CurWaveNum.ToString();
            waveStatusText.text = waveStatusString + " Spawning";

            CanSpawnNextWave = false;
        }

        private void SetStoppedSpawnWaveInfo()
        {
            waveNumText.text = waveNumString + waveSpawner.CurWaveNum.ToString();
            waveStatusText.text = waveStatusString + " Spawned";
            CanSpawnNextWave = true;
        }
    }
}