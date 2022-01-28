using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Create Random Card", 
        menuName = "ScriptableObject/Card/Trigger Input/Create Card/Random Card/Random")]
    public class InputCreateRandomCard : InputCreateCard
    {
        [SerializeField] protected int numberCardCreate = 1;

        public override Card[] GetCard()
        {
            Card[] cardArray = new Card[numberCardCreate];

            for (int i = 0; i < cardArray.Length; i++)
            {
                int randomIndex = Random.Range(0, cards.Length);

                cardArray[i] = cards[randomIndex];
            }
            return cardArray;
        }
    }
}

