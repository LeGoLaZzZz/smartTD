using UnityEngine;

namespace Particles
{
    [CreateAssetMenu(menuName = "MyObjects/ParticlesInfo/DamageParticlesInfo", fileName = "damageParticlesInfo")]
    public class DamageParticlesContainerInfo : ParticleContainerInfo
    {
        
        public override ParticleContainer ConstructParticle()
        {
            var particle = new DamageParticleContainer(particleSystemPrefab, type);
            return particle;
        }
        
    }
}