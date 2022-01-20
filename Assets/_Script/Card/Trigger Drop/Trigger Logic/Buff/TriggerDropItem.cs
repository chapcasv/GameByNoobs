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

            BaseUnit unitTakeItem = DictionaryTeamBattle.GetUnitByNode(team, dropNode);

            bool canDrop = unitTakeItem.Equip(item);

            if (canDrop)
            {
                Excute(inputDropItem, unitTakeItem);
            }
            return canDrop;
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

