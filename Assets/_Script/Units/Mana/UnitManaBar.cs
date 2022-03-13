using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class UnitManaBar : MonoBehaviour
    {
        [SerializeField] Image manaBar;
        private ManaSystem mana;

        private void Awake()
        {
            mana = GetComponentInParent<ManaSystem>();
        }

        private void OnEnable()
        {
            Reload();
            mana.OnManaValueChange += OnManaChange;
        }

        private void Reload()
        {
            if (mana.ORMaxMana == 0) return;

            float pct = (float)mana.ORManaCurrent / mana.ORMaxMana;
            manaBar.fillAmount = pct;
        }

        private void OnManaChange(float pct) => manaBar.fillAmount = pct;

        private void OnDisable() => mana.OnManaValueChange -= OnManaChange;
    }
}

