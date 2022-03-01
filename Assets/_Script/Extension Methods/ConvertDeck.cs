using System.Collections.Generic;

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
    }
}

