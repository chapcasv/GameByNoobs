using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName ="Create", menuName = "ScriptableObject/Card/Trigger On Board/Input/Create Card/Chosen Card")]

    public class InputCreateChosenCard : InputCreateCard
    {
        [SerializeField] Card[] cards;

        public override Card[] GetCard()
        {
            Card[] cardArray = new Card[cards.Length];

            for (int i = 0; i < cardArray.Length; i++)
            {
                cardArray[i] = cards[i];
            }
            return cardArray;
        }
    }
}


