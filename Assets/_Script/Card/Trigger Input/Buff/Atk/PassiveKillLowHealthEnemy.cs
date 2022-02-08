using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Passive", menuName = "ScriptableObject/Card/Trigger Input/Buff/Passive Kill Low Hp Enemy")]
    public class PassiveKillLowHealthEnemy : Buff
    {
        [SerializeField] private SlayerLowHealthEnemy slayer;
        public override void Execute(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;

            if (buffOneRound)
            {
                atkSystem.AddOneRoundAddOnBasicAtk(slayer);
            }
            else
            {
                atkSystem.AddOnBasicAtk(slayer);
            }
        }
    }

}
