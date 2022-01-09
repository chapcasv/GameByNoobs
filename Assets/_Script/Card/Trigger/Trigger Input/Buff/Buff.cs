using UnityEngine;

namespace PH
{
    public abstract class Buff : ScriptableObject
    {
        public abstract void BuffUnit(BaseUnit unit);
    }
}

