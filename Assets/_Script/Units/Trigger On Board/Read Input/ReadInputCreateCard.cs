using UnityEngine;


namespace PH
{   
    [CreateAssetMenu(fileName ="Read", menuName = "ScriptableObject/Card/Trigger On Board/Read Input/Create Card")]
    public class ReadInputCreateCard : TriggerOnBoardReadInput
    {
        [SerializeField] DeckSystem deckSystem;

        public override void Read(TriggerOnBoardInput input)
        {
            InputCreateCard inputCreateCard = (InputCreateCard)input;

            Card[] cardArray = inputCreateCard.GetCard();

            for (int i = 0; i < cardArray.Length; i++)
            {
                deckSystem.AddCardToHand(cardArray[i]);
            }
        }
    }
}

