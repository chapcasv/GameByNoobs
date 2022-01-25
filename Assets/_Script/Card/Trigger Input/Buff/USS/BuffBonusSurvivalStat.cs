using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(fileName ="BSS", menuName = "ScriptableObject/Card/Trigger Input/Buff/Bonus Survival Stat")]
    public class BuffBonusSurvivalStat : Buff
    {
        [SerializeField] int hpBonus;
        [SerializeField] int armorBonus;
        [SerializeField] int magicResistBonus;

        public override void Excute(BaseUnit unit)
        {
            if (buffOneRound)
            {
                OneRound(unit);
            }
            else
            {
                EndLess(unit);
            }
        }

        private void EndLess(BaseUnit unit)
        {
            var USS = unit.GetUnitSurvivalStat;

            USS.UpBaseStatArmor(armorBonus);
            USS.UpBaseStatMaxHP(hpBonus);
            USS.UpBaseStatMR(magicResistBonus);
        }

        private void OneRound(BaseUnit unit)
        {
            var USS = unit.GetUnitSurvivalStat;

            USS.UpOneRoundStatArmor(armorBonus);
            USS.UpOneRoundStatMaxHP(hpBonus);
            USS.UpOneRoundStatMR(magicResistBonus);
        }
    }
}

