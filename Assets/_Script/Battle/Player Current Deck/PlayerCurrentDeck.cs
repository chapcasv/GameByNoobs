using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/PlayerCurrentDeck")]
    public class PlayerCurrentDeck : ScriptableObject
    {
        [SerializeField] Deck deckBeforeShuffle;
        [SerializeField] Deck deckAfterShuffle;

        private Deck _currentDeck;

        [System.NonSerialized]
        private bool _isInit = false;
        public Card DrawCard()
        {
            Card card = _currentDeck.GetCard(0);
            _currentDeck.Remove(card);
            return card;
        }

        public Card Replace(Card cardWantReplace)
        {
            _currentDeck.Add(cardWantReplace);
            int indexRandom = Random.Range(0, _currentDeck.AmountCard());
            Card newCard = _currentDeck.GetCard(indexRandom);
            _currentDeck.RemoveAt(indexRandom);
            return newCard;
        }

        public void InitializePlayerDeck(Deck currentDeck)
        {
            if (_isInit) return;

            CoplyPlayerCurrentDeck(currentDeck);
            Shuffle();
            _currentDeck = deckAfterShuffle;

            _isInit = true;
        }

        private void CoplyPlayerCurrentDeck( Deck currentDeck)
        {
            Deck deckDefault = currentDeck;
            
            foreach (var card in deckDefault.CardsInDeck)
            {
                deckBeforeShuffle.Add(card);
            }
        }

        private void Shuffle()
        {
            deckAfterShuffle.Clear();
            int loopCount = deckBeforeShuffle.AmountCard();

            for (int i = 0; i < loopCount; i++)
            {
                int randomIndexCard = Random.Range(0, deckBeforeShuffle.AmountCard());
                deckAfterShuffle.Add(deckBeforeShuffle.GetCard(randomIndexCard));
                deckBeforeShuffle.RemoveAt(randomIndexCard);
            }
        }
    }
}

