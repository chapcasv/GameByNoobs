using PH.GraphSystem;
using UnityEngine;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop/Logic/Summon")]
    public class TriggerSummonUnit : CardDropTriggerLogic
    {
        
        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, CardDropTriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            var inputSummon = (TriggerInputSummon)input;

            CardUnit unitSummon = inputSummon.GetUnit;

            return boardSystem.TrySpawnUnit(unitSummon, dropNode, team);
        }
    }
}

