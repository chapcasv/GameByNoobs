using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [RequireComponent(typeof(GiongAtkSystem))]
    [RequireComponent(typeof(DragUnit))]
    [RequireComponent(typeof(UnitEquipment))]
    [RequireComponent(typeof(UnitStatusEffect))]
    [RequireComponent(typeof(NormalUnitMove))]

    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class GiongUnit : BaseUnit
    {
        protected int _giongWPCount = 0;

        protected override void SetUpAttack(CardUnit unit)
        {
            Atk = gameObject.GetComponent<GiongAtkSystem>();
            Atk.Constructor(unit.AtkSpeed, unit.Range, unit.Damage, unit.Abitity, SurvivalStat, anim);
            Atk.Holder = this;
        }

        public void IncreaseGiongWPcount()
        {
            _giongWPCount++;

            if(_giongWPCount == 4)
            {
                Debug.Log("Level up");
            }
        }
    }
}


