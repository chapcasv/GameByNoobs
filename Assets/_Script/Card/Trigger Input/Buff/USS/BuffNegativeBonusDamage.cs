using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    [CreateAssetMenu(fileName = "BSS", menuName = "ScriptableObject/Card/Trigger Input/Buff/Negative Bonus Damage")]
    public class BuffNegativeBonusDamage : Buff
    {
        public override void Execute(BaseUnit unit)
        {
            if (buffOneRound)
            {
                OneRound(unit);
            }
            else
            {
                EndLess(unit);
            }
        }

        private void EndLess(BaseUnit unit)
        {
            var USS = unit.GetUnitSurvivalStat;
            USS.BuffNegative(true);
        }

        private void OneRound(BaseUnit unit)
        {
            throw new NotImplementedException();
        }
    }

}
