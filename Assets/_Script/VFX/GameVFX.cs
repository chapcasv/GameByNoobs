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
            particles = VfxExtention.GetParticlesChild(transform);
        }

        protected virtual void Update()
        {
            bool isPlaying = VfxExtention.ParticleIsPlay(particles);

            if (!isPlaying)
            {
                VFXManager.Instance.ReturnPool(this, key_VFX);
            }
        }

       
    }
}

