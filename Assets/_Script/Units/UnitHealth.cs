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
        public event Action OnDie;
        public event Action<float> OnHealthUp;

        private readonly Color32 COLOR_PLAYER_TEAM = new Color32(74, 243, 101, 255); //Green
        private readonly Color32 COLOR_ENEMY = new Color32(243, 97, 74, 255); //Red

        private int maxHP;
        private int currentHP;

        public bool IsLive { get; set; }

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
            IsLive = true;

            if (team == UnitTeam.Enemy) HpBar.color = COLOR_ENEMY;
            else HpBar.color = COLOR_PLAYER_TEAM;
        }

        public void TakeDamage(int amount)
        {
            if (!IsLive) return;

            currentHP -= amount;

            float currentHPPct = (float)currentHP / maxHP;
            OnTakeDamage?.Invoke(currentHPPct);

            if (currentHP <= 0 && IsLive)
            {
                Die();
            }
        }

        public void Die()
        {   
            OnDie?.Invoke();
        }
    }
}

