using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(fileName ="BSS", menuName = "ScriptableObject/Card/Trigger Drop/Input/Buff/Bonus Survival Stat")]
    public class BuffBonusSurvivalStat : Buff
    {
        [SerializeField] int hpBonus;
        [SerializeField] int armorBonus;
        [SerializeField] int magicResistBonus;

        public override void BuffUnit(BaseUnit unit)
        {
            var USS = unit.GetUnitSurvivalStat();

            USS.UpStatArmor(armorBonus);
            USS.UpStatMaxHP(hpBonus);
            USS.UpStatMR(magicResistBonus);
        }
    }
}

