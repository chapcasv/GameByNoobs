using PH.GraphSystem;
using UnityEngine;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop Logic/Spawn form Trigger Input")]
    public class TriggerSpawnFormTriggerInput : TriggerSpawn
    {
        
        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, TriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            var inputSummon = (TriggerInputSummon)input;

            CardUnit unitSummon = inputSummon.GetUnit;

            bool result = boardSystem.TrySpawnUnit(unitSummon, dropNode, team);

            if (result) AddHistory(unitSummon, team);

            return result;
        }
    }
}

