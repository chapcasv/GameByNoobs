using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PH
{
    public class AddBaseSMCK : AddOnBonusBasicAtk
    {
        [SerializeField] private int value;
        public override void Execute(BaseUnit unit)
        {
            var atkSys = unit.GetAtkSystem;

            atkSys.UpBasePhysicalDmg(value);
        }
        
    }

}
