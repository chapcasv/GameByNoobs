using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{
    public class UnitNormalSurvivalStat : UnitSurvivalStat
    {
        public override void HealthUp(int amount)
        {
            CurrentHP += amount;
            float currentHPPct = (float)CurrentHP / MaxHP;
            TriggerOnHealUp(currentHPPct);
        }

        public override void SetUp(int maxHP, int armor, int mr, UnitTeam team)
        {
            this.MaxHP = maxHP;
            CurrentHP = this.MaxHP;
            this.Armor = armor;
            this.MagicResist = mr;
            IsLive = true;

            if (team == UnitTeam.Enemy) HpBar.color = COLOR_ENEMY;
            else HpBar.color = COLOR_PLAYER_TEAM;
        }

        public override void TakeDamage(int amount)
        {
            if (!IsLive) return;

            CurrentHP -= amount;

            float currentHPPct = (float)CurrentHP / MaxHP;
            TriggerOnTakeDmg(currentHPPct);

            if (CurrentHP <= 0 && IsLive)
            {
                IsLive = false;
                Die();
            }
        }

        public override void ReLoadStat(UnitItem item)
        {   
            item.SetSurvivalStat(this);
        }

        public override void UpStatMaxHP(int value)
        {
            MaxHP += value;
            HealthUp(value);
        }

        public override void UpStatMR(int value) => MagicResist += value;

        public override void UpStatArmor(int value) => Armor += value;

        public override void Die() => TriggerOnDie();
    }

}

