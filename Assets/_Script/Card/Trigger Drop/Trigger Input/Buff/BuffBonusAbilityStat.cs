using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "BAS", menuName = "ScriptableObject/Card/Trigger Drop/Input/Buff/Bonus Ability Stat")]
    public class BuffBonusAbilityStat : Buff
    {
        [SerializeField] int startManaBonus;

        [SerializeField] int manaRegenOnHit;

        [SerializeField] int manaRegenOnTakeDame;

        [SerializeField] int abilityPct;

        public override void Excute(BaseUnit unit)
        {
            if (buffOneRound)
            {
                OneRound(unit);
            }
            else
            {
                Endless(unit);
            }
        }

        private void OneRound(BaseUnit unit)
        {

        }

        private void Endless(BaseUnit unit)
        {
            var mana = unit.GetManaSystem;

            mana.UpManaRegenOnHit(manaRegenOnHit);
            mana.UpManaRegenOnTakeDmg(manaRegenOnTakeDame);
            mana.UpManaStart(startManaBonus);

            var atkSystem = unit.GetAtkSystem;

            atkSystem.UpAbilityPower(abilityPct);
        }
    }
}

