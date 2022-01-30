using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Passive", menuName = "ScriptableObject/Card/Trigger Input/Buff/Passive Kill Low Hp enemy")]
    public class PassiveKillLowhealthEnemy : Buff
    {
        [SerializeField] private SlayerLowHealthEnemy slayer;
        public override void Execute(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;

            if (buffOneRound)
            {
                
            }
            else
            {
                atkSystem.AddOnBasicAtk(slayer);
            }
        }
    }

}
