using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using System;

namespace PH
{
    public class VFXManager : MonoSingleton<VFXManager>
    {
        [SerializeField] GameObject localPlayerZone;
        [SerializeField] GameObject pfTileUnder;
        [SerializeField] GameVFX pfDropUnit;
        [SerializeField] SpawnVFX pfSpawn;
        [SerializeField] GameVFX pfHeal;
        [SerializeField] GameVFX pfUpStat;
        [SerializeField] GameVFX pfReuse;
        [SerializeField] GameVFX pfRecall;


        private GameObject tileUnder;

        private Dictionary<string, Queue<GameVFX>> vfxPool;

        private Queue<GameVFX> vfxDropUnit;
        private Queue<GameVFX> vfxSpawn;
        private Queue<GameVFX> vfxHeal;
        private Queue<GameVFX> vfxUpStat;
        private Queue<GameVFX> vfxReuse;
        private Queue<GameVFX> vfxRecall;


        protected override void Awake()
        {
            base.Awake();

            vfxPool = new Dictionary<string, Queue<GameVFX>>();

            InitVFX();

            tileUnder = Instantiate(pfTileUnder, transform);
            tileUnder.SetActive(false);
        }

        private void InitVFX()
        {
            InitDrop();
            InitSpawn();
            InitHeal();
            InitUpStat();
            InitReuse();
            InitRecall();
        }

        private void InitRecall()
        {
            vfxRecall = new Queue<GameVFX>();

            string key = KeysVFX.Recall.ToString();
            vfxPool.Add(key, vfxRecall);
            AddToPool(4, pfRecall, key);
        }

        private void InitReuse()
        {
            vfxReuse = new Queue<GameVFX>();

            string key = KeysVFX.Reuse.ToString();
            vfxPool.Add(key, vfxReuse);
            AddToPool(4, pfReuse, key);
        }

        private void InitUpStat()
        {
            vfxUpStat = new Queue<GameVFX>();

            string key = KeysVFX.UpStat.ToString();
            vfxPool.Add(key, vfxUpStat);
            AddToPool(1, pfUpStat, key);
        }

        private void InitSpawn()
        {
            vfxSpawn = new Queue<GameVFX>();

            string key = KeysVFX.Spawn.ToString();
            vfxPool.Add(key, vfxSpawn);
            AddToPool(4, pfSpawn, key);
        }

        private void InitDrop()
        {
            vfxDropUnit = new Queue<GameVFX>();
            vfxPool.Add(KeysVFX.Drop.ToString(), vfxDropUnit);
            AddToPool(2, pfDropUnit, KeysVFX.Drop.ToString());
        }

        private void InitHeal()
        {
            vfxHeal = new Queue<GameVFX>();
            vfxPool.Add(KeysVFX.Heal.ToString(), vfxHeal);
            AddToPool(2, pfHeal, KeysVFX.Heal.ToString());
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
            PlayVFX(pos, KeysVFX.Drop.ToString());
        }

        public void PlayVFX(Vector3 pos, string key)
        {
            var vfxQueue = vfxPool[key];

            if(vfxQueue.Count > 0)
            {
                var gameVFX = vfxQueue.Dequeue();
                gameVFX.transform.position = pos;
                gameVFX.gameObject.SetActive(true);
            }
        }

        public void SpawnUnit(Vector3 pos, BaseUnit unit)
        {
            SpawnVFX spawn = (SpawnVFX)GetSpawnVFX();
            spawn.SetUp(unit);
            spawn.transform.position = pos;
            spawn.gameObject.SetActive(true);
        }

        public void ReuseUnit(BaseUnit unit)
        {
            ReuseVFX reuseVFX = (ReuseVFX)GetReuseVFX();
            reuseVFX.SetUp(unit);
        }

        public void RecallUnit(BaseUnit unit, RecallTrigger recall)
        {
            RecallVFX recallVFX = (RecallVFX)GetRecallVFX();
            recallVFX.SetUp(unit,recall);
        }

        protected GameVFX GetSpawnVFX()
        {
            if (vfxSpawn.Count == 0)
            {
                AddToPool(1, pfSpawn, KeysVFX.Spawn.ToString());
            }

            return vfxPool[KeysVFX.Spawn.ToString()].Dequeue();
        }

        protected GameVFX GetReuseVFX()
        {
            if (vfxReuse.Count == 0)
            {
                AddToPool(1, pfReuse, KeysVFX.Reuse.ToString());
            }

            return vfxPool[KeysVFX.Reuse.ToString()].Dequeue();
        }

        protected GameVFX GetRecallVFX()
        {
            if (vfxRecall.Count == 0)
            {
                AddToPool(1, pfRecall, KeysVFX.Recall.ToString());
            }

            return vfxPool[KeysVFX.Recall.ToString()].Dequeue();
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
            }
        }

        public void ReturnPool(GameVFX gameVFX, string key)
        {
            gameVFX.gameObject.SetActive(false);
            vfxPool[key].Enqueue(gameVFX);
        }
    }
}

