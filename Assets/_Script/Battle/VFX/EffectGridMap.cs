using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    public class EffectGridMap : MonoBehaviour
    {
        [SerializeField] GameObject localPlayerZone;
        [SerializeField] ParticleSystem highLight_TileUnder;

        private List<ParticleSystem> highLightMap;
        private ParticleSystem prefeb_highLight_TileUnder;

        private void Awake()
        {
            Setting.effectGridMap = this;
        
        }

        void Start()
        {
           
        }

        public void HighLighMap()
        {
            localPlayerZone.SetActive(true);
        }

        public void StopHighLighMap()
        {
            localPlayerZone.SetActive(false);
        }

        public void HighLight_TileUnder(Vector3 pos)
        {
          
        }

        public void Stop_HighLight_TileUnder()
        {
         
        }
    }
}

