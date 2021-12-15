using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    public abstract class UnitSkill : ScriptableObject
    {
        protected int damage;

        public virtual void SetUp(int dmg)
        {
            damage = dmg;
        }

        public abstract void CastSkill(BaseUnit currentTarget);
    }
}

