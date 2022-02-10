using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "CalDmg", menuName = "ScriptableObject/Card/Unit/Calculator Pre Mitigation/Calculator Crit Damage")]
    public class CalCriticalDamge : CalPreMitigation
    {
        
        public override void Cal(ref int dmg, BaseUnit currentTarget, UnitAtkSystem atkSystem)
        {
            if (currentTarget == null) return; //target die

            int critDamageBonus = CalCritDamageBonus(currentTarget, atkSystem);
            int random = Random.Range(0, 100);
            if(random < atkSystem.ORCritRate)
            {
                dmg += (int)(dmg * (critDamageBonus / 100f));
            }
            else
            {
                dmg += 0;
            }
        }

        private int CalCritDamageBonus(BaseUnit currentTarget, UnitAtkSystem atkSystem)
        {

            int damageCrit = 0;
            if (currentTarget.GetUnitSurvivalStat.IsNegatesBonusCritDmg)
            {
                damageCrit = (atkSystem.ORCritDmg - atkSystem.BaseCritDmg);
            }
            else
            {
                damageCrit = atkSystem.ORCritDmg; 
            }
            return damageCrit;
        }
    }
}

