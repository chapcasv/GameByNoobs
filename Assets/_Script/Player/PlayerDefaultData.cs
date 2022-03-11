using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace PH
{   

    //Only use when InitializeNewPlayer
    [CreateAssetMenu(menuName = "ScriptableObject/Data/Player/DefaultData")]
    public class PlayerDefaultData : ScriptableObject
    {
        public int Diamond;
        public int Coin;
        public List<Deck> Decks;
        public List<Card> Cards;
    }
}

