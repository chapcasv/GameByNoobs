using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Data/Player/DefaultData")]
    public class PlayerDefaultData : ScriptableObject
    {
        public BoolVariable IsHaveData;
        public IntVariable Coin;
        public List<Deck> Decks;
        public List<Card> Cards;
    }
}

