using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public abstract class AddOnBasicAtk : ScriptableObject
    {
        public abstract void Execute(BaseUnit currentTarget, UnitAtkSystem atkSystem);
    }
}

