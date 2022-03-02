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
                cardsInDeck = new List<PlayerCard>()
            };

            foreach (var card in cardsInDeck)
            {
                var newPlayerCard = ConvertCard.CardInDeckToPlayerCard(card);
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
                CardInDeck c = ConvertCard.PlayerCardToCardInDeck(playerCard, allCard);
                deck.GetCardInDecks.Add(c);
            }
            return deck;
        }

        public static List<Deck> FormPlayerDecks(List<PlayerDeck> playerDecks, ALLCard allCard)
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

