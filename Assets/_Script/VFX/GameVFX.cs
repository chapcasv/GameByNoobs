using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class GameVFX : MonoBehaviour
    {
        protected List<ParticleSystem> particles;
        private string key_VFX;

        public string Key_VFX {set => key_VFX = value; }

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

        protected virtual void Update()
        {
            bool isPlaying = ParticleIsPlay();

            if (!isPlaying)
            {
                VFXManager.Instance.ReturnPool(this, key_VFX);
            }
        }

        protected bool ParticleIsPlay()
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

