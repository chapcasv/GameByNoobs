using UnityEngine;

namespace PH
{
    [System.Serializable]
    public class CardInDeck 
    {
        [SerializeField] string developmentDiscription;
        public Card Card;
        [Range(1,3)]
        public int Amount;

        public CardInDeck(Card c)
        {
            Card = c;
            Amount = 1;
        }

        public CardInDeck(Card c, int amount)
        {
            Card = c;
            Amount = amount;
        }
    }
}


