using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "BSS_GiongWP", 
        menuName = "ScriptableObject/Card/Trigger Input/Buff/Giong WP Bonus Survival Stat")]
    public class BuffSurvivalStatGiongWP : Buff
    {   
        [Header("Giong")]
        [SerializeField] int hpBonusG;
        [SerializeField] int armorBonusG;
        [SerializeField] int magicResistBonusG;

        [Header("Other Unit")]
        [SerializeField] int hpBonus;
        [SerializeField] int armorBonus;
        [SerializeField] int magicResistBonus;

        //CardID
        private int giongID = 8;

        public override void Excute(BaseUnit unit)
        {
            var id = unit.GetID;

            if (id == giongID)
            {
                GiongOneRound(unit);
            }
            else OneRound(unit);
        }

        private void OneRound(BaseUnit unit)
        {
            var USS = unit.GetUnitSurvivalStat;

            USS.UpOneRoundStatArmor(armorBonus);
            USS.UpOneRoundStatMaxHP(hpBonus);
            USS.UpOneRoundStatMR(magicResistBonus);
        }

        private void GiongOneRound(BaseUnit giong)
        {
            var USS = giong.GetUnitSurvivalStat;

            USS.UpOneRoundStatArmor(armorBonusG);
            USS.UpOneRoundStatMaxHP(hpBonusG);
            USS.UpOneRoundStatMR(magicResistBonusG);
        }
    }
}

