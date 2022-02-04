using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Add On",
        menuName = "ScriptableObject/Card/Unit/Add On Basic Atk/Slayer Low Health")]
    public class SlayerLowHealthEnemy : AddOnBasicAtk
    {
        public override void Execute(BaseUnit currentTarget, UnitAtkSystem atkSystem)
        {
            var curHp = currentTarget.GetUnitSurvivalStat;
            int lowHp = (int)(curHp.ORMaxHP / 10);

            if(curHp.ORCurrentHP <= lowHp)
            {
                curHp.ORCurrentHP = 0;
            }
        }
    }

}
