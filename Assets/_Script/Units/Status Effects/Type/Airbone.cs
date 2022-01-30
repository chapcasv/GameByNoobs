using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Unit/Status Effects/Airbone")]
    public class Airbone : StatusEffect
    {
        public override void Execute(BaseUnit unit)
        {
            var Atk = unit.GetAtkSystem;

            Atk.CanAtk = false;
            Atk.IsDisableAtk = true;
            Atk.CanCastAbility = false;
            unit.GetMove.CanMove = false;
        }

        public override void Remove(BaseUnit unit)
        {
            var Atk = unit.GetAtkSystem;

            Atk.CanAtk = true;
            Atk.IsDisableAtk = false;
            Atk.CanCastAbility = true;
            unit.GetMove.CanMove = true;
        }
    }
}

