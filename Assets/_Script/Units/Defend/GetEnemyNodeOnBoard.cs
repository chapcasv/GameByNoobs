using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class GetEnemyNodeOnBoard : ScriptableObject
    {
        public abstract void Get(BaseUnit target, UnitAtkSystem atkSystem);
    }

}
