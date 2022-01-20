using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop/Logic/Item")]
    public class TriggerDropItem : CardDropTriggerLogic
    {
        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, CardDropTriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            var item = (CardItem)card;

            TriggerInputDropItem inputDropItem = (TriggerInputDropItem)input;

            var allTeamMate = DictionaryTeamBattle.GetAllUnits(team);

            foreach (var unit in allTeamMate)
            {
                if (unit.CurrentNode == dropNode)
                {
                    bool canDrop = unit.Equip(item);

                    if (canDrop)
                    {
                        Excute(inputDropItem, unit);
                    }

                    return canDrop;
                }
            }
            return false;
        }

        private void Excute(TriggerInputDropItem input, BaseUnit unit)
        {
            Buff[] buffs = input.GetBuffs;

            for (int i = 0; i < buffs.Length; i++)
            {
                buffs[i].Excute(unit);
            }
        }
    }
}

