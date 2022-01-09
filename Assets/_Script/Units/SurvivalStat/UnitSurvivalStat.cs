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
        private int currentHP;
        protected readonly Color32 COLOR_PLAYER_TEAM = new Color32(74, 243, 101, 255); //Green
        protected readonly Color32 COLOR_ENEMY = new Color32(243, 97, 74, 255); //Red
        public bool IsLive { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP
        {
            get => currentHP;
            set
            {
                currentHP = value;
                float currentHPPct = (float)CurrentHP / MaxHP;
                //Update UI after reuse data
                OnHealthUp?.Invoke(currentHPPct);
            }
        }
        public int MagicResist { get; set; }
        public int Armor { get; set; }

        public abstract void UpStatMR(int value);
        public abstract void UpStatMaxHP(int value);
        public abstract void UpStatArmor(int value);

        public abstract void HealthUp(int amount);
        public abstract void SetUp(int maxHP, int armor, int mr, UnitTeam team);
        public abstract void TakeDamage(int amount);

        protected virtual void TriggerOnTakeDmg(float pct) => OnTakeDamage?.Invoke(pct);

        protected virtual void TriggerOnDie() => OnDie?.Invoke();

        protected void TriggerOnHealUp(float pct) => OnHealthUp?.Invoke(pct);

        public abstract void Die();
    }
}