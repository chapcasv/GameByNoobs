using UnityEngine;


namespace PH
{
    public class UnitCache
    {
        public int NodePos;
        public Quaternion roration;

        public UnitCacheSurvivalStat cacheUSS;
        public UnitCacheAtkStat cacheUAS;
        public UnitManaStat cacheMNS;

        public UnitCache()
        {
            cacheUSS = new UnitCacheSurvivalStat();
            cacheUAS = new UnitCacheAtkStat();
            cacheMNS = new UnitManaStat();
        }

        public bool IsLive;
    }

    public class UnitCacheSurvivalStat
    {
        public int MaxHP;
        public int CurrentHP;
        public int MagicResist;
        public int Armor;
    }

    public class UnitCacheAtkStat
    {
        public float AtkSpd;
        public int Dmg;
        public int CritRate;
        public int CritDmg;
        public int LifeSteal;
        public float Range;
        public int AbilityPower;
    }

    public class UnitManaStat
    {
        public int MaxMana;
        public int RegenOnTakeDmg;
        public int RegenOnHit;
        public int StartMana;
    }

   
}

