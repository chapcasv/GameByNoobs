using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Data/Player/PlayerSO")]
    public class PlayerSO : ScriptableObject
    {
        [SerializeField] BoolVariable isHaveData;
        [SerializeField] string playerName;
        [SerializeField] IntVariable coin;
        [SerializeField] Deck currentDeck;
        [SerializeField] List<Deck> decks;
        [SerializeField] List<Card> cards;

        public bool IsHaveData { get => isHaveData.value; set => isHaveData.value = value; }
        public Deck CurrentDeck { get => currentDeck; set => currentDeck = value; }
        public string PlayerName { get => playerName; set => playerName = value; }
        public int Coin { get => coin.value; set => coin.value = value; }
        public List<Deck> Decks { get => decks; set => decks = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
    }
}

