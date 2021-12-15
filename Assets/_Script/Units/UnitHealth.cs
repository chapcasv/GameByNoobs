using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PH.GraphSystem;
using UnityEngine.UI;

namespace PH
{
    public class UnitHealth : MonoBehaviour, IUnitHealth
    {
        [SerializeField] Image HpBar;

        public event Action<float> OnTakeDamage;
        public event Action<BaseUnit> OnDie;
        public event Action<float> OnHealthUp;

        private readonly Color32 COLOR_PLAYER_TEAM = new Color32(74, 243, 101, 255); //Green
        private readonly Color32 COLOR_ENEMY = new Color32(243, 97, 74, 255); //Red

        private int maxHP;
        private int currentHP;

        public void HealthUp(int amount)
        {
            currentHP += amount;
            float currentHPPct = (float)currentHP / maxHP;
            OnHealthUp?.Invoke(currentHPPct);
        }

        public void SetHP(int value, UnitTeam team)
        {
            maxHP = value;
            currentHP = maxHP;

            if (team == UnitTeam.Enemy) HpBar.color = COLOR_ENEMY;
            else HpBar.color = COLOR_PLAYER_TEAM;
        }

        public void TakeDamage(int amount, BaseUnit unit)
        {
            currentHP -= amount;

            float currentHPPct = (float)currentHP / maxHP;
            OnTakeDamage?.Invoke(currentHPPct);

            if (currentHP <= 0 && !unit.Dead)
            {
                unit.Dead = true;
                unit.CurrentNode.SetOccupied(false);
                Die(unit);
            }
        }

        public void Die(BaseUnit unit)
        {
            OnDie?.Invoke(unit);
        }
    }
}

