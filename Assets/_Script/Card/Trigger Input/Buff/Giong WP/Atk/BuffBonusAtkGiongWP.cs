using UnityEngine;
using System.Collections.Generic;

namespace PH
{
    [CreateAssetMenu(fileName = "BAS_",
        menuName = "ScriptableObject/Card/Trigger Input/Buff/Giong WP Bonus Atk Stat")]
    public class BuffBonusAtkGiongWP : Buff
    {
        [SerializeField] List<AddOnBasicAtk> listAddOn;

        [SerializeField] List<CalPreMitigation> calPreMitigations;

        [SerializeField] Ability abilityAddOn;
        [SerializeField] string abilityAnimName;

        [Header("Giong")]
        [SerializeField] int damageBonusG;
        [Range(0, 100)]
        [SerializeField] int critRateBonusG;
        [Range(0, 200)]
        [SerializeField] int critDmgBonusG;

        [Range(0, 50)]
        [SerializeField] int lifeStealBonusG;
        [Range(0f, 1f)]
        [SerializeField] float atkSpeedBonusG;

        [SerializeField] float rangeG;
        [SerializeField] int abilityPowerG;

        [Header("Other Unit")]
        [SerializeField] int damageBonus;
        [Range(0, 100)]
        [SerializeField] int critRateBonus;
        [Range(0, 200)]
        [SerializeField] int critDmgBonus;

        [Range(0, 20)]
        [SerializeField] int lifeStealBonus;
        [Range(0f, 1f)]
        [SerializeField] float atkSpeedBonus;

        [SerializeField] float range;
        [SerializeField] int abilityPower;

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

        protected virtual void GiongOneRound(BaseUnit giong)
        {
            GiongAtkSystem atkSystem = (GiongAtkSystem)giong.GetAtkSystem;

            atkSystem.ChangeAbility(abilityAddOn,abilityAnimName);

            atkSystem.UpOneRoundAtkSPD(atkSpeedBonusG);
            atkSystem.UpOneRoundCritRate(critRateBonusG);
            atkSystem.UpOneRoundCritDmg(critDmgBonusG);
            atkSystem.UpOneRoundPhysicalDmg(damageBonusG);
            atkSystem.UpOneRoundLifeSteal(lifeStealBonusG);
            atkSystem.UpOneRoundRangeAtk(rangeG);
            atkSystem.UpOneRoundAbilityPower(abilityPowerG);

            foreach (var addOn in listAddOn)
            {
                atkSystem.AddOneRoundAddOnBasicAtk(addOn);
            }

            foreach (var cal in calPreMitigations)
            {
                atkSystem.AddOneRoundCal(cal);
            }
        }

        protected virtual void OneRound(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;

            atkSystem.UpOneRoundAtkSPD(atkSpeedBonus);
            atkSystem.UpOneRoundCritRate(critRateBonus);
            atkSystem.UpOneRoundCritDmg(critDmgBonus);
            atkSystem.UpOneRoundPhysicalDmg(damageBonus);
            atkSystem.UpOneRoundLifeSteal(lifeStealBonus);
            atkSystem.UpOneRoundRangeAtk(range);
            atkSystem.UpOneRoundAbilityPower(abilityPower);
        }
    }

}
