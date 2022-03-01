using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.Save;

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

        /// <summary>
        /// Set propertie unlock by data
        /// </summary>
        public void ReloadUnlock()
        {
            foreach (var card in allCard)
            {
                card.Unlocked = false;
            }

            var playerCards = SaveSystem.LoadCards();
            foreach (var playerCard in playerCards)
            {
                Card c = GetCard(playerCard.ID);
                c.Unlocked = true;
            }
           
        }
    }
}

