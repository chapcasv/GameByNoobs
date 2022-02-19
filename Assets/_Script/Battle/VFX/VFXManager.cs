using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    public class VFXManager : MonoSingleton<VFXManager>
    {
        [SerializeField] GameObject localPlayerZone;
        [SerializeField] GameObject pfTileUnder;
        [SerializeField] GameVFX pfDropUnit;

        private Queue<GameVFX> vfxDropUnit = new Queue<GameVFX>();
        private GameObject tileUnder;

        protected override void Awake()
        {
            base.Awake();
            AddToPool(2);

            tileUnder = Instantiate(pfTileUnder, transform);
            tileUnder.SetActive(false);
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
            if(vfxDropUnit.Count > 0)
            {
                var dropUnit = vfxDropUnit.Dequeue();
                dropUnit.transform.position = pos;
                dropUnit.gameObject.SetActive(true);
            }
        }

        public void HighLightTileUnder(Vector3 pos)
        {
            tileUnder.transform.position = new Vector3(pos.x, pos.y + 0.1f, pos.z);
            tileUnder.SetActive(true);
        }

        public void HidenTileUnder()
        {
            tileUnder.SetActive(false);
        }



       


        private void AddToPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameVFX DropUnit = Instantiate(pfDropUnit, transform);
                DropUnit.gameObject.SetActive(false);
                vfxDropUnit.Enqueue(DropUnit);
            }
        }

        public void ReturnPool(GameVFX gameVFX)
        {
            gameVFX.gameObject.SetActive(false);
            vfxDropUnit.Enqueue(gameVFX);
        }
    }
}

