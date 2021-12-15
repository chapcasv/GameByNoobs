using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Skills/Target Skill")]
    public class TargetSkill : UnitSkill
    {
        public override void CastSkill(BaseUnit currentTarget)
        {
            if (currentTarget == null) return; //Target dead

            int dmg = damage * 2;
            currentTarget.TakeDamage(dmg);
        }
    }
}

