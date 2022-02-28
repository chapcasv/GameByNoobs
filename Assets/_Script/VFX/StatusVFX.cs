using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class StatusVFX : MonoBehaviour
    {
        private List<ParticleSystem> particles;
        private string key_VFX;
        public string Key_VFX { set => key_VFX = value; }

        private void Awake()
        {
            particles = VfxExtention.GetParticlesChild(transform);
        }

        public void Play(float duringEffect, Vector3 pos)
        {
            foreach (var particle in particles)
            {
                var particleMain = particle.main;
                particleMain.startLifetime = duringEffect;
            }

            transform.position = pos;
            gameObject.SetActive(true);
        }

        protected void Update()
        {
            bool isPlaying = VfxExtention.ParticleIsPlay(particles);

            if (!isPlaying)
            {
                VFXManager.Instance.ReturnPool(this, key_VFX);
            }
        }
    }

}
