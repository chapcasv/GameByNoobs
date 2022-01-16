using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Create Random Card", menuName = "ScriptableObject/Card/Trigger On Board/Input/Create Card/Random Card")]
    public class InputCreateRandomCard : InputCreateCard
    {
        [SerializeField] Card[] cards;

        [SerializeField] int numberCardCreate = 1;

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

