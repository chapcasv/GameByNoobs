using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class CollectionUI : MonoBehaviour
    {
      
        public CardVisual cardPrefab;
        public ALLCard cardCollections;

        private void Start()
        {
            ShowAllCard();
        }
      
        private void ShowAllCard()
        {
            var firstCard = cardCollections.allCard[0];
            cardPrefab.SetCard(cardCollections.allCard[0]);

            for (int i = 1; i < cardCollections.allCard.Count; i++)
            {
                var card = cardCollections.allCard[i];
                var _cardCollection = Instantiate(cardPrefab, cardPrefab.transform.parent);
                _cardCollection.SetCard(cardCollections.allCard[i]);
            }
        }
    }
}

