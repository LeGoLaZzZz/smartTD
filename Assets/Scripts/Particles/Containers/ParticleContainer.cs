using System;
using DefaultNamespace;
using UnityEngine;

namespace Particles
{
    public abstract class ParticleContainer
    {
        public abstract void SubscribeToEvent(GameObject gameObject,
            Func<ParticleType, ParticleSystem> spawnParticleAction);

        public abstract void UnSubscribeToEvent(GameObject gameObject,
            Func<ParticleType, ParticleSystem> spawnParticleAction);

        public event Func<ParticleType, ParticleSystem> ParticleEvent;
        public ParticleSystem ParticleSystemPrefab;
        public ParticleType type;


        protected ParticleContainer(ParticleSystem particleSystemPrefab, ParticleType type)
        {
            this.ParticleSystemPrefab = particleSystemPrefab;
            this.type = type;
        }

        protected virtual ParticleSystem OnParticleEvent(ParticleType arg)
        {
            return ParticleEvent?.Invoke(arg);
        }
    }
}