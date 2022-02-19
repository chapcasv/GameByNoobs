using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class GameVFX : MonoBehaviour
    {
        private List<ParticleSystem> particles;

        void Awake()
        {
            particles = new List<ParticleSystem>();

            for (int i = 0; i < transform.childCount; i++)
            {
                ParticleSystem particle = transform.GetChild(i).GetComponent<ParticleSystem>();
                if(particle != null)
                {
                    particles.Add(particle);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            bool isPlaying = ParticleIsPlay();

            if (!isPlaying)
            {
                VFXManager.Instance.ReturnPool(this);
            }
        }

        private bool ParticleIsPlay()
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

