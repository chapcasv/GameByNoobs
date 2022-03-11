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
        [SerializeField] GameVFX pfHit;
        [SerializeField] StatusVFX pfStun;
        [SerializeField] GameVFX pfUndeadGreen;
        [SerializeField] GameVFX pfTamaShockWave;

        private GameObject tileUnder;
        private Dictionary<string, Queue<GameVFX>> vfxPool;
        private Dictionary<string, Queue<StatusVFX>> vfxStatusPool;

        private Queue<GameVFX> vfxDropUnit;
        private Queue<GameVFX> vfxSpawn;
        private Queue<GameVFX> vfxHeal;
        private Queue<GameVFX> vfxUpStat;
        private Queue<GameVFX> vfxReuse;
        private Queue<GameVFX> vfxRecall;
        private Queue<GameVFX> vfxHit;
        private Queue<StatusVFX> vfxStun;
        private Queue<GameVFX> vfxUndeadGreen;
        private Queue<GameVFX> vfxTamaShockWave;

        protected override void Awake()
        {
            base.Awake();

            vfxPool = new Dictionary<string, Queue<GameVFX>>();
            vfxStatusPool = new Dictionary<string, Queue<StatusVFX>>();

            InitVFX();
            InitStutusVFX();

            tileUnder = Instantiate(pfTileUnder, transform);
            tileUnder.SetActive(false);
        }

        #region Init VFX
        private void InitVFX()
        {
            InitDrop();
            InitSpawn();
            InitHeal();
            InitUpStat();
            InitReuse();
            InitRecall();
            InitHit();
            InitUndeadGreen();
            InitTamaShockWave();
        }

        private void InitTamaShockWave()
        {
            vfxTamaShockWave = new Queue<GameVFX>();

            string key = KeysVFX.TamaShockWave.ToString();
            vfxPool.Add(key, vfxTamaShockWave);
            AddToPool(2, pfTamaShockWave, key);
        }

        private void InitUndeadGreen()
        {
            vfxUndeadGreen = new Queue<GameVFX>();

            string key = KeysVFX.UndeadGreen.ToString();
            vfxPool.Add(key, vfxUndeadGreen);
            AddToPool(2, pfUndeadGreen, key);
        }

        private void InitHit()
        {
            vfxHit = new Queue<GameVFX>();

            string key = KeysVFX.Hit.ToString();
            vfxPool.Add(key, vfxHit);
            AddToPool(8, pfHit, key);
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
        #endregion

        #region Init StatusVFX

        private void InitStutusVFX()
        {
            InitStun();
        }

        private void InitStun()
        {
            vfxStun = new Queue<StatusVFX>();

            string key = KeysVFX.Stun.ToString();
            vfxStatusPool.Add(key, vfxStun);
            AddToPool(4, pfStun, key);
        }

        #endregion

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
            else
            {
                Debug.Log(" Need more " + key);
            }
        }

        #region GetVFX

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

        #endregion

        #region PlayStatusVFX

        public void PlayStatusVFX(Vector3 pos, string key, float duringEffect)
        {
            var vfxQueue = vfxStatusPool[key];

            if(vfxQueue.Count > 0)
            {
                var statusVFX = vfxQueue.Dequeue();
                statusVFX.Play(duringEffect,pos);
            }
        }

        #endregion
        private void AddToPool(int count, StatusVFX pf, string key)
        {
            for (int i = 0; i < count; i++)
            {
                StatusVFX vfxInstantiate = Instantiate(pf, transform);
                vfxInstantiate.Key_VFX = key;
                vfxInstantiate.gameObject.SetActive(false);

                vfxStatusPool[key].Enqueue(vfxInstantiate);
            }
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

        public void ReturnPool(StatusVFX statusVFX, string key)
        {
            statusVFX.gameObject.SetActive(false);
            vfxStatusPool[key].Enqueue(statusVFX);
        }
    }
}

