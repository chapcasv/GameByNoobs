using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "CalDmg by Max HP", 
        menuName = "ScriptableObject/Card/Unit/Calculator Pre Mitigation/Calculator by max HP")]
    public class CalDmgByMaxHP : CalPreMitigation
    {
        [SerializeField] int bonusPCT;

        public override void Cal(ref int dmg, BaseUnit currentTarget, UnitAtkSystem atkSystem)
        {
            int maxHPtarget = currentTarget.GetUnitSurvivalStat.BaseMaxHP;

            float dmgBonus = bonusPCT * (maxHPtarget / 100f);

            dmg += (int)dmgBonus;
        }
    }
}

