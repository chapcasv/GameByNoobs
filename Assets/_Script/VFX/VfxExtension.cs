using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class VfxExtention
    {   
        public static List<ParticleSystem> GetParticlesChild(Transform transform)
        {
            List<ParticleSystem> particles = new List<ParticleSystem>();

            for (int i = 0; i < transform.childCount; i++)
            {
                ParticleSystem particle = transform.GetChild(i).GetComponent<ParticleSystem>();
                if (particle != null)
                {
                    particles.Add(particle);
                }
            }
            return particles;
        }

        public static bool ParticleIsPlay(List<ParticleSystem> particles)
        {
            bool isPlaying = true;

            foreach (var particle in particles)
            {
                isPlaying = particle.isPlaying;

                if (isPlaying)
                {
                    break;
                }
            }
            return isPlaying;
        }
    }

}
