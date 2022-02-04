using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Plus Damage",
        menuName = "ScriptableObject/Card/Unit/Add On After Cast Skill/Plus Magic Power")]
    public class PlusMagicPower : AddOnAfterCastSkill
    {
        [SerializeField] private int value;
        public override void Execute(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;
            atkSystem.UpBaseAbilityPower(value);
        }

    }

}
