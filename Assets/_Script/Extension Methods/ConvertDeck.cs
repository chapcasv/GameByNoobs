using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class ConvertDeck 
    {
        public static PlayerDeck DeckToPlayerDeck(Deck deck)
        {
            var cardsInDeck = deck.GetCardInDecks;

            PlayerDeck playerDeck = new PlayerDeck()
            {
                deckName = deck.deckName,
                cardsInDeck = new List<PlayerCardInDeck>()
            };

            foreach (var card in cardsInDeck)
            {
                var newPlayerCard = ConvertCard.CardInDeckToPlayerCardInDeck(card);
                playerDeck.cardsInDeck.Add(newPlayerCard);
            }

            return playerDeck;
        }

        public static List<PlayerDeck> DecksToPlayerDecks(List<Deck> decks)
        {
            List<PlayerDeck> playerDecks = new List<PlayerDeck>();

            foreach (var deck in decks)
            {
                var playerDeck = DeckToPlayerDeck(deck);
                playerDecks.Add(playerDeck);
            }
            return playerDecks;
        }

        public static Deck PlayerDeckToDeck(PlayerDeck playerDeck,ALLCard allCard)
        {
            var pCards = playerDeck.cardsInDeck;

            Deck deck = ScriptableObject.CreateInstance<Deck>();
            deck.NewDeck(playerDeck.deckName);

            foreach (var playerCard in pCards)
            {
                CardInDeck c = ConvertCard.PCardInDeckToCardInDeck(playerCard, allCard);
                deck.GetCardInDecks.Add(c);
            }
            return deck;
        }

        public static List<Deck> PlayerDecksToDecks(List<PlayerDeck> playerDecks, ALLCard allCard)
        {
            List<Deck> decks = new List<Deck>();

            foreach (var playerDeck in playerDecks)
            {
                Deck newDeck = PlayerDeckToDeck(playerDeck, allCard);
                decks.Add(newDeck);
            }
            return decks;
        }
    }
}

