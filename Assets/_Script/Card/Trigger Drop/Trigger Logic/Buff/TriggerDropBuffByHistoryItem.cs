using PH.GraphSystem;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop Logic/Buff/Buff By History Item")]
    public class TriggerDropBuffByHistoryItem : CardDropTriggerLogic
    {
        [SerializeField] SpawnSystem spawnSystem;

        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, TriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            BaseUnit lastUnitSpawn = spawnSystem.GetLastUnitSpawn();

            TriggerInputSingle inputSingle = (TriggerInputSingle)input;

            int numberItemDrop = CardDropHistory.GetCardItems(team).Count;

            Buff[] buffs = inputSingle.GetBuffs;

            for (int i = 0; i < numberItemDrop; i++)
            {
                for (int j = 0; j < buffs.Length; j++)
                {
                    buffs[j].Execute(lastUnitSpawn);
                }
            }
            return true;
        }
    }
}

