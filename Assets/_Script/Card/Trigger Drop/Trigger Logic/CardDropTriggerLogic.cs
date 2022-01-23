using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    public abstract class CardDropTriggerLogic : ScriptableObject
    {
        public abstract bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, TriggerInput input, UnitTeam team = UnitTeam.Player);
    }
}

