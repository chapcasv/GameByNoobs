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
        private static Dictionary<Faction, List<BaseUnit>> unitOfFaction;


        public static event Action<UnitTeam> OnTeamDefeat;

        public static void Reset() => isInit = false;

        public static void Init(FactionContainer factionContainer)
        {
            if (isInit) return;

            InitTeam();
            InitFaction(factionContainer);


            isInit = true;
        }

        private static void InitTeam()
        {
            unitOfTeam = new Dictionary<UnitTeam, List<BaseUnit>>();

            unitOfTeam.Add(UnitTeam.Player, new List<BaseUnit>());
            unitOfTeam.Add(UnitTeam.Enemy, new List<BaseUnit>());
        }

        private static void InitFaction(FactionContainer factionContainer)
        {
            Faction[] factions = factionContainer.GetFactions;

            unitOfFaction = new Dictionary<Faction, List<BaseUnit>>();

            for (int i = 0; i < factions.Length; i++)
            {
                unitOfFaction.Add(factions[i], new List<BaseUnit>());
            }
        }

        public static List<BaseUnit> GetAllUnits(UnitTeam team) => unitOfTeam[team];

        public static BaseUnit GetUnitByNode(UnitTeam team , Node node)
        {
            BaseUnit unitResult;

            foreach (var unit in unitOfTeam[team])
            {
                if (unit.CurrentNode == node)
                    return unitResult = unit;
            }

            throw new Exception("Dont have unit stay in node");
        }

        public static List<BaseUnit> GetAllUnitsByFaction(UnitTeam team, Faction[] factions, bool useModeSame = true)
        {
            List<BaseUnit> newList = new List<BaseUnit>();

            var unitTeamFilter = unitOfTeam[team];

            var unitFactionFilter = FilterFactionByMode(factions, useModeSame);

            foreach (var unit in unitTeamFilter)
            {
                bool isContains = unitFactionFilter.Contains(unit);

                if (isContains)
                {
                    newList.Add(unit);
                }
            }

            return newList;
        }

        private static List<BaseUnit> FilterFactionByMode(Faction[] factions, bool useModeSame)
        {
            List<BaseUnit> unitFactionFilter = new List<BaseUnit>();

            if (useModeSame)
            {
                unitFactionFilter = FilterSameFaction(factions);
            }
            else
            {
                unitFactionFilter = FilterOtherFaction(factions);
            }

            return unitFactionFilter;
        }

        //Need fix
        private static List<BaseUnit> FilterOtherFaction(Faction[] factions)
        {
            List<BaseUnit> factionFilter = new List<BaseUnit>();
            


            return factionFilter;
        }

        private static List<BaseUnit> FilterSameFaction(Faction[] factions)
        {
            List<BaseUnit> factionFilter = new List<BaseUnit>();

            for (int i = 0; i < factions.Length; i++)
            {
                var listUnit = unitOfFaction[factions[i]];

                for (int j = 0; j < listUnit.Count; j++)
                {
                    BaseUnit unit = listUnit[j];
                    bool isContains = factionFilter.Contains(unit);

                    if (!isContains)
                    {
                        factionFilter.Add(unit);
                    }
                }
            }

            return factionFilter;
        }

        public static void AddUnit(UnitTeam team, BaseUnit unit)
        {
            AddByTeam(team, unit);
            AddByFaction(unit);
        }

        private static void AddByTeam(UnitTeam team, BaseUnit unit)
        {
            List<BaseUnit> units = unitOfTeam[team];
            bool isConstain = units.Contains(unit);

            if (!isConstain)
            {
                unitOfTeam[team].Add(unit);
            }
        }

        private static void AddByFaction(BaseUnit unit)
        {
            Faction[] factions = unit.GetFactions;

            for (int i = 0; i < factions.Length; i++)
            {
                unitOfFaction[factions[i]].Add(unit);
            }
        }

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

