using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public static class ConvertCard 
    {
        public static PlayerCard CardToPlayerCard(Card card)
        {
            PlayerCard cardData = new PlayerCard();
            cardData.ID = card.ID;
            return cardData;
        }

        public static Card PlayerCardToCard(PlayerCard cardData, AllCard allCards)
        {
            foreach (Card c in allCards.allCard)
            {
                if (c.ID == cardData.ID) return c;
            }
            return null;
        }
    }
}

