using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


namespace PH.Player {

    /// <summary>
    /// Initialize player data with default collection & default properties
    /// </summary>

    public static class InitializeNewPlayer
    {
        public static bool CanInitialize(string playerName, PlayerSO player, PlayerDefaultData defaultPlayer)
        {
            if (CanUse(playerName))
            {
                Initialize(playerName, player, defaultPlayer);
                return true;
            }
            else return false;
        }

        private static void Initialize(string playerName, PlayerSO player, PlayerDefaultData defaultPlayer)
        {
            player.PlayerName = playerName;
            player.Coin = defaultPlayer.Coin;
            player.Cards = AddCards(defaultPlayer);
            player.Decks = AddDecks(defaultPlayer);
            player.CurrentDeck = player.Decks[0];

            player.IsHaveData = true;
        }

        private static List<Deck> AddDecks(PlayerDefaultData playerDefault)
        {
            List<Deck> defaultDecks = new List<Deck>();
            foreach (var deck in playerDefault.Decks)
            {
                defaultDecks.Add(deck);
            }
            return defaultDecks;
        }

        private static List<Card> AddCards(PlayerDefaultData playerDefault)
        {
            List<Card> defaultCards = new List<Card>();
            foreach (var card in playerDefault.Cards)
            {
                defaultCards.Add(card);
            }
            return defaultCards;
        }


        private static bool CanUse(string player_name)
        {
            if (player_name != null)
            {
                if (player_name.Length <= 12 && player_name.Length >= 4
                    && !HaveSpace(player_name)
                    && !HaveSpecialCharacter(player_name))
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        private static bool HaveSpecialCharacter(string player_name)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (regexItem.IsMatch(player_name)) return true;
            else return false;
        }

        private static bool HaveSpace(string player_name)
        {
            if (player_name.Contains(" ")) return true;
            else return false;
        }
    }

}
