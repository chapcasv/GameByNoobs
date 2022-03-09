using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace PH.Save
{
    public static class SaveSystem
    {
        private const string MESS_ERROR = "Data Path dont Exists";
        private readonly static string playerDataPath = Application.persistentDataPath + "playerData.json";

        #region Init Player
        public static bool IsHavePlayerData()
        {
            return File.Exists(playerDataPath);
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

        public static void InitPlayer(PlayerLocalSO playerSO)
        {
            PlayerData playerData = new PlayerData
            {
                PlayerName = playerSO.GetPlayerName(),
                Coin = playerSO.Coin,
                Cards = ConvertCard.DefaultCardToPlayerCards(playerSO.Cards),
                Rank = ConvertRank.ToPlayerRank(playerSO.Rank)
            };

            WriteJSon(playerData);

            //Call after init Cards
            InitDeck(playerSO.Decks);
        }

        private static void InitDeck(List<Deck> decks)
        {
            PlayerData playerData = ReadJSon();
            playerData.Decks = ConvertDeck.DecksToPlayerDecks(decks);
            WriteJSon(playerData);
        }

        #endregion

        //ONLY call when you want to save ALL player data
        //01/03/2022 0 references
        public static void SavePlayer(PlayerLocalSO playerSO)
        {
            PlayerData playerData = ReadJSon();
            //playerData = ConvertPlayerSOToPlayerData(playerSO, playerData);
            WriteJSon(playerData);
        }
     

        //ONLY call when you want to load ALL player data
        public static void LoadPlayer(PlayerLocalSO playerSO, ALLCard allCards)
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                LoadPlayerDataToPlayerSO(playerData, playerSO, allCards);
            }
            else throw new Exception(MESS_ERROR);
        }

        private static void LoadPlayerDataToPlayerSO(PlayerData playerData, PlayerLocalSO playerSO, ALLCard allCards)
        {
            playerSO.SetPlayerName(playerData.PlayerName);
            playerSO.Coin = playerData.Coin;
            playerSO.Cards = ConvertCard.PlayerCardsToCards(playerData.Cards, allCards); 
        }

        #region Card

        public static List<PlayerCard> LoadCards()
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                return playerData.Cards;
            }
            else throw new Exception(MESS_ERROR);
        }

        public static void SaveCards(List<PlayerCard> cards)
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                playerData.Cards = cards;
                WriteJSon(playerData);
            }
            else throw new Exception(MESS_ERROR);
        }

        #endregion

        #region Coin
        public static int LoadCoin()
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                return playerData.Coin;
            }
            else throw new Exception(MESS_ERROR);
        }

        public static bool SaveCoin(PlayerLocalSO playerSO, int increaseValue)
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();

                int playerCoin = playerData.Coin + increaseValue;

                if (playerCoin == playerSO.Coin)
                {
                    playerData.Coin = playerCoin;
                    WriteJSon(playerData);
                    return true;
                }
                else throw new Exception("Local data dont match with sever data");
            }
            else throw new Exception(MESS_ERROR);
        }

        #endregion

        #region Diamond

        public static int LoadDiamond()
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                return playerData.Diamond;
            }
            else throw new Exception(MESS_ERROR);
        }

        #endregion

        #region Deck

        public static List<PlayerDeck> LoadDecks()
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                return playerData.Decks;
            }
            else throw new Exception(MESS_ERROR);
        }

        public static void SaveDecks(List<PlayerDeck> playerDecks)
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                playerData.Decks = playerDecks;
                WriteJSon(playerData);
            }
            else throw new Exception(MESS_ERROR);
        }

        public static PlayerDeck LoadCurrentDeck()
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                return playerData.CurrentDeck;
            }
            else throw new Exception(MESS_ERROR);
        }

        public static void SaveCurrentDeck(PlayerDeck deck)
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                playerData.CurrentDeck = deck;
                WriteJSon(playerData);
            }
            else throw new Exception(MESS_ERROR);
        }

        #endregion

        public static string LoadName()
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                return playerData.PlayerName;
            }
            else throw new Exception(MESS_ERROR);
        }

        //0 references 21/02/2022
        public static void SaveName(PlayerLocalSO playerLocalSO)
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                playerData.PlayerName = playerLocalSO.GetPlayerName();
                WriteJSon(playerData);
            }
            else throw new Exception(MESS_ERROR);
        }

        #region Rank

        public static PlayerRank LoadRank()
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                return playerData.Rank;
            }
            else throw new Exception(MESS_ERROR);
        }

        public static void SaveRank(PlayerLocalSO playerLocalSO)
        {
            if (File.Exists(playerDataPath))
            {
                PlayerData playerData = ReadJSon();
                playerData.Rank = ConvertRank.ToPlayerRank(playerLocalSO.Rank);
                WriteJSon(playerData);
            }
            else throw new Exception(MESS_ERROR);
        }

        #endregion

        #region Json

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

        #endregion
    }
}

