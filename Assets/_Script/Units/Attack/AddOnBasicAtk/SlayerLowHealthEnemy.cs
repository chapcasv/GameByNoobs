using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Unit/Add on basic Atk/Slayer Low Health enemy")]
    public class SlayerLowHealthEnemy : AddOnBasicAtk
    {
       
        public override void Execute(BaseUnit currentTarget, UnitAtkSystem atkSystem)
        {
            var curHP = currentTarget.GetUnitSurvivalStat;
            int lowHp = (int)(curHP.ORMaxHP / 10);
            if(curHP.ORCurrentHP <= lowHp)
            {
                Debug.Log("a");
                curHP.ORCurrentHP = 0;
            }
        }
    }

}
