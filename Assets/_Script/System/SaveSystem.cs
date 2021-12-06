using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace PH.Save
{

    public static class SaveSystem
    {
        private readonly static string playerDataPath = Application.dataPath + "playerData.json";

        public static bool IsHavePlayerData()
        {
            if (File.Exists(playerDataPath)) return true;
            else return false;
        }

        public static bool RemovePlayerData()
        {
            if (File.Exists(playerDataPath))
            {
                File.Delete(playerDataPath);
                return true;
            }
            else return false;
        }

        //ONLY call when you want to save ALL player data
        public static void SavePlayer(PlayerSO playerSO)
        {
            PlayerData data = ConvertPlayerSOToPlayerData(playerSO);
            WriteJSon(data);
        }

        private static PlayerData ConvertPlayerSOToPlayerData(PlayerSO playerSO)
        {
            PlayerData playerData = new PlayerData();
            playerData.PlayerName = playerSO.PlayerName;
            playerData.Coin = playerSO.Coin;
            playerData.Cards = ConvertCardsToPlayerCards(playerSO.Cards);
            playerData.Decks = ConvertDecksToPlayerDecks(playerSO.Decks);
            return playerData;
        }

        private static List<PlayerDeck> ConvertDecksToPlayerDecks(List<Deck> decks)
        {
            List<PlayerDeck> playerDecks = new List<PlayerDeck>();

            foreach (var deck in decks)
            {
                PlayerDeck playerDeck = new PlayerDeck();

            }

            return playerDecks;
        }

        private static List<PlayerCard> ConvertCardsToPlayerCards(List<Card> Cards)
        {
            List<PlayerCard> playerCards = new List<PlayerCard>();

            foreach (var c in Cards)
            {
                PlayerCard cardData = ConvertCard.CardToPlayerCard(c);
                playerCards.Add(cardData);
            }
            return playerCards;
        }

        //ONLY call when you want to load ALL player data
        public static void LoadPlayer(PlayerSO playerSO, AllCard allCards)
        {
            PlayerData playerData = new PlayerData();
            if (File.Exists(playerDataPath))
            {
                playerData = ReadJSon();
                LoadPlayerDataToPlayerSO(playerData, playerSO, allCards);
            }
            else throw new System.Exception("Data Path dont Exists");
        }

        private static void LoadPlayerDataToPlayerSO(PlayerData playerData, PlayerSO playerSO, AllCard allCards)
        {
            playerSO.PlayerName = playerData.PlayerName;
            playerSO.Coin = playerData.Coin;
            playerSO.Cards = ConverPlayerCardsToCards(playerData.Cards, allCards);
        }

        private static List<Card> ConverPlayerCardsToCards(List<PlayerCard> playerCards, AllCard allCards)
        {
            List<Card> cards = new List<Card>();

            foreach (var c in playerCards)
            {
                cards.Add(ConvertCard.PlayerCardToCard(c, allCards));
            }
            return cards;
        }

        public static void SavePlayerCoin(PlayerSO playerSO)
        {
            PlayerData playerData = new PlayerData();
            playerData.Coin = playerSO.Coin;
            WriteJSon(playerData);
        }

        private static void WriteJSon(PlayerData playerData)
        {
            string jsonData = JsonUtility.ToJson(playerData);
            File.WriteAllText(playerDataPath, jsonData);
        }

        private static PlayerData ReadJSon()
        {
            string jsonData = File.ReadAllText(playerDataPath);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
    }
}

