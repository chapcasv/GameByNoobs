using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "BAS", menuName = "ScriptableObject/Card/Trigger Drop/Input/Buff/Bonus Atk Stat")]
    public class BuffBonusAtkStat : Buff
    {
        [SerializeField] int damageBonus;
        [Range(0, 100)]
        [SerializeField] int critRateBonus;
        [Range(0, 20)]
        [SerializeField] int lifeStealBonus;
        [Range(0f,1f)]
        [SerializeField] float atkSpeedBonus;

        [SerializeField] float range;

        [SerializeField] int abilityPower;

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

        protected virtual void OneRound(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;

            atkSystem.UpOneRoundAtkSPD(atkSpeedBonus);
            atkSystem.UpOneRoundCritRate(critRateBonus);
            atkSystem.UpOneRoundPhysicalDmg(damageBonus);
            atkSystem.UpOneRoundLifeSteal(lifeStealBonus);
            atkSystem.UpOneRoundRangeAtk(range);
            atkSystem.UpOneRoundAbilityPower(abilityPower);
        }

        protected virtual void EndLess(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;

            atkSystem.UpBaseAtkSPD(atkSpeedBonus);
            atkSystem.UpBaseCritRate(critRateBonus);
            atkSystem.UpBasePhysicalDmg(damageBonus);
            atkSystem.UpBaseLifeSteal(lifeStealBonus);
            atkSystem.UpBaseRangeAtk(range);
            atkSystem.UpBaseAbilityPower(abilityPower);
        }
    }
}

