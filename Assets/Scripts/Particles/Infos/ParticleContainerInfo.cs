using DefaultNamespace;
using UnityEngine;

namespace Particles
{
    public abstract class ParticleContainerInfo : ScriptableObject
    {
        public abstract ParticleContainer ConstructParticle();


        public ParticleSystem particleSystemPrefab;
        public ParticleType type;
    }
}