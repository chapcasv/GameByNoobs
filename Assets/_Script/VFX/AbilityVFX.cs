using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class AbilityVFX : MonoBehaviour
    {   
        private UnitAtkSystem _holder;
        private List<ParticleSystem> particles;

        public void Constructor(UnitAtkSystem holder)
        {
            _holder = holder;

            particles = VfxExtention.GetParticlesChild(transform);

            gameObject.SetActive(false);
        }

        protected virtual void Update()
        {
            bool isPlaying = VfxExtention.ParticleIsPlay(particles);

            if (!isPlaying)
            {
                gameObject.SetActive(false);
            }
        }

        public void Play()
        {
            gameObject.SetActive(true);
            Debug.Log("Play");
        }
    }
}

