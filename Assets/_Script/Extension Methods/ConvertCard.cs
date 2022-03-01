using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{
    public static class ConvertCard 
    {
        private static readonly int MAX_AMOUNT = 3;

        #region Card => Player Card
        public static PlayerCard ToPlayerCard(Card card)
        {
            PlayerCard cardData = new PlayerCard
            {
                ID = card.CardID
            };
            return cardData;
        }

        //01/03/2022 0 references
        public static List<PlayerCard> ToPlayerCards(List<Card> cards, List<PlayerCard> playerCards)
        {
            foreach (var card in cards)
            {
                PlayerCard newCard = ToPlayerCard(card);
                AddPlayerCard(ref playerCards, newCard);
            }
            return playerCards;
        }

        #endregion

        #region Player Card => Card
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

            foreach (var playerCard in playerCards)
            {
                Card c = PlayerCardToCard(playerCard, allCards);

                for (int i = 0; i < playerCard.Amount; i++)
                {
                    listCard.Add(c);
                }
            }
            return listCard;
        }

        #endregion

        #region Add Card

        public static void AddPlayerCardInDeck(ref PlayerDeck deck, PlayerCard newCard)
        {
            foreach (var playerCard in deck.cardsInDeck)
            {
                if(playerCard.ID == newCard.ID)
                {
                    if(playerCard.Amount < MAX_AMOUNT)
                    {
                        playerCard.Amount++;
                        return;
                    }
                }
            }
            newCard.Amount = 1;
            deck.cardsInDeck.Add(newCard);
        }

        public static bool AddPlayerCard(ref List<PlayerCard> playerCards, PlayerCard newCard) 
        {
            foreach (var playerCard in playerCards)
            {
                if(playerCard.ID == newCard.ID)
                {
                    if (playerCard.Amount < MAX_AMOUNT)
                    {
                        playerCard.Amount++;
                        return true;
                    }
                    else return false;
                }
            }
            throw new Exception("Data cards unlocked dont have new card");
        }

        #endregion

        public static List<PlayerCard> DefaultCardToPlayerCards(List<Card> cards)
        {
            List<PlayerCard> playerCards = new List<PlayerCard>();

            //Set unlock
            foreach (var card in cards)
            {
                var newCard = ToPlayerCard(card);
                playerCards.Add(newCard);
            }

            //Set amount after unlocked
            foreach (var card in cards)
            {
                PlayerCard newCard = ToPlayerCard(card);
                AddPlayerCard(ref playerCards, newCard);
            }
            return playerCards;
        }
       
    }
}

