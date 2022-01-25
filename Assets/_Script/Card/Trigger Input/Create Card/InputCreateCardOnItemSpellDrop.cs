using UnityEngine;


namespace PH
{
    [CreateAssetMenu(fileName = "Create Random Card", menuName =
        "ScriptableObject/Card/Trigger Input/Create Card On Item Spell Drop/Random Card")]
    public class InputCreateCardOnItemSpellDrop : InputCreateRandomCard
    {   
        [Range(0, 12)]
        [SerializeField] int cost;
        [SerializeField] CostMode costMode;

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

        public int GetCost => cost;
        public CostMode GetCostMode => costMode;
    }
}

