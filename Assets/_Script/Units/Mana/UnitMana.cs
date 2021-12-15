using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace PH
{
    public class UnitMana : MonoBehaviour, IMana
    {
        public event Action<float> OnManaIncrease;
        public event Action OnCastSkill;

        private int maxMana;
        private int manaCurrent;
        private int manaRegen;
        private bool isFullMana;

        public void SetMana(int maxMana, int startMana, int manaRegen)
        {
            this.maxMana = maxMana;
            this.manaRegen = manaRegen;
            manaCurrent = startMana;
            isFullMana = false;

            float currentManaPct = (float)manaCurrent / this.maxMana;
            OnManaIncrease?.Invoke(currentManaPct);
        }

        public void CastSkill()
        {
            manaCurrent = 0;
        }

        public void IncreaseMana()
        {
            manaCurrent += manaRegen;
            float currentManaPct = (float)manaCurrent / maxMana;
            OnManaIncrease?.Invoke(currentManaPct);

            if(manaCurrent == maxMana) isFullMana = true;

        }

        public bool IsFullMana() => isFullMana;
    }
}

