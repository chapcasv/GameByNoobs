using UnityEngine;

namespace PH
{
    public abstract class Buff : ScriptableObject
    {
        [SerializeField] protected bool buffOneRound = false;
        public abstract void Execute(BaseUnit unit);
    }
}

