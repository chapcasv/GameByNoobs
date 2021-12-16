using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;


namespace PH
{
    public static class DictionaryTeamBattle 
    {
        private static bool isInit = false;
        private static Dictionary<UnitTeam, List<BaseUnit>> unitOfTeam;
        private static Dictionary<BaseUnit, int> dictionaryNodeCache;

        public static event Action<UnitTeam> OnTeamDefeat;

        public static void Reset() => isInit = false;

        public static void Init()
        {
            if (isInit) return;

            unitOfTeam = new Dictionary<UnitTeam, List<BaseUnit>>();
            dictionaryNodeCache = new Dictionary<BaseUnit, int>();
            unitOfTeam.Add(UnitTeam.Player, new List<BaseUnit>());
            unitOfTeam.Add(UnitTeam.Enemy, new List<BaseUnit>());

            isInit = true;
        }

        public static void CacheNode(BaseUnit unit)
        {
            if (dictionaryNodeCache.ContainsKey(unit))
            {   
                //update Pos
                dictionaryNodeCache[unit] = unit.CurrentNode.Index;
            }
            else
            {   
                dictionaryNodeCache.Add(unit, unit.CurrentNode.Index);
            }
 
        }

        public static void RemoveCacheNode(BaseUnit unit)
        {
            if (dictionaryNodeCache.ContainsKey(unit))
            {
                dictionaryNodeCache.Remove(unit);
            }
        }

        public static int GetCachePos(BaseUnit unit)
        {
            if (dictionaryNodeCache.ContainsKey(unit))
            {
                return dictionaryNodeCache[unit];
            }
            throw new Exception("Dont have key: unit in dictionary");
        }




        public static List<BaseUnit> GetAllUnits(UnitTeam team) => unitOfTeam[team];

        public static void AddUnit(UnitTeam team, BaseUnit unit) => unitOfTeam[team].Add(unit);

        public static void RemoveUnit(UnitTeam team, BaseUnit unit)
        {
            unitOfTeam[team].Remove(unit);

            if(unitOfTeam[team].Count == 0)
            {   
                //Change phase
                OnTeamDefeat?.Invoke(team);
            }
        }

        public static List<BaseUnit> GetUnitsAgainst(UnitTeam unitTeam) 
        { 
            if(unitTeam == UnitTeam.Player)
            {
                return unitOfTeam[UnitTeam.Enemy];
            }
            else
            {
                return unitOfTeam[UnitTeam.Player];
            }
        }  

        public static List<BaseUnit> GetTeamMate(UnitTeam unitTeam) => unitOfTeam[unitTeam];
    }
}

