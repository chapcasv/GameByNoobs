using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Passive", menuName = "ScriptableObject/Card/Trigger Input/Buff/Passive plus power after cast skill")]
    public class AddPlusDamageCastSkill : Buff
    {
        [SerializeField] private PlusBaseMagicPower addATK;
        public override void Execute(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;

            if (buffOneRound)
            {
                atkSystem.AddOnRoundAfterCastSkill(addATK);
            }
            else
            {
                atkSystem.AddToBaseATK(addATK);
            }
        }
    }

}
