using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [RequireComponent(typeof(RangerUnitAtk))]
    public class RangerUnit : BaseUnit
    {

        protected override void SetUpAttack(CardUnit unit)
        {
            Atk = GetComponent<RangerUnitAtk>();
            Atk.Constructor(unit.AtkSpeed, unit.Range, unit.Damage, unit.CritRate, unit.Abitity, SurvivalStat, anim);
        }

        protected override void AttackInRange()
        {
            if (Atk.IsInRange(currentTarget) && !Move.IsMoving && currentTarget.IsLive)
            {
                if (Atk.CanAtk)
                {
                    Atk.BasicAtk(currentTarget);
                    Mana.IncreaseMana();
                }
            }
            else GetInRange();
        }
    }
}

