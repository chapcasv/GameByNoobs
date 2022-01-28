using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop Logic/Item")]
    public class TriggerDropItem : CardDropTriggerLogic
    {
        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, TriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            var item = (CardItem)card;

            TriggerInputSingle inputDropItem = (TriggerInputSingle)input;

            BaseUnit unitTakeItem = DictionaryTeamBattle.GetUnitByNode(team, dropNode);

            bool canDrop = unitTakeItem.Equip(item);

            if (canDrop)
            {
                Excute(inputDropItem, unitTakeItem);
                CardDropHistory.AddCardItemDrop(item,team);
            }
            return canDrop;
        }

        private void Excute(TriggerInputSingle input, BaseUnit unit)
        {
            Buff[] buffs = input.GetBuffs;

            for (int i = 0; i < buffs.Length; i++)
            {
                buffs[i].Execute(unit);
            }
        }
    }
}

