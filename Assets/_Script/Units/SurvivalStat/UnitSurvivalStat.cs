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

        protected readonly Color32 COLOR_PLAYER_TEAM = new Color32(74, 243, 101, 255); //Green
        protected readonly Color32 COLOR_ENEMY = new Color32(243, 97, 74, 255); //Red

        protected int baseMaxHP;
        protected int orMaxHP;
        protected int orCurrentHP;

        protected int baseMagicResist;
        protected int orMagicResist;

        protected int baseArmor;
        protected int orArmor;

        public bool IsLive { get; set; }
        public int BaseMaxHP { get => baseMaxHP;}
        public int ORMaxHP { get => orMaxHP;}
        public int ORCurrentHP
        {
            get => orCurrentHP;
            set
            {
                orCurrentHP = value;
                float currentHPPct = (float)ORCurrentHP / BaseMaxHP;
                //Update UI after reuse data
                OnHealthUp?.Invoke(currentHPPct);
            }
        }

        public int BaseMagicResist { get => baseMagicResist; set => baseMagicResist = value; }
        public int ORMagicResist { get => orMagicResist; set => orMagicResist = value; }
        public int BaseArmor { get => baseArmor; set => baseArmor = value; }
        public int ORArmor { get => orArmor; set => orArmor = value; }

        public abstract void SetUp(int maxHP, int armor, int mr, UnitTeam team);
        public abstract void TakeDamage(int amount);

        public void ReuseMaxHP(int value)
        {
            baseMaxHP = value;
            orMaxHP = baseMaxHP;
            ORCurrentHP = baseMaxHP;
        }

        public virtual void UpOneRoundStatMaxHP(int value)
        {
            orMaxHP += value;
            ORCurrentHP = ORMaxHP;
        }

        public virtual void UpBaseStatMaxHP(int value)
        {
            baseMaxHP += value;
            orMaxHP += value;
            ORCurrentHP = ORMaxHP;
        }

        public virtual void UpOneRoundStatMR(int value)
        {
            ORMagicResist += value;
        }

        public virtual void UpBaseStatMR(int value)
        {
            BaseMagicResist += value;
            ORMagicResist += value;
        }

        public virtual void UpOneRoundStatArmor(int value)
        {
            ORArmor += value;
        }
        public virtual void UpBaseStatArmor(int value)
        {
            BaseArmor += value;
            ORArmor += value;
        }
        protected void HealthUp(int amount)
        {
            ORCurrentHP += amount;
            float currentHPPct = (float)ORCurrentHP / BaseMaxHP;
            OnTakeDamage?.Invoke(currentHPPct);
        }



        protected virtual void TriggerOnTakeDmg(float pct) => OnTakeDamage?.Invoke(pct);

        protected virtual void TriggerOnDie() => OnDie?.Invoke();

        protected void TriggerOnHealUp(float pct) => OnHealthUp?.Invoke(pct);

        public abstract void Die();
    }
}