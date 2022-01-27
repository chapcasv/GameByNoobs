using System;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public abstract class UnitSurvivalStat : MonoBehaviour
    {
        public event Action OnDie;
        public event Action<float> OnTakeDamage;
        public event Action<float> OnHealthUp;

        [SerializeField] protected Image HpBar;

        #region Properties

        protected readonly Color32 COLOR_PLAYER_TEAM = new Color32(74, 243, 101, 255); //Green
        protected readonly Color32 COLOR_ENEMY = new Color32(243, 97, 74, 255); //Red

        protected int baseMaxHP;
        protected int orMaxHP;
        private int orCurrentHP;

        protected int baseMagicResist;
        protected int orMagicResist;

        protected int baseArmor;
        protected int orArmor;

        protected bool negatesBonusCritDmg;

        public bool IsLive { get; set; }
        public bool CanRegen { get; set; }
        public bool IsNegatesBonusCritDmg => negatesBonusCritDmg;
        public int BaseMaxHP { get => baseMaxHP;}
        public int ORMaxHP => orMaxHP;
        public int ORCurrentHP
        {
            get => orCurrentHP;
            set
            {
                orCurrentHP = value;
                float currentHPPct = (float)orCurrentHP / orMaxHP;
                //Update UI after reuse data
                OnHealthUp?.Invoke(currentHPPct);
            }
        }

        public int BaseMagicResist { get => baseMagicResist;}
        public int ORMagicResist => orMagicResist;
        public int BaseArmor { get => baseArmor;}
        public int ORArmor => orArmor;

        public abstract void SetUp(int maxHP, int armor, int mr,bool negativeBonusDamage ,UnitTeam team);
      
        #endregion

        #region Reuse Methods - Use for PlayerCacheUnitData
        public void ReuseMaxHP(int value)
        {
            baseMaxHP = value;
            orMaxHP = baseMaxHP;
            ORCurrentHP = baseMaxHP;
        }

        public void ReuseArmor(int value)
        {
            baseArmor = value;
            orArmor = baseArmor;
        }

        public void ReuseMagicResist(int value)
        {
            baseMagicResist = value;
            orMagicResist = baseMagicResist;
        }

        #endregion

        #region UpStat Methods - Use for buff
        public virtual void UpOneRoundStatMaxHP(int value)
        {
            orMaxHP += value;
            ORCurrentHP = orMaxHP;
        }

        public virtual void UpBaseStatMaxHP(int value)
        {
            baseMaxHP += value;
            orMaxHP += value;
            ORCurrentHP = orMaxHP;
        }

        public virtual void UpOneRoundStatMR(int value)
        {
            orMagicResist += value;
        }

        public virtual void UpBaseStatMR(int value)
        {
            baseMagicResist += value;
            orMagicResist += value;
        }

        public virtual void UpOneRoundStatArmor(int value)
        {
            orArmor += value;
        }

        public virtual void UpBaseStatArmor(int value)
        {
            baseArmor += value;
            orArmor += value;
        }
        public virtual void BuffNegative(bool isNegative)
        {
            this.negatesBonusCritDmg = isNegative;
        }
        #endregion
        
        public void HealthUp(int amount)
        {
            ORCurrentHP += amount;

            if(ORCurrentHP > orMaxHP)
            {
                ORCurrentHP = orMaxHP;
            }
            float currentHPPct = (float)ORCurrentHP / orMaxHP;
            OnTakeDamage?.Invoke(currentHPPct);
        }


        public virtual int TakeDmg(int rawDmg, DmgType dmgType)
        {
            if (!IsLive) return 0;

            int postMitigationDmg = CalculatorByDmgType(rawDmg, dmgType);

            DecreaseHP(postMitigationDmg);

            return postMitigationDmg;
        }

        private void DecreaseHP(int postMitigationDmg)
        {
            ORCurrentHP -= postMitigationDmg;

            float currentHPPct = (float)ORCurrentHP / orMaxHP;

            OnTakeDamage?.Invoke(currentHPPct);

            if (ORCurrentHP <= 0 && IsLive)
            {
                IsLive = false;
                Die();
            }
        }

        protected virtual int CalculatorByDmgType(int rawDmg, DmgType type)
        {
            switch (type)
            {
                case DmgType.Physical:
                    return CalPostMitigationPhysical(rawDmg);
                case DmgType.Magic:
                    return CalPostMitigationMagic(rawDmg);
                case DmgType.TrueDmg:
                    return CalPostMitigationTrueDmg(rawDmg);
                case DmgType.DoT:
                    return CalPostMitigationDoT(rawDmg);
                default:
                    throw new Exception("Cant get damage type");
            }
        }

        protected virtual int CalPostMitigationPhysical(int rawDmg)
        {
            int postMitigationDmg = CalculatorByDefenseType(rawDmg, orArmor);
            return postMitigationDmg;
        }

        protected virtual int CalPostMitigationMagic(int rawDmg)
        {
            int postMitigationDmg = CalculatorByDefenseType(rawDmg, orMagicResist);
            return postMitigationDmg;
        }

        protected virtual int CalPostMitigationTrueDmg(int rawDmg)
        {
            return rawDmg;
        }

        protected virtual int CalPostMitigationDoT(int rawDmg)
        {
            return rawDmg;
        }

        private int CalculatorByDefenseType(int rawDmg, int defenseType)
        {
            float pct = 100f;

            float postMitigationDmg = rawDmg * (pct / (pct + defenseType));

            return (int)postMitigationDmg;
        }

        protected virtual void TriggerOnDie() => OnDie?.Invoke();

        protected void TriggerOnHealUp(float pct) => OnHealthUp?.Invoke(pct);

        public abstract void Die();
    }
}