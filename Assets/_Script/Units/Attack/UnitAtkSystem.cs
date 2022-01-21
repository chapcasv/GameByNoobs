using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class UnitAtkSystem : MonoBehaviour
    {
        #region Properties
        protected Ability ability;

        [SerializeField] protected List<CalPreMitigation> baseCalPreMitigations;
        protected List<CalPreMitigation> orCalPreMitigations;

        protected float baseAttackSpeed;
        protected float orAttackSpeed;

        protected int basePhysicalDmg;
        protected int orPhysicalDmg;

        protected const int CRIT_RATE_DEFAULT = 25;
        protected int baseCritRate;
        protected int orCritRate;

        protected const int CRIT_DMG_DEFAULT = 100;
        protected const int CRIT_DMG_BONUS_DEFAULT = 30;
        protected int baseCritDmg;
        protected int baseCritDmgBonus;
        protected int orCritDmg;

        protected const int LIFE_STEAL_DEFAULT = 0;
        protected int baseLifeSteal;
        protected int orLifeSteal;

        protected float baseRangeAtk;
        protected float orRangeAtk;

        protected const int MAGIC_DEFAULT = 100;
        protected int baseMagicPower;
        protected int orMagicPower;

        protected bool canAttack;
        protected bool canCastAbility;
        protected float waitBetweenAttack;

        protected UnitSurvivalStat unitSurvivalStat;
        protected Animator animator;

        public float BaseAttackSpeed { get => baseAttackSpeed; }
        public float ORAtkSpd => orAttackSpeed;
        public int BasePhysicalDmg { get => basePhysicalDmg; }
        public int ORPhysicalDamage { get => orPhysicalDmg; }
        public bool CanAtk => canAttack;
        public int BaseCritRate { get => baseCritRate; }
        public int ORCritRate { get => orCritRate; }
        public int BaseCritDmg { get => baseCritDmgBonus; }
        public int ORCritDmg { get => orCritDmg; }
        public int BaseLifeSteal { get => baseLifeSteal; }
        public int ORLifeSteal { get => orLifeSteal; }
        public float BaseRangeAtk { get => baseRangeAtk; }
        public int BaseMagicPower { get => baseMagicPower; }
        public int ORMagicPower { get => orMagicPower; }

        public Ability GetAbility => ability;
        public bool CanCastAbility => canCastAbility;

        #endregion

        public virtual void Constructor(float ats, float range, int dmg, Ability ability,
                                UnitSurvivalStat uss, Animator anim)
        {
            unitSurvivalStat = uss;

            baseAttackSpeed = ats;
            orAttackSpeed = baseAttackSpeed;

            basePhysicalDmg = dmg;
            orPhysicalDmg = basePhysicalDmg;

            baseCritRate = CRIT_RATE_DEFAULT;
            orCritRate = baseCritRate;

            baseCritDmg = CRIT_DMG_DEFAULT;
            baseCritDmgBonus = CRIT_DMG_BONUS_DEFAULT;
            orCritDmg = baseCritDmg + baseCritDmgBonus;

            baseLifeSteal = LIFE_STEAL_DEFAULT;
            orLifeSteal = baseLifeSteal;

            baseRangeAtk = CaculatorRangeAtk(range);
            orRangeAtk = baseRangeAtk;

            baseMagicPower = MAGIC_DEFAULT;
            orMagicPower = baseMagicPower;

            orCalPreMitigations = new List<CalPreMitigation>();

            canAttack = true;
            canCastAbility = true;
        
            this.ability = ability;

            animator = anim;
        }

        #region Reuse Methods - Use for PlayerCacheUnitData

        public void ReuseAtkSpeed(float value)
        {
            baseAttackSpeed = value;
            orAttackSpeed = baseAttackSpeed;
        }

        public void ReusePhysicalDmg(int value)
        {
            basePhysicalDmg = value;
            orPhysicalDmg = basePhysicalDmg;
        }

        public void ReuseCritRate(int value)
        {
            baseCritRate = value;
            orCritRate = baseCritRate;
        }

        public void ReuseCritDmg(int value)
        {
            baseCritDmgBonus = value;
            orCritDmg = baseCritDmg + baseCritDmgBonus;
        }

        public void ReuseLifeSteal(int value)
        {
            baseLifeSteal = value;
            orLifeSteal = baseLifeSteal;
        }

        public void ReuseRangeAtk(float value)
        {
            baseRangeAtk = value;
            orRangeAtk = baseRangeAtk;
        }

        public void ReuseAbilityPower(int value)
        {
            baseMagicPower = value;
            orMagicPower = baseMagicPower;
        }

        #endregion

        #region UpStat Methods - Use for buff

        public virtual void UpBaseAtkSPD(float value)
        {
            baseAttackSpeed += value;
            orAttackSpeed += value;
        }
        public virtual void UpOneRoundAtkSPD(float value) => orAttackSpeed += value;

        public virtual void UpBasePhysicalDmg(int value)
        {
            basePhysicalDmg += value;
            orPhysicalDmg += value;
        }
        public virtual void UpOneRoundPhysicalDmg(int value) => orPhysicalDmg += value;

        public virtual void UpBaseCritRate(int value)
        {
            baseCritRate += value;
            orCritRate += value;
        }
        public virtual void UpOneRoundCritRate(int value) => orCritRate += value;

        public virtual void UpBaseLifeSteal(int value)
        {
            baseLifeSteal += value;
            orLifeSteal += value;
        }
        public virtual void UpOneRoundLifeSteal(int value) => orLifeSteal += value;

        public virtual void UpBaseRangeAtk(float value)
        {   
            float rangeValue = CaculatorRangeAtk(value);
            baseRangeAtk += rangeValue;
            orRangeAtk += rangeValue;
        }
        public virtual void UpOneRoundRangeAtk(float value) => orRangeAtk += CaculatorRangeAtk(value);

        public virtual void UpBaseAbilityPower(int value)
        {
            baseMagicPower += value;
            orMagicPower += value;
        }
        public virtual void UpOneRoundAbilityPower(int value) => orMagicPower += value;
        #endregion

        #region Caculator Pre-mitigation damage

        public void RemoveOneRoundAddOn()
        {
            RemoveOneRoundCal();
        }

        protected void RemoveOneRoundCal() => orCalPreMitigations.Clear();

        public void AddOneRoundCal(CalPreMitigation cal) => orCalPreMitigations.Add(cal);

        public void AddBaseCaculator(CalPreMitigation cal) => baseCalPreMitigations.Add(cal);

        public void RemoveBaseCaculator(CalPreMitigation cal)
        {
            if (baseCalPreMitigations.Contains(cal))
            {
                baseCalPreMitigations.Remove(cal);
            }
        }

        protected void Caculator(ref int preMitigationDmg, BaseUnit currentTarget)
        {
            foreach (var cal in baseCalPreMitigations)
            {
                cal.Cal(ref preMitigationDmg, currentTarget, this);
            }

            foreach (var cal in orCalPreMitigations)
            {
                cal.Cal(ref preMitigationDmg, currentTarget, this);
            }
        }

        #endregion

        public abstract bool IsInRange(BaseUnit currentTarget);
        public abstract bool IsInRangeAbility(BaseUnit currentTarget);
        public abstract void BasicAtk(BaseUnit currentTarget);
        public abstract void CastAbility(BaseUnit currentTarget);

        public virtual void LifeStealByDmg(int damageDealt)
        {
            if (orLifeSteal == 0) return;
            if (!unitSurvivalStat.CanRegen) return;

            var heals = CalHPRegenByDmgDealt(damageDealt);
            unitSurvivalStat.HealthUp(heals);
        }

        private int CalHPRegenByDmgDealt(int dmgDealt)
        {   
            //use ORstat
            dmgDealt *= 2;
            return dmgDealt;
        }

        protected void RotationFollowTarget(BaseUnit currentTarget)
        {
            Vector3 moveDir = (currentTarget.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(moveDir);
            transform.rotation = rotation;
        }

        private float CaculatorRangeAtk(float range)
        {   
            if(range == 0)
            {
                return 0;
            }
            else return range * 6 + 2.5f; //CellSize
        }
    }
}
