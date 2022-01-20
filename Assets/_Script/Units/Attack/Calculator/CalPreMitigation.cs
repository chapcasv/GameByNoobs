using UnityEngine;

namespace PH
{   
    /// <summary>
    /// Use for UnitAtkSystem
    /// This class will caculator pre-mitigation damage before send raw damage to current target
    /// </summary>
    public abstract class CalPreMitigation : ScriptableObject
    {
        public abstract void Cal(ref int dmg, BaseUnit currentTarget, UnitAtkSystem atkSystem);
    }
}

