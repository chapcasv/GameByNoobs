using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Unit/Add on basic Atk/Decrease Mana")]
    public class DecreaseManaTargetOnBasicAtk : AddOnBasicAtk
    {
        [SerializeField] int value;

        public override void Execute(BaseUnit currentTarget, UnitAtkSystem atkSystem)
        {
            var mana = currentTarget.GetManaSystem;
            mana.DecreaseMana(value);
        }
    }
}

