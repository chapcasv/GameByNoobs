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

        public static List<Card> PlayerCardsToCards(List<PlayerCard> playerCards, ALLCard allCards)
        {
            List<Card> listCard = new List<Card>();

            foreach (var card in playerCards)
            {
                Card c = PlayerCardToCard(card, allCards);

                for (int i = 0; i < card.Amount; i++)
                {
                    listCard.Add(c);
                }
            }
            return listCard;
        }

        #endregion

        #region CardInDeck

        public static PlayerCard CardInDeckToPlayerCard(CardInDeck c)
        {
            PlayerCard playerCard = new PlayerCard()
            {
                Amount = c.Amount,
                ID = c.Card.CardID
            };
            return playerCard;
        }


        public static CardInDeck PlayerCardToCardInDeck(PlayerCard playerCard, ALLCard all)
        {
            Card c = PlayerCardToCard(playerCard, all);
            CardInDeck cardInDeck = new CardInDeck(c, playerCard.Amount);
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

