using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(fileName ="new Deck",menuName = "ScriptableObject/Deck")]
    public class Deck :ScriptableObject
    {
        public string deckName;
        public List<Card> cardsInDeck;
    }
}

