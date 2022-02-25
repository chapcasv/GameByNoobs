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
            CacheManaStat(cache, baseUnit);
            CacheSurvivalStat(cache, baseUnit);
            CacheAtkStat(cache, baseUnit);
            CacheNode(cache, baseUnit);
            CacheRoration(cache, baseUnit);
        }

        private static void CacheManaStat(UnitCache cache, BaseUnit baseUnit)
        {
            var mana = baseUnit.GetManaSystem;

            var cacheMNS = cache.cacheMNS;

            cacheMNS.MaxMana = mana.BaseMaxMana;
            cacheMNS.RegenOnHit = mana.BaseManaRegenOnHit;
            cacheMNS.RegenOnTakeDmg = mana.BaseManaRegenOnTakeDmg;
            cacheMNS.StartMana = mana.BaseManaStart;

        }

        private static void CacheAtkStat(UnitCache cache, BaseUnit baseUnit)
        {
            var UAS = baseUnit.GetAtkSystem;

            var cacheUAS = cache.cacheUAS;

            cacheUAS.AtkSpd = UAS.BaseAttackSpeed;
            cacheUAS.CritRate = UAS.BaseCritRate;
            cacheUAS.CritDmg = UAS.BaseCritDmg;
            cacheUAS.Dmg = UAS.BasePhysicalDmg;
            cacheUAS.LifeSteal = UAS.BaseLifeSteal;
            cacheUAS.Range = UAS.BaseRangeAtk;
            cacheUAS.AbilityPower = UAS.BaseMagicPower;
        }

        private static void CacheSurvivalStat(UnitCache cache, BaseUnit baseUnit)
        {
            var USS = baseUnit.GetUnitSurvivalStat;

            var cacheUSS = cache.cacheUSS;

            cacheUSS.MaxHP = USS.BaseMaxHP;
            cacheUSS.CurrentHP = cacheUSS.MaxHP;
            cacheUSS.MagicResist = USS.BaseMagicResist;
            cacheUSS.Armor = USS.BaseArmor;
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

                ReuseRoration(cacheUnit, unit);
                ReuseNode(cacheUnit, unit);
                ReuseAtkStat(cacheUnit, unit);
                ReuseManaStat(cacheUnit, unit);
                ReuseSurvialStat(cacheUnit, unit);

                unit.InTeamFight = false;
                unit.RemoveCurrentTarget();
                unit.ResetMove();

                VFXManager.Instance.ReuseUnit(unit);

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

        private static void ReuseManaStat(UnitCache cache, BaseUnit baseUnit)
        {
            var mana = baseUnit.GetManaSystem;
            var cacheMNS = cache.cacheMNS;

            mana.ReuseMaxMana(cacheMNS.MaxMana);
            mana.ReuseManaStart(cacheMNS.StartMana);
            mana.ReuseManaRegenOnHit(cacheMNS.RegenOnHit);
            mana.ReuseManaRegenOnTakeDmg(cacheMNS.RegenOnTakeDmg);
        }

        private static void ReuseSurvialStat(UnitCache cache, BaseUnit baseUnit)
        {
            var USS = baseUnit.GetUnitSurvivalStat;
            var cacheUSS = cache.cacheUSS;

            USS.ReuseMaxHP(cacheUSS.MaxHP);
            USS.ReuseMagicResist(cacheUSS.MagicResist);
            USS.ReuseArmor(cacheUSS.Armor);
            USS.IsLive = true;
        }

        private static void ReuseAtkStat(UnitCache cache, BaseUnit baseUnit)
        {
            var UAS = baseUnit.GetAtkSystem;
            var cacheUAS = cache.cacheUAS;

            UAS.ReuseAtkSpeed(cacheUAS.AtkSpd);
            UAS.ReuseCritRate(cacheUAS.CritRate);
            UAS.ReuseCritDmg(cacheUAS.CritDmg);
            UAS.ReuseLifeSteal(cacheUAS.LifeSteal);
            UAS.ReusePhysicalDmg(cacheUAS.Dmg);
            UAS.ReuseRangeAtk(cacheUAS.Range);
            UAS.ReuseAbilityPower(cacheUAS.AbilityPower);

        }

        private static void ReuseRoration(UnitCache cache, BaseUnit baseUnit)
        {
            baseUnit.transform.rotation = cache.roration;
        }


        #endregion
    }
}

