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
        [SerializeField] Transform pfDropUnit;
        private ParticleSystem particleDropUnit;
        

        private void Awake()
        {
            Setting.effectGridMap = this;

            particleDropUnit = pfDropUnit.GetChild(0).GetComponent<ParticleSystem>();
        }

        void Start()
        {
           
        }

        public void HighLightMap()
        {
            localPlayerZone.SetActive(true);
        }

        public void StopHighLightMap()
        {
            localPlayerZone.SetActive(false);
        }

        public void DropUnit(Vector3 pos)
        {
            pfDropUnit.transform.position = pos;
            particleDropUnit.Play();
        }

        public void HighLight_TileUnder(Vector3 pos)
        {
          
        }

        public void Stop_HighLight_TileUnder()
        {
         
        }
    }
}

