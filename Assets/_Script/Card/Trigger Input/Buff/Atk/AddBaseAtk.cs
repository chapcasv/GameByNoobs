using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class AddBaseAtk : Buff
    {
        [SerializeField] private AddOnBonusBasicAtk addATK;
        public override void Execute(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;

            if (buffOneRound)
            {
                
            }
            else
            {
                atkSystem.AddToBaseATK(addATK);
            }
        }
    }

}
