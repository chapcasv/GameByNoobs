using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [RequireComponent(typeof(RangerUnitAtk))]
    [RequireComponent(typeof(DragUnit))]
    [RequireComponent(typeof(UnitEquipment))]
    
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class RangerUnit : BaseUnit
    {

        protected override void SetUpAttack(CardUnit unit)
        {
            Atk = GetComponent<RangerUnitAtk>();
            Atk.Constructor(unit.AtkSpeed, unit.Range, unit.Damage, unit.Abitity, SurvivalStat, anim);
        }

        protected override void AttackInRange()
        {
            Atk.CurrentTarget = currentTarget;
            if (Atk.IsInRange(currentTarget) && !Move.IsMoving && currentTarget.IsLive)
            {
                if (Atk.CanAtk)
                {
                    Atk.BasicAtk();
                    Mana.IncreaseManaOnHit();
                }
            }
            else GetInRange();
        }
    }
}

