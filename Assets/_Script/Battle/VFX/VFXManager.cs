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
        [SerializeField] SpawnVFX pfSpawnVFX;


        private GameObject tileUnder;

        private Dictionary<string, Queue<GameVFX>> vfxPool = new Dictionary<string, Queue<GameVFX>>();

        private readonly string key_drop = "Drop Unit";
        private Queue<GameVFX> vfxDropUnit = new Queue<GameVFX>();

        private readonly string key_spawn = "Spawn";
        private Queue<GameVFX> vfxSpawn = new Queue<GameVFX>();


        protected override void Awake()
        {
            base.Awake();

            vfxPool.Add(key_drop, vfxDropUnit);
            AddToPool(2, pfDropUnit, key_drop);

            vfxPool.Add(key_spawn, vfxSpawn);
            AddToPool(4, pfSpawnVFX, key_spawn);

            tileUnder = Instantiate(pfTileUnder, transform);
            tileUnder.SetActive(false);
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
            if (vfxDropUnit.Count > 0)
            {
                var dropUnit = vfxPool[key_drop].Dequeue();
                dropUnit.transform.position = pos;
                dropUnit.gameObject.SetActive(true);
            }
        }

        public void SpawnUnit(Vector3 pos, SpawnSystem ss, CardUnit unit, Node node, UnitTeam team)
        {
            SpawnVFX spawn = (SpawnVFX)GetSpawnVFX();
            spawn.SetUp(ss, unit,node, team);
            spawn.transform.position = pos;
            spawn.gameObject.SetActive(true);
        }

        protected GameVFX GetSpawnVFX()
        {
            if (vfxSpawn.Count == 0)
            {
                AddToPool(1, pfSpawnVFX, key_spawn);
            }

            return vfxPool[key_spawn].Dequeue();
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



       


        private void AddToPool(int count, GameVFX pf, string key)
        {
            for (int i = 0; i < count; i++)
            {
                GameVFX vfxInstantiate = Instantiate(pf, transform);
                vfxInstantiate.Key_VFX = key;
                vfxInstantiate.gameObject.SetActive(false);

                vfxPool[key].Enqueue(vfxInstantiate);
                vfxDropUnit.Enqueue(vfxInstantiate);
            }
        }

        public void ReturnPool(GameVFX gameVFX, string key)
        {
            gameVFX.gameObject.SetActive(false);
            vfxPool[key].Enqueue(gameVFX);
        }
    }
}

