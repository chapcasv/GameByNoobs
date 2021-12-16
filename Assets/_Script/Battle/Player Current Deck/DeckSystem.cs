using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Deck System")]
    public class DeckSystem : ScriptableObject
    {
        public event Action OnDrawCard;
        public event Action OnDropCard;

        [SerializeField] PlayerSO data;
        [SerializeField] Deck deckBeforeShuffle;
        [SerializeField] Deck deckAfterShuffle;
        [NonSerialized] private Deck _currentDeck;
        public List<Card> CardsInHand { get; private set; }


        public void DrawStartCard() 
        {
            Card card = _currentDeck.GetCard(0);
            _currentDeck.Remove(card);
            CardsInHand.Add(card);
        } 

        public void DrawCard()
        {
            Card card = _currentDeck.GetCard(0);
            _currentDeck.Remove(card);
            CardsInHand.Add(card);

            OnDrawCard?.Invoke();
        }

        public void DropCard(Card card)
        {
            CardsInHand.Remove(card);
            OnDropCard?.Invoke();
        }



        public void ReplaceCardHand(int index)
        {
            Card cardReplace = CardsInHand[index];
            Card newCard = Replace(cardReplace);
            CardsInHand[index] = newCard;
        }

        public Card Replace(Card cardWantReplace)
        {
            _currentDeck.Add(cardWantReplace);
            int indexRandom = UnityEngine.Random.Range(0, _currentDeck.AmountCard());
            Card newCard = _currentDeck.GetCard(indexRandom);
            _currentDeck.RemoveAt(indexRandom);
            return newCard;
        }

        public void InitializePlayerDeck()
        {

            CoplyPlayerCurrentDeck();
            Shuffle();
            _currentDeck = deckAfterShuffle;
            CardsInHand = new List<Card>();
        }

        private void CoplyPlayerCurrentDeck()
        {
            Deck deckDefault = data.CurrentDeck;
            
            foreach (var card in deckDefault.CardsInDeck)
            {
                deckBeforeShuffle.Add(card);
            }
        }

        private void Shuffle()
        {
            deckAfterShuffle.Clear();
            int deckCount = deckBeforeShuffle.AmountCard();

            for (int i = 0; i < deckCount; i++)
            {
                int randomIndexCard = UnityEngine.Random.Range(0, deckBeforeShuffle.AmountCard());
                deckAfterShuffle.Add(deckBeforeShuffle.GetCard(randomIndexCard));
                deckBeforeShuffle.RemoveAt(randomIndexCard);
            }
        }
    }
}

