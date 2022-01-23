using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop Logic/Spawn form CardUnit")]
    public class TriggerSpawnFormCardUnit : TriggerSpawn
    {
        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, TriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            var cardUnit = (CardUnit)card;

            bool result = boardSystem.TrySpawnUnit(cardUnit, dropNode);

            if (result)
            {
                AddHistory(cardUnit, team);
            }
            return result;
        }
    }
}

