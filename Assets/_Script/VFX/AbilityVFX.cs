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

            particles = new List<ParticleSystem>();

            for (int i = 0; i < transform.childCount; i++)
            {
                ParticleSystem particle = transform.GetChild(i).GetComponent<ParticleSystem>();
                if (particle != null)
                {
                    particles.Add(particle);
                }
            }

            gameObject.SetActive(false);
        }

        protected virtual void Update()
        {
            bool isPlaying = IsPlaying();

            if (!isPlaying)
            {
                ReturnPool();
            }
        }

        public void Play()
        {
            gameObject.SetActive(true);
            Debug.Log("Play");
        }

        private bool IsPlaying()
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

        protected void ReturnPool()
        {
            gameObject.SetActive(false);
            _holder.AbilityVFXReturn(this);
        }
    }
}

