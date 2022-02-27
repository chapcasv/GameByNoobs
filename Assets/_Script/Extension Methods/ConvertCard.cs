using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{
    public static class ConvertCard 
    {
        public static PlayerCard ToPlayerCard(Card card)
        {
            PlayerCard cardData = new PlayerCard
            {
                ID = card.CardID
            };
            return cardData;
        }

        public static Card PlayerCardToCard(PlayerCard cardData, ALLCard allCards)
        {
            foreach (Card c in allCards.allCard)
            {
                if (c.CardID == cardData.ID) return c;
            }
            throw new Exception("Cant convert player card to Card !!!");
        }

        public static List<Card> PlayerCardsToCards(List<PlayerCard> cards, ALLCard allCards)
        {
            List<Card> listCard = new List<Card>();

            foreach (var card in cards)
            {
                Card c = PlayerCardToCard(card, allCards);
                listCard.Add(c);
            }

            return listCard;
        }
    }
}

