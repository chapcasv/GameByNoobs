using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "BMS_GiongWP",
        menuName = "ScriptableObject/Card/Trigger Input/Buff/GiongWP Bonus Mana Stat")]
    public class BuffBonusManaGiongWP : Buff
    {
        [Header("Giong")]
        [Tooltip("unit max mana will decread value by maxMana")]
        [SerializeField] int maxManaG;

        [SerializeField] int startManaBonusG;

        [SerializeField] int manaRegenOnHitG;

        [SerializeField] int manaRegenOnTakeDameG;

        [Header("Other Unit")]
        [Tooltip("unit max mana will decread value by maxMana")]
        [SerializeField] int maxMana;

        [SerializeField] int startManaBonus;

        [SerializeField] int manaRegenOnHit;

        [SerializeField] int manaRegenOnTakeDame;


        //CardID
        private int giongID = 8;

        public override void Execute(BaseUnit unit)
        {
            var id = unit.GetID;

            if (id == giongID)
            {
                GiongOneRound(unit);
            }
            else OneRound(unit);
        }

        private void GiongOneRound(BaseUnit unit)
        {

            var mana = unit.GetManaSystem;

            mana.UpOneRoundManaRegenOnHit(manaRegenOnHitG);
            mana.UpOneRoundManaRegenOnTakeDame(manaRegenOnTakeDameG);
            mana.UpOneRoundMaxMana(maxManaG);
            mana.UpOneRoundManaStart(startManaBonusG);
        }

        private void OneRound(BaseUnit unit)
        {
            var mana = unit.GetManaSystem;

            mana.UpOneRoundManaRegenOnHit(manaRegenOnHit);
            mana.UpOneRoundManaRegenOnTakeDame(manaRegenOnTakeDame);
            mana.UpOneRoundMaxMana(maxMana);
            mana.UpOneRoundManaStart(startManaBonus);
        }
    }
}


