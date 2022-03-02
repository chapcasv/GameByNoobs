using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using PH.Save;

namespace PH {

    /// <summary>
    /// Initialize player data with default collection & default properties
    /// </summary>

    public static class InitializeNewPlayer
    {
        public static bool CanInitialize(string playerName, PlayerLocalSO player, PlayerDefaultData defaultPlayer, ALLCard allCards)
        {
            if (CanUse(playerName))
            {
                Initialize(playerName, player, defaultPlayer, allCards);
                return true;
            }
            else return false;
        }

        private static void Initialize(string playerName, PlayerLocalSO playerSO, PlayerDefaultData defaultPlayer, ALLCard allCards)
        {
            playerSO.SetPlayerName(playerName);

            playerSO.Coin = defaultPlayer.Coin;
            playerSO.Cards = new List<Card>(defaultPlayer.Cards);
            playerSO.Decks = new List<Deck>(defaultPlayer.Decks);
            playerSO.SetCurrentDeck(playerSO.Decks[0]);
            playerSO.Rank = new Rank();

            SaveSystem.InitPlayer(playerSO);
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
