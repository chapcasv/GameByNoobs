using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{
    public class UnitNormalSurvivalStat : UnitSurvivalStat
    {

        public override void SetUp(int maxHP, int armor, int mr, UnitTeam team)
        {
            baseMaxHP = maxHP;
            orMaxHP = BaseMaxHP;
            ORCurrentHP = ORMaxHP;

            BaseArmor = armor;
            BaseMagicResist = mr;
            IsLive = true;

            if (team == UnitTeam.Enemy) HpBar.color = COLOR_ENEMY;
            else HpBar.color = COLOR_PLAYER_TEAM;
        }

        public override void TakeDamage(int amount)
        {
            if (!IsLive) return;

            ORCurrentHP -= amount;

            float currentHPPct = (float)ORCurrentHP / ORMaxHP;
            TriggerOnTakeDmg(currentHPPct);

            if (ORCurrentHP <= 0 && IsLive)
            {
                IsLive = false;
                Die();
            }
        }

        public override void Die() => TriggerOnDie();
    }

}

