using System;
using DefaultNamespace;
using UnityEngine;

namespace Particles
{
    [Serializable]
    public class DamageParticleContainer : ParticleContainer
    {
        public override void SubscribeToEvent(GameObject gameObject,
            Func<ParticleType, ParticleSystem> spawnParticleAction)
        {
            ParticleEvent += spawnParticleAction;
            gameObject.GetComponent<DefenseSystem>().DamagedEvent += OnObjectDamaged;
        }

        public override void UnSubscribeToEvent(GameObject gameObject,
            Func<ParticleType, ParticleSystem> spawnParticleAction)
        {
            ParticleEvent -= spawnParticleAction;
            gameObject.GetComponent<DefenseSystem>().DamagedEvent -= OnObjectDamaged;
        }

        private void OnObjectDamaged()
        {
            OnParticleEvent(type);
        }

        public DamageParticleContainer(ParticleSystem particleSystemPrefab, ParticleType type) : base(
            particleSystemPrefab, type)
        {
        }
    }
}