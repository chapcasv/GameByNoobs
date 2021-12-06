using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Data/Player/PlayerSO")]
    public class PlayerSO : ScriptableObject
    {
        [SerializeField] string playerName;
        [SerializeField] int coin;
        [SerializeField] Deck currentDeck;
        [SerializeField] List<Deck> decks;
        [SerializeField] List<Card> cards;

        public string PlayerName { get => playerName; set => playerName = value; }
        public int Coin { get => coin; set => coin = value; }
        public Deck CurrentDeck { get => currentDeck; set => currentDeck = value; }
        public List<Deck> Decks { get => decks; set => decks = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
    }
}

