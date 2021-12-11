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
            cardData.ID = card.CardID;
            return cardData;
        }

        public static Card PlayerCardToCard(PlayerCard cardData, ALLCard allCards)
        {
            foreach (Card c in allCards.allCard)
            {
                if (c.CardID == cardData.ID) return c;
            }
            return null;
        }
    }
}

