using System;
using DefaultNamespace;
using UnityEngine;

namespace Particles
{
    [Serializable]
    public class DestroyParticleContainer : ParticleContainer
    {
        public override void SubscribeToEvent(GameObject gameObject,
            Func<ParticleType, ParticleSystem> spawnParticleAction)
        {
            ParticleEvent += spawnParticleAction;
            gameObject.GetComponent<DefenseSystem>().ObjectKilled += OnObjectDestroyed;
        }

        public override void UnSubscribeToEvent(GameObject gameObject,
            Func<ParticleType, ParticleSystem> spawnParticleAction)
        {
            ParticleEvent -= spawnParticleAction;
            gameObject.GetComponent<DefenseSystem>().ObjectKilled -= OnObjectDestroyed;
        }

        private void OnObjectDestroyed(IAttackable obj)
        {
            OnParticleEvent(type);
        }

        public DestroyParticleContainer(ParticleSystem particleSystemPrefab, ParticleType type) : base(particleSystemPrefab, type)
        {
        }
    }
}