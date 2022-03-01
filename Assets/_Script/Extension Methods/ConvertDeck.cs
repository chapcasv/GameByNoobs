using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class ConvertDeck 
    {
        public static PlayerDeck ToPlayerDeck(Deck deck)
        {
            var cards = deck.CardsInDeck;
            PlayerDeck playerDeck = new PlayerDeck()
            {
                deckName = deck.deckName,
                cardsInDeck = new List<PlayerCard>()
            };

            foreach (var card in cards)
            {
                var newCard = ConvertCard.ToPlayerCard(card);
                ConvertCard.AddPlayerCardInDeck(ref playerDeck, newCard);
            }

            return playerDeck;
        }

        public static List<PlayerDeck> ToPlayerDecks(List<Deck> decks)
        {
            List<PlayerDeck> playerDecks = new List<PlayerDeck>();

            foreach (var deck in decks)
            {
                var playerDeck = ToPlayerDeck(deck);
                playerDecks.Add(playerDeck);
            }
            return playerDecks;
        }

        public static Deck FormPlayerDeck(PlayerDeck playerDeck,ALLCard allCard)
        {
            var playerCards = playerDeck.cardsInDeck;

            Deck deck = ScriptableObject.CreateInstance<Deck>();
            deck.NewDeck(playerDeck.deckName);

            foreach (var playerCard in playerCards)
            {
                Card card = ConvertCard.PlayerCardToCard(playerCard, allCard);
                deck.Add(card);
            }
            return deck;
        }

        public static List<Deck> FormPlayerDecks(List<PlayerDeck> playerDecks, ALLCard allCard)
        {
            List<Deck> decks = new List<Deck>();

            foreach (var playerDeck in playerDecks)
            {
                Deck newDeck = FormPlayerDeck(playerDeck, allCard);
                decks.Add(newDeck);
            }
            return decks;
        }
    }
}

