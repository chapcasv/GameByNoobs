using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "BPA", menuName = "ScriptableObject/Card/Trigger Drop/Input/Buff/Bonus Physical Atk Stat")]
    public class BuffBonusPhysicalAtkStat : Buff
    {
        [SerializeField] int damageBonus;
        [Range(0, 25)]
        [SerializeField] int critRateBonus;
        [Range(0, 20)]
        [SerializeField] int lifeStealBonus;
        [Range(0f,1f)]
        [SerializeField] float atkSpeedBonus;

        public override void BuffUnit(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem();

            atkSystem.UpAtkSPD(atkSpeedBonus);
            atkSystem.UpCritRate(critRateBonus);
            atkSystem.UpDamage(damageBonus);
            atkSystem.UpLifeSteal(lifeStealBonus);
        }
    }
}

