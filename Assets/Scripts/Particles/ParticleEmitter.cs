using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Particles
{
    public class ParticleEmitter : MonoBehaviour
    {
        [SerializeField] private ParticleContainerInfo[] particlesContainerInfo;
        private List<ParticleContainer> _particles = new List<ParticleContainer>();


        public Dictionary<ParticleType, ParticleSystem>
            particleSystems = new Dictionary<ParticleType, ParticleSystem>();

        private void Awake()
        {
            foreach (var particleInfo in particlesContainerInfo)
            {
                particleSystems.Add(particleInfo.type, particleInfo.particleSystemPrefab);
                var constructParticle = particleInfo.ConstructParticle();
                _particles.Add(constructParticle);
                constructParticle.SubscribeToEvent(gameObject, SpawnParticle);
            }
        }

        private void OnDisable()
        {
            foreach (var particle in _particles)
            {
                particle.UnSubscribeToEvent(gameObject, SpawnParticle);
            }
        }

        public ParticleSystem SpawnParticle(ParticleType type)
        {
            var particleSystemPrefab = particleSystems[type];
            var spawnedParticleSystem = Instantiate(particleSystemPrefab);
            spawnedParticleSystem.transform.position = transform.position;


            return spawnedParticleSystem;
        }
    }
}