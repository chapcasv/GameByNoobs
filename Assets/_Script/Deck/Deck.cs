using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "new Deck", menuName = "ScriptableObject/Deck")]
    public class Deck : ScriptableObject
    {
        public string deckName;
        [SerializeField] private List<Card> cardsInDeck;

        public List<Card> CardsInDeck { get => cardsInDeck; }

        public void Clear()
        {
            CardsInDeck.Clear();
        }

        public int AmountCard()
        {
            return CardsInDeck.Count;
        }

        public void Add(Card card)
        {
            CardsInDeck.Add(card);
        }
        public void Remove(Card card)
        {
            CardsInDeck.Remove(card);
        }

        public void RemoveAt(int index)
        {
            CardsInDeck.RemoveAt(index);
        }

        public Card GetCard(int index)
        {
            return CardsInDeck[index];
        }

        public Card FindCard(int id)
        {
            Card result = null;

            foreach (var card in cardsInDeck)
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

