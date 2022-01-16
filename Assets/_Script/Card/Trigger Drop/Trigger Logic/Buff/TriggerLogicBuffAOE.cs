using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Logic Buff AoE", menuName = "ScriptableObject/Card/Trigger Drop/Logic/Buff/Buff AoE")]
    public class TriggerLogicBuffAOE : CardDropTriggerLogic
    {
        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, CardDropTriggerInput input, UnitTeam team = UnitTeam.Player)
        {

            TriggerInputBuffAOE inputBuffAOE = (TriggerInputBuffAOE)input;

            Buff[] buffs = inputBuffAOE.Buffs;

            Faction[] factionCard = card.GetFaction();

            FactionMode factionTakeBuff = inputBuffAOE.FactionTakeBuff;

            switch (factionTakeBuff)
            {
                case FactionMode.ALL:
                    BuffAll(buffs, team);
                    break;
                case FactionMode.OTHER:
                    BuffOther(buffs, team, factionCard);
                    break;
                case FactionMode.SAME:
                    BuffSame(buffs, team, factionCard);
                    break;
                default:
                    throw new System.Exception("Buff Need Faction Mode");
            }
            return true;
        }


        private void BuffAll(Buff[] buffs, UnitTeam team)
        {
            var listUnitTakeBuff = DictionaryTeamBattle.GetAllUnits(team);

            Excute(buffs, listUnitTakeBuff);
        }

        private void BuffSame(Buff[] buffs, UnitTeam team, Faction[] factions)
        {
            var listUnitTakeBuff = DictionaryTeamBattle.GetAllUnitsByFaction(team, factions);

            Excute(buffs, listUnitTakeBuff);
        }

        private void BuffOther(Buff[] buffs, UnitTeam team, Faction[] factions)
        {
            var listUnitTakeBuff = DictionaryTeamBattle.GetAllUnitsByFaction(team, factions,false);

            Excute(buffs, listUnitTakeBuff);
        }

        private void Excute(Buff[] buffs, List<BaseUnit> listUnitTakeBuff)
        {
            foreach (var unit in listUnitTakeBuff)
            {
                for (int i = 0; i < buffs.Length; i++)
                {
                    buffs[i].Excute(unit);
                }
            }
        }
    }
}

