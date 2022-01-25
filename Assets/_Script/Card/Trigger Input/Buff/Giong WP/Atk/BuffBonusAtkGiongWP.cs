using UnityEngine;


namespace PH
{
    [CreateAssetMenu(fileName = "BAS_GiongWP",
        menuName = "ScriptableObject/Card/Trigger Input/Buff/Giong WP Bonus Atk Stat")]
    public class BuffBonusAtkGiongWP : Buff
    {
        [Header("Giong")]
        [SerializeField] int damageBonusG;
        [Range(0, 100)]
        [SerializeField] int critRateBonusG;
        [Range(0, 20)]
        [SerializeField] int lifeStealBonusG;
        [Range(0f, 1f)]
        [SerializeField] float atkSpeedBonusG;

        [SerializeField] float rangeG;
        [SerializeField] int abilityPowerG;

        [Header("Other Unit")]
        [SerializeField] int damageBonus;
        [Range(0, 100)]
        [SerializeField] int critRateBonus;
        [Range(0, 20)]
        [SerializeField] int lifeStealBonus;
        [Range(0f, 1f)]
        [SerializeField] float atkSpeedBonus;

        [SerializeField] float range;
        [SerializeField] int abilityPower;

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

        protected virtual void GiongOneRound(BaseUnit giong)
        {
            var atkSystem = giong.GetAtkSystem;

            atkSystem.UpOneRoundAtkSPD(atkSpeedBonusG);
            atkSystem.UpOneRoundCritRate(critRateBonusG);
            atkSystem.UpOneRoundPhysicalDmg(damageBonusG);
            atkSystem.UpOneRoundLifeSteal(lifeStealBonusG);
            atkSystem.UpOneRoundRangeAtk(rangeG);
            atkSystem.UpOneRoundAbilityPower(abilityPowerG);
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
    }

}
