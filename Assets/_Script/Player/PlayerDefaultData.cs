using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    [CreateAssetMenu(menuName = "Data/Player/DefaultData")]
    public class PlayerDefaultData : ScriptableObject
    {
        public int Coin;
        public List<Deck> Decks;
        public List<Card> Cards;
    }
}

