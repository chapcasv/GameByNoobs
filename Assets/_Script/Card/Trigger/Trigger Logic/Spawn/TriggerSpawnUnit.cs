using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop/Logic/Spawn")]
    public class TriggerSpawnUnit : CardDropTriggerLogic
    {
        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, CardDropTriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            var cardUnit = (CardUnit)card;
            return boardSystem.TrySpawnUnit(cardUnit, dropNode);
        }
    }
}

