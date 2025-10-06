using System.Collections.Generic;
using Opsive.Shared.Game;
using Sirenix.Utilities;
using UnityEngine;

namespace Zocat
{
    public class ParticleManager : MonoSingleton<ParticleManager>
    {
        public Trail Trail;
        public Dictionary<ParticleType, GameObject> ParticlesDic;

        /*-----------------------------PARTICLE---------------------------------------------------------*/
        protected override void Awake()
        {
            base.Awake();
            ParticlesDic.ForEach(particle => particle.Value.SetActive(false));
            // Trails.ForEach(trail => trail.SetActive(false));
        }

        public void PlayParticle(ParticleType particleType, Vector3 position, float duration)
        {
            var particle = PoolTracker.InstantiateGo(ParticlesDic[particleType], position, Quaternion.identity);
            particle.SetActive(true);
            Scheduler.Schedule(duration, () => PoolTracker.Destroy(particle));
        }

        /*------------------------------TRAIL--------------------------------------------------------*/
    }
}