using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop Logic/Giong WP")]
    public class TriggerDropGiongWP : CardDropTriggerLogic
    {
        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, TriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            var item = (CardItem)card;

            TriggerInputSingle inputDropItem = (TriggerInputSingle)input;

            BaseUnit unitTakeItem = DictionaryTeamBattle.GetUnitByNode(team, dropNode);

            if (unitTakeItem is GiongUnit)
            {
                return EquipGiong(team, item, inputDropItem, unitTakeItem as GiongUnit);
            } else  return Equip(team, item, inputDropItem, unitTakeItem);
        }

        private bool Equip(UnitTeam team, CardItem item, TriggerInputSingle inputDropItem, BaseUnit unitTakeItem)
        {
            bool canDrop = unitTakeItem.Equip(item);

            if (canDrop)
            {
                Excute(inputDropItem, unitTakeItem);
                CardDropHistory.AddCardItemDrop(item, team);
            }
            return canDrop;
        }

        private bool EquipGiong(UnitTeam team, CardItem item, TriggerInputSingle inputDropItem, GiongUnit giong)
        {
            bool canDrop = giong.Equip(item);

            if (canDrop)
            {
                Excute(inputDropItem, giong);
                CardDropHistory.AddCardItemDrop(item, team);
                giong.IncreaseGiongWPcount();
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

