using UnityEngine;
using System;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Collection/Deck Library/Logic")]
    public class DeckLibraryLogic : CollectionLogic
    {
        public event Action OnClickLogic;
        private ChildCardUI _childCardUI;
        public void SetChildCardUI(ChildCardUI value) => _childCardUI = value;

        public override void OnClick(Card card, CardVizCollection cardVizCollection)
        {
            CardInDeck cardInDeck = DeckLibraryManager.CurrentDeck.GetCard(card);

            if(cardInDeck != null)
            {
                if (cardInDeck.usingAmount >= cardVizCollection.Bought) return;
            }
            else
            {
                //Card unlocked but haven't bought 
                int bought = CollectionMethods.GetBought(card);
                if (bought == 0) return;
            }

            bool successful = DeckLibraryManager.CurrentDeck.Add(card);

            if (successful)
            {   
                cardInDeck = DeckLibraryManager.CurrentDeck.GetCard(card);
                cardVizCollection.SetCard(cardInDeck);
                _childCardUI.LoadCardChild();
                OnClickLogic?.Invoke();
            }

        }
    }
}

