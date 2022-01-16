using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [RequireComponent(typeof(MeleeUnitAtk))]
    public class MeleeUnit : BaseUnit
    {
        protected override void SetUpAttack(CardUnit unit)
        {
            Atk = gameObject.GetComponent<MeleeUnitAtk>();
            Atk.Constructor(unit.AtkSpeed, unit.Range, unit.Damage, unit.CritRate, unit.Abitity, SurvivalStat, anim);
        }
    }
}

