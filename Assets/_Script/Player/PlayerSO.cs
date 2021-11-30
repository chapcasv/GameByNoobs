using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName ="Data/Player")]
    public class PlayerSO : ScriptableObject
    {
        private bool isHaveData = false;
        private Deck currentDeck;

        public PlayerProperties[] properties;

        public List<Deck> decks; 

        public bool IsHaveData { get => isHaveData; set => isHaveData = value; }
        public Deck CurrentDeck { get => currentDeck; set => currentDeck = value; }
    }
}

