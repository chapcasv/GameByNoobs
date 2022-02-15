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
        [SerializeField] ParticleSystem dropUnit;
        

        private void Awake()
        {
            Setting.effectGridMap = this;
        
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

        public void EffectDropUnit(Node nodeDrop)
        {
            dropUnit.transform.position = nodeDrop.WorldPosition;
            
        }

        public void HighLight_TileUnder(Vector3 pos)
        {
          
        }

        public void Stop_HighLight_TileUnder()
        {
         
        }
    }
}

