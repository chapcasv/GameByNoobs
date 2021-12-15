using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class UnitManaBar : MonoBehaviour
    {
        [SerializeField] Image manaBar;

        private void Awake()
        {
            var IMana = GetComponentInParent<IMana>();
            IMana.OnManaIncrease += OnManaChange;
        }

        private void OnManaChange(float pct)
        {
            manaBar.fillAmount = pct;
        }
    }
}

