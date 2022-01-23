using PH.GraphSystem;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop Logic/Create Card/Create By Number Item History")]
    public class CreateCardByNumberItemHistory : CardDropTriggerLogic
    {
        [SerializeField] DeckSystem deckSystem;

        public override bool CanTrigger(Node dropNode, BoardSystem BS, Card card, TriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            InputCreateItemByHistoryItem inputCreateItem = (InputCreateItemByHistoryItem)input;

            int require = inputCreateItem.RequireHistoryItemCount;

            int historyItemCount = CardDropHistory.GetCardItems(team).Count;

            if(historyItemCount >= require)
            {
                Card[] cardArray = inputCreateItem.GetCard();

                for (int i = 0; i < cardArray.Length; i++)
                {
                    deckSystem.AddCardToHand(cardArray[i]);
                }
            }

            return true;
        }
    }
}

