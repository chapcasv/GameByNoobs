using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace PH
{
    public class UnitSkillPoint : MonoBehaviour, IUnitSkillPoint
    {
        [SerializeField] Image SpBar;

        public event Action<float> OnSPIncrease;
        public event Action OnCastSkill;

        private int maxSP;
        private int currentSP;
        private int spRegen;

        public void SetSP(int maxSP, int startSP, int spRegen)
        {
            this.maxSP = maxSP;
            this.spRegen = spRegen;
            currentSP = startSP;

            float currentSPPct = (float)currentSP / this.maxSP;
            OnSPIncrease?.Invoke(currentSPPct);
        }

        public void CastSkill()
        {
            currentSP = 0;
        }

        public void IncreaseSP()
        {
            currentSP += spRegen;
            float currentSPPct = (float)currentSP / maxSP;
            OnSPIncrease?.Invoke(currentSPPct);

            if(currentSP == maxSP)
            {
                CastSkill();
                OnCastSkill?.Invoke();
            }
        }
    }
}

