using PH.GraphSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    /// <summary>
    /// Cache Unit Survival Stat, Skill Point Stat, Roration and Position in before teamfight phase
    /// Reuse data in phase draw card
    /// </summary>
    public static class PlayerCacheUnitData
    {   
        private static Dictionary<BaseUnit, UnitCache> dictionaryNodeCache;
        private static List<BaseUnit> playerUnits;
        private static bool isInit = false;
        public static void Reset() => isInit = false;
        public static void Init()
        {
            if (isInit) return;
            playerUnits = new List<BaseUnit>();
            dictionaryNodeCache = new Dictionary<BaseUnit, UnitCache>();
            isInit = true;
        }

        public static void Add(BaseUnit unit) => playerUnits.Add(unit);

        public static void Remove(BaseUnit unit) => playerUnits.Remove(unit);

        public static List<BaseUnit> GetAllUnit() => playerUnits;

        #region Cache

        public static void CacheUnitData(BaseUnit unit)
        {
            if (dictionaryNodeCache.ContainsKey(unit))
            {
                UnitCache oldCache = dictionaryNodeCache[unit];
                Cache(oldCache, unit);
            }
            else
            {
                UnitCache unitCache = new UnitCache();
                Cache(unitCache, unit);
                dictionaryNodeCache.Add(unit, unitCache);
            }
        }

        public static void RemoveCacheUnitData(BaseUnit unit)
        {
            if (dictionaryNodeCache.ContainsKey(unit))
            {
                dictionaryNodeCache.Remove(unit);
            }
        }

        private static void Cache(UnitCache cache, BaseUnit baseUnit)
        {
            CacheSurvivalStat(cache, baseUnit);
            CacheNode(cache, baseUnit);
            CacheRoration(cache, baseUnit);
        }

        private static void CacheSurvivalStat(UnitCache cache, BaseUnit baseUnit)
        {
            var USS = baseUnit.GetUnitSurvivalStat();

            cache.MaxHP = USS.MaxHP;
            cache.CurrentHP = USS.CurrentHP;
            cache.MagicResist = USS.MagicResist;
            cache.Armor = USS.Armor;
            cache.IsLive = true;
        }

        private static void CacheNode(UnitCache cache, BaseUnit baseUnit) => cache.NodePos = baseUnit.CurrentNode.Index;
        private static void CacheRoration(UnitCache cache, BaseUnit baseUnit) => cache.roration = baseUnit.transform.rotation;

        #endregion

        #region Reuse
        public static void ReuseUnit(BaseUnit unit)
        {
            if (dictionaryNodeCache.ContainsKey(unit))
            {
                var cacheUnit = dictionaryNodeCache[unit];
                unit.gameObject.SetActive(true);

                ReuseSurvialStat(cacheUnit, unit);
                ReuseRoration(cacheUnit, unit);
                ReuseNode(cacheUnit, unit);
                DictionaryTeamBattle.AddUnit(unit.GetTeam(), unit);

            }
            else throw new Exception("Dont have key: unit in dictionary");
        }

        private static void ReuseNode(UnitCache cache, BaseUnit baseUnit)
        {
            Node cacheNode = GridBoard.IntPositiontoNode(cache.NodePos);
            baseUnit.CurrentNode.SetOccupied(false);
            baseUnit.CurrentNode = cacheNode;
            baseUnit.transform.position = baseUnit.CurrentNode.WorldPosition;
            baseUnit.CurrentNode.SetOccupied(true);
        }

        private static void ReuseSurvialStat(UnitCache cache, BaseUnit baseUnit)
        {
            var USS = baseUnit.GetUnitSurvivalStat();

            USS.MaxHP = cache.MaxHP;
            USS.CurrentHP = cache.CurrentHP;
            //USS.MagicResist = cache.MagicResist;
            //USS.Armor = cache.Armor;
            USS.IsLive = true;
        }

        private static void ReuseRoration(UnitCache cache, BaseUnit baseUnit)
        {
            baseUnit.transform.rotation = cache.roration;
        }


        #endregion
    }
}

