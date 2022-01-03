using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{   
    [CreateAssetMenu(fileName ="BSS", menuName = "ScriptableObject/Card/Item/Bonus Survival Stat")]
    public class ItemBonusSurvivalStat : ScriptableObject
    {
        [SerializeField] int hpBonus;
        [SerializeField] int armorBonus;
        [SerializeField] int magicResistBonus;

        public void UpSurvialStat(UnitSurvivalStat uss)
        {
            uss.UpStatMaxHP(hpBonus);
            uss.UpStatMR(magicResistBonus);
            uss.UpStatArmor(armorBonus);
        }

    }
}

