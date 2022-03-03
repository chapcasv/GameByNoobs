using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{
    [CreateAssetMenu(fileName = "new Deck", menuName = "ScriptableObject/Deck")]
    public class Deck : ScriptableObject
    {
        public string deckName;
        [SerializeField] List<CardInDeck> cardsInDeck;

        private const string ErrorMessage = "List card is null. Need to reload list card";
        private List<Card> listCard;

        public List<CardInDeck> GetCardInDecks => cardsInDeck;
        public void SetCardInDecks(List<CardInDeck> value) => cardsInDeck = value;

        public void ReloadListCard()
        {
            if(listCard != null)
            {
                listCard.Clear();
                CardsInDeckToListCard();
            }
            else
            {
                listCard = new List<Card>();
                CardsInDeckToListCard();
            }
        }

        private void CardsInDeckToListCard()
        {
            foreach (var card in cardsInDeck)
            {
                for (int i = 0; i < card.usingAmount; i++)
                {
                    listCard.Add(card.Card);
                }
            }
        }


        public List<Card> GetListCard()
        {
            if (listCard != null)
            {
                return listCard;
            }
            else throw new Exception(ErrorMessage);
        }

        public void NewDeck(string deckName)
        {
            cardsInDeck = new List<CardInDeck>();
            listCard = new List<Card>();
            this.deckName = deckName;
        }

        public int AmountCard()
        {
            if(listCard != null)
            {
                return listCard.Count;
            }
            else throw new Exception(ErrorMessage);
        }

        public bool Add(Card card)
        {
            foreach (var c in cardsInDeck)
            {
                if(c.Card.CardID == card.CardID)
                {
                    if (c.usingAmount < GameConst.MAX_USE_AMOUNT)
                    {
                        c.usingAmount++;
                        if (listCard != null)
                        {
                            listCard.Add(card);
                        }
                        return true;
                    }
                    else return false; 
                }
            }

            var newCard = new CardInDeck(card);
            cardsInDeck.Add(newCard);
            listCard.Add(card);
            return true;
        }
        
        public void Remove(Card card)
        {
            CardInDeck cardRemove = null;

            foreach (var c in cardsInDeck)
            {
                if(c.Card.CardID == card.CardID)
                {
                    if(c.usingAmount > 1)
                    {
                        c.usingAmount--;
                        listCard.Remove(card);
                        break;
                    }
                    else if(c.usingAmount == 1)
                    {
                        c.usingAmount = 0;
                        cardRemove = c;
                        listCard.Remove(card);
                        break;
                    }
                }
            }

            if(cardRemove != null)
            {
                cardsInDeck.Remove(cardRemove);
            }

        }

        private void RemoveCardsInDeck(Card card)
        {
            CardInDeck cardRemove = null;

            foreach (var c in cardsInDeck)
            {
                if (c.Card.CardID == card.CardID)
                {
                    if (c.usingAmount > 1)
                    {
                        c.usingAmount--;
                        break;
                    }
                    else if (c.usingAmount == 1)
                    {
                        c.usingAmount = 0;
                        cardRemove = c;
                        break;
                    }
                }
            }

            if (cardRemove != null)
            {
                cardsInDeck.Remove(cardRemove);
            }
        }

        public void RemoveAt(int index)
        {
            if (listCard != null)
            {
                Card c = listCard[index];
                listCard.RemoveAt(index);
                RemoveCardsInDeck(c);
            }
            else throw new Exception(ErrorMessage);
        }

        public Card GetCard(int index)
        {
            if (listCard != null)
            {
                return listCard[index];
            }
            else throw new Exception(ErrorMessage);
        }

        public CardInDeck GetCard(Card c)
        {
            CardInDeck result = null;

            foreach (var cardInDeck in cardsInDeck)
            {
                if(cardInDeck.Card.CardID == c.CardID)
                {
                    return result = cardInDeck;
                }
            }
            return result;
        }

        public Card FindCard(int id)
        {
            Card result = null;

            foreach (var card in listCard)
            {
                if(card.CardID == id)
                {
                    result = card;
                    break;
                }
            }

            return result;
        }
    }
}

