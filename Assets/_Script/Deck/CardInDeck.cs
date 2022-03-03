using UnityEngine;

namespace PH
{
    [System.Serializable]
    public class CardInDeck 
    {
        [SerializeField] string devDiscription;
        public Card Card;
        [Range(1,3)]
        public int usingAmount = 1;

        public CardInDeck(Card c)
        {
            Card = c;
            usingAmount = 1;
        }

        public CardInDeck(Card c, int amount)
        {
            Card = c;
            usingAmount = amount;
        }
    }
}


