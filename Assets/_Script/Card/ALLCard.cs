using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Card/ALLCard")]
    public class ALLCard : ScriptableObject
    {
        public List<Card> allCard;

        public Card GetCard(int id)
        {
            foreach (var card in allCard)
            {
                if (card.CardID == id)
                    return card;
            }

            throw new System.Exception("Dont have id in data");
        }
    }
}

