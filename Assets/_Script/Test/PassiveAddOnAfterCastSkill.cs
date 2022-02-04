using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Passive", menuName = "ScriptableObject/Card/Trigger Input/Buff/Passive Plus Magic Power")]
    public class PassiveAddOnAfterCastSkill : Buff
    {
        [SerializeField] private PlusMagicPower plus;
        public override void Execute(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;

            if (buffOneRound)
            {
                atkSystem.AddOnRoundAfterCastSkill(plus);
            }
            else
            {
                atkSystem.AddAfterCastSkill(plus);
            }
        }
    }

}
