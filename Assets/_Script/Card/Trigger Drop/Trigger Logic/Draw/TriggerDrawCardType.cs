using PH.GraphSystem;
using UnityEngine;
using System.Collections.Generic;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop Logic/Draw Type")]
    public class TriggerDrawCardType : CardDropTriggerLogic
    {
        [SerializeField] DeckSystem deckSystem;

        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, TriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            TriggerInputDrawCard inputDraw = (TriggerInputDrawCard)input;

            bool result = deckSystem.DrawCardFilter(inputDraw);

            return result;
        }
    }
}

