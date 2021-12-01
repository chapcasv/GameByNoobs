using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName ="Data/Player")]
    public class PlayerSO : ScriptableObject
    {
        [SerializeField] bool isHaveData = false;
        private string playerName;
        private int coin;
        [SerializeField] Deck currentDeck;
        [SerializeField] List<Deck> decks;
        [SerializeField] List<Card> cards;

        public bool IsHaveData { get => isHaveData; set => isHaveData = value; }
        public Deck CurrentDeck { get => currentDeck; set => currentDeck = value; }
        public string PlayerName { get => playerName; set => playerName = value; }
        public int Coin { get => coin; set => coin = value; }
        public List<Deck> Decks { get => decks; set => decks = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
    }
}

