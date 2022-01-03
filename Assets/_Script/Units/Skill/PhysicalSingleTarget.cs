using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Physical Single Target")]
    public class PhysicalSingleTarget : Ability
    {
        [SerializeField] int physicalDmg;

        public override void CastSkill(BaseUnit currentTarget,UnitAtkSystem atkSystem)
        {
            if (currentTarget == null) return; //Target dead

            int dmg = physicalDmg + atkSystem.PhysicalDamage;
            currentTarget.TakeDamage(dmg);
        }
    }
}

