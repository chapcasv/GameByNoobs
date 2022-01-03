using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "BPA", menuName = "ScriptableObject/Card/Item/ Bonus Physical Atk Stat")]
    public class ItemBonusPhysicalAtkStat : ScriptableObject
    {
        [SerializeField] int damageBonus;
        [Range(0, 25)]
        [SerializeField] int critRateBonus;
        [Range(0, 20)]
        [SerializeField] int lifeStealBonus;
        [Range(0f,1f)]
        [SerializeField] float atkSpeedBonus;

        public void UpPhysicalAtkStat(UnitAtkSystem UPA)
        {
            UPA.UpAtkSPD(atkSpeedBonus);
            UPA.UpCritRate(critRateBonus);
            UPA.UpDamage(damageBonus);
            UPA.UpLifeSteal(lifeStealBonus);
        }
    }
}

