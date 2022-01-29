using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public abstract class AddOnBonusBasicAtk : ScriptableObject
    {
        public abstract void Execute(BaseUnit unit);
        
    }

}
