using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Add On",
        menuName = "ScriptableObject/Card/Unit/Add On Basic Atk/Slayer Low Health")]
    public class SlayerLowHealthEnemy : AddOnBasicAtk
    {
        [Range(0,1)]
        [SerializeField] private float percent = 0.15f;
        [SerializeField] DamageType trueDmg;
        public override void Execute(BaseUnit currentTarget, UnitAtkSystem atkSystem)
        {
            var curHp = currentTarget.GetUnitSurvivalStat;
            float lowHp = (curHp.ORMaxHP *  percent);
            if(curHp.ORCurrentHP <= lowHp)
            {
                currentTarget.GetUnitSurvivalStat.TakeDmg(999, trueDmg);

            }
        }
    }

}
