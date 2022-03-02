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
                AddStructCardToListCard();
            }
            else
            {
                listCard = new List<Card>();
                AddStructCardToListCard();
            }
        }

        private void AddStructCardToListCard()
        {
            foreach (var card in cardsInDeck)
            {
                for (int i = 0; i < card.Amount; i++)
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
            this.deckName = deckName;
        }

        public void ClearListCard()
        {
            if (listCard != null)
            {
                listCard.Clear();
            }
            else throw new Exception(ErrorMessage);
        }

        public int AmountCard()
        {
            if(listCard != null)
            {
                return listCard.Count;
            }
            else
            {
                ReloadListCard();
                return listCard.Count;
            }
        }

        public void Add(Card card)
        {
            foreach (var c in cardsInDeck)
            {
                if(c.Card.CardID == card.CardID)
                {
                    if (c.Amount < GameConst.MAX_AMOUNT_CARD_INSTANCE)
                    {
                        c.Amount++;
                        if (listCard != null)
                        {
                            listCard.Add(card);
                        }
                        return;
                    }
                    else return;
                }
            }

            var newCard = new CardInDeck(card);
            cardsInDeck.Add(newCard);
            listCard.Add(card);
        }

        public void Remove(Card card)
        {
            CardInDeck cardRemove = null;

            foreach (var c in cardsInDeck)
            {
                if(c.Card.CardID == card.CardID)
                {
                    if(c.Amount > 1)
                    {
                        c.Amount--;
                        listCard.Remove(card);
                        break;
                    }
                    else if(c.Amount == 1)
                    {
                        c.Amount = 0;
                        cardRemove = c;
                        listCard.Remove(card);
                        break;
                    }
                }
            }

            if(cardRemove != null)
            {
                cardsInDeck.Remove(cardRemove);
                Debug.Log("Remove");
            }

        }

        private void RemoveCardsInDeck(Card card)
        {
            CardInDeck cardRemove = null;

            foreach (var c in cardsInDeck)
            {
                if (c.Card.CardID == card.CardID)
                {
                    if (c.Amount > 1)
                    {
                        c.Amount--;
                        break;
                    }
                    else if (c.Amount == 1)
                    {
                        c.Amount = 0;
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

