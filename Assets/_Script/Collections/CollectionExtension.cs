using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PH
{
    public static class CollectionExtension 
    {
        public static List<Card> SortByCost(List<Card> allCard)
        {
            List<Card> clone = new List<Card>(allCard);
            clone = clone.OrderBy(x => x.Cost).ToList();
            return clone;
        }

        public static void DisplayCardLocked(List<CardVizCollection> listCardLocked)
        {
            foreach (var cardUI in listCardLocked)
            {
                bool isActive = cardUI.gameObject.activeInHierarchy;
                cardUI.gameObject.SetActive(!isActive);
            }
        }
    }
}

