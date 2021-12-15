using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class UnitSkillPointBar : MonoBehaviour
    {
        [SerializeField] Image spBar;

        private void Awake()
        {
            var IUnitSP = GetComponentInParent<IUnitSkillPoint>();
            IUnitSP.OnSPIncrease += UnitSkillPointBar_OnSPIncrease;
        }

        private void UnitSkillPointBar_OnSPIncrease(float pct)
        {
            spBar.fillAmount = pct;
        }
    }
}

