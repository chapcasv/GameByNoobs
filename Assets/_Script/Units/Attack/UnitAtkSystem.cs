using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class UnitAtkSystem : MonoBehaviour
    {
        [SerializeField] protected Ability ability;
        [SerializeField] protected float attackSpeed;
        [SerializeField] protected int damage;
        protected int critRate;
        [SerializeField] protected int lifeSteal;
        protected int abilityPct;

        protected float range;

        protected bool canAttack;
        protected bool canCastAbility;
        protected float waitBetweenAttack;
        protected UnitSurvivalStat unitSurvivalStat;
        protected Animator animator;

        public int PhysicalDamage => damage;
        public int AbilityPower => abilityPct;
        public bool CanAtk => canAttack;
        public bool CanCastAbility => canCastAbility;
        
        public virtual void Constructor(float ats, float range, int dmg, int critRate, Ability ability, 
                                UnitSurvivalStat uss, Animator anim)
        {
            unitSurvivalStat = uss;
            attackSpeed = ats;
            damage = dmg;
            canAttack = true;
            canCastAbility = true;
            abilityPct = 100;
            this.critRate = critRate;
            this.ability = ability;
            this.range = CaculatorRangeAtk(range);
            animator = anim;
        }


        public virtual void ReLoadStat(UnitItem item) => item.SetPhysicalAtkStat(this);

        public virtual void UpAtkSPD(float value) => attackSpeed += value;

        public virtual void UpCritRate(int value) => critRate += value;

        public virtual void UpDamage(int value) => damage += value;

        public virtual void UpLifeSteal(int value) => lifeSteal += value;

        public abstract bool IsInRange(BaseUnit currentTarget);
        public abstract bool IsInRangeAbility(BaseUnit currentTarget);

        public abstract void BasicAtk(BaseUnit currentTarget);
        public abstract void CastAbility(BaseUnit currentTarget);

        private float CaculatorRangeAtk(float range) => range * 6 + 2.5f; //CellSize
    }
}

