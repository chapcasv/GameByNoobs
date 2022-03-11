using PH.Save;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Data/Player/Player Local SO")]
    public class PlayerLocalSO : ScriptableObject
    {   
        //Use for debug
        [SerializeField] string playerName;
        [SerializeField] int coin;
        [SerializeField] List<Card> cards;
        [SerializeField] ALLCard allCard;
        [SerializeField] Sprite avatar;
        [SerializeField] int diamond;

        private Deck currentDeck;
        private List<Deck> decks;

        private Rank rank;

        public Sprite GetAvatar => avatar;

        public string GetPlayerName()
        {
            if(playerName != null)
            {
                return playerName;
            }
            else
            {
                return SaveSystem.LoadName();
            }
        }

        public void SetPlayerName(string value)
        {
            playerName = value;
        }

        public int Coin { get => coin; set => coin = value; }

        public bool SaveCoinData(int increaseValue)
        {
            bool result = SaveSystem.SaveCoin(this,increaseValue);
            return result;
        }

        public List<Deck> Decks { get => decks; set => decks = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
        public Rank Rank { get => rank; set => rank = value; }
        public int Diamond { get => diamond; set => diamond = value; }

        public Deck GetCurrentDeck()
        {
            ReloadDecks();
            //Need Fix
            currentDeck = decks[0];
            currentDeck.ReloadListCard();
            return currentDeck;
        }

        public void SetCurrentDeck(Deck value) => currentDeck = value;

        public void ReloadDecks()
        {
            var playerDecks = SaveSystem.LoadDecks();

            decks = ConvertDeck.PlayerDecksToDecks(playerDecks, allCard);
        }
    }
}

