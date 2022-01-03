using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    public abstract class Ability : ScriptableObject
    {   
        [Range(1,4)]
        [SerializeField] float range = 1;

        public float GetRange()
        {
            return range * 6 + 2.5f;
        }
        public abstract void CastSkill(BaseUnit currentTarget, UnitAtkSystem atkSystem);
    }
}

