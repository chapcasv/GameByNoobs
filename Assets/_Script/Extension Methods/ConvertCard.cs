using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{
    public static class ConvertCard
    {
        #region Card

        public static PlayerCard CardToPlayerCard(Card card)
        {
            return new PlayerCard { ID = card.CardID };
        }

        public static Card ConvertByID(int ID, ALLCard allCards)
        {
            foreach (Card c in allCards.allCard)
            {
                if (c.CardID == ID) return c;
            }
            throw new Exception("Cant convert player card to Card !!!");
        }

        public static List<Card> PlayerCardsToCards(List<PlayerCard> playerCards, ALLCard allCards)
        {
            List<Card> listCard = new List<Card>();

            foreach (var card in playerCards)
            {
                Card c = ConvertByID(card.ID, allCards);

                for (int i = 0; i < card.Bought; i++)
                {
                    listCard.Add(c);
                }
            }
            return listCard;
        }

        #endregion

        #region CardInDeck

        public static PlayerCardInDeck CardInDeckToPlayerCardInDeck(CardInDeck c)
        {
            PlayerCardInDeck playerCard = new PlayerCardInDeck()
            {
                usingAmount = c.usingAmount,
                ID = c.Card.CardID
            };
            return playerCard;
        }

        public static CardInDeck PCardInDeckToCardInDeck(PlayerCardInDeck playerCard, ALLCard all)
        {
            Card c = ConvertByID(playerCard.ID, all);
            CardInDeck cardInDeck = new CardInDeck(c, playerCard.usingAmount);
            return cardInDeck;
        }

        #endregion


        public static List<PlayerCard> DefaultCardToPlayerCards(List<Card> cards)
        {
            List<PlayerCard> playerCards = new List<PlayerCard>();

            //Set unlock
            foreach (var card in cards)
            {
                var newCard = CardToPlayerCard(card);
                playerCards.Add(newCard);
            }

            //Set amount after unlocked
            foreach (var card in cards)
            {
                PlayerCard newCard = CardToPlayerCard(card);
                CollectionMethods.AddPlayerCard(ref playerCards, newCard);
            }
            return playerCards;
        }

    }
}

