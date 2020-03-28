using UnityEngine;

namespace Particles
{
    [CreateAssetMenu(menuName = "MyObjects/ParticlesInfo/DestroyParticlesInfo", fileName = "destroyParticlesInfo")]
    public class DestroyParticlesContainerInfo : ParticleContainerInfo
    {
        public override ParticleContainer ConstructParticle()
        {
            var particle = new DestroyParticleContainer(particleSystemPrefab, type);
            return particle;
        }
    }
}