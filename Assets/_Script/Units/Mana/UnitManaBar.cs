using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class UnitManaBar : MonoBehaviour
    {
        [SerializeField] Image manaBar;
        private IMana mana;

        private void Awake()
        {
            mana = GetComponentInParent<IMana>();
        }

        private void OnEnable() => mana.OnManaValueChange += OnManaChange;

        private void OnManaChange(float pct) => manaBar.fillAmount = pct;

        private void OnDisable() => mana.OnManaValueChange -= OnManaChange;
    }
}

