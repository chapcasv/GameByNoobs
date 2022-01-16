using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    public abstract class CardDropTriggerLogic : ScriptableObject
    {
        public abstract bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, CardDropTriggerInput input, UnitTeam team = UnitTeam.Player);
    }
}

