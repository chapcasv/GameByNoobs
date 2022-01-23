using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "BMS", menuName = "ScriptableObject/Card/Trigger Input/Buff/Bonus Mana Stat")]
    public class BuffBonusManaStat : Buff
    {   
        [Tooltip("unit max mana will decread value by maxMana")]
        [SerializeField] int maxMana;

        [SerializeField] int startManaBonus;

        [SerializeField] int manaRegenOnHit;

        [SerializeField] int manaRegenOnTakeDame;

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
            var mana = unit.GetManaSystem;

            mana.UpOneRoundManaRegenOnHit(manaRegenOnHit);
            mana.UpOneRoundManaRegenOnTakeDame(manaRegenOnTakeDame);
            mana.UpOneRoundMaxMana(maxMana);
            mana.UpOneRoundManaStart(startManaBonus);
        }

        private void Endless(BaseUnit unit)
        {
            var mana = unit.GetManaSystem;

            mana.UpBaseManaRegenOnHit(manaRegenOnHit);
            mana.UpBaseManaRegenOnTakeDmg(manaRegenOnTakeDame);
            mana.UpBaseMaxMana(maxMana);
            mana.UpBaseManaStart(startManaBonus);
        }
    }
}

