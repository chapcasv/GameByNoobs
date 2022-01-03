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

        public static event Action<UnitTeam> OnTeamDefeat;

        public static void Reset() => isInit = false;

        public static void Init()
        {
            if (isInit) return;

            unitOfTeam = new Dictionary<UnitTeam, List<BaseUnit>>();

            unitOfTeam.Add(UnitTeam.Player, new List<BaseUnit>());
            unitOfTeam.Add(UnitTeam.Enemy, new List<BaseUnit>());

            isInit = true;
        }


        public static List<BaseUnit> GetAllUnits(UnitTeam team) => unitOfTeam[team];

        public static void AddUnit(UnitTeam team, BaseUnit unit) => unitOfTeam[team].Add(unit);

        public static void Clear(UnitTeam team) => unitOfTeam[team].Clear();


        public static void RemoveUnit(UnitTeam team, BaseUnit unit)
        {
            unitOfTeam[team].Remove(unit);

            if (unitOfTeam[team].Count == 0)
            {
                //Change team fight to next phase
                OnTeamDefeat?.Invoke(team);
            }
        }

        public static List<BaseUnit> GetUnitsAgainst(UnitTeam unitTeam)
        {
            if (unitTeam == UnitTeam.Player)
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

