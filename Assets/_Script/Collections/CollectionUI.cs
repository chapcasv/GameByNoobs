using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class CollectionUI : MonoBehaviour
    {
        public RectTransform parent;
        public GameObject cardPrefabs;
        public ALLCard allcards;

        private void Start()
        {
            ShowCard(cardPrefabs, parent);
        }

        private void ShowCard(GameObject card, RectTransform parent)
        {
            for (int i = 0; i < allcards.allCard.Count; i++)
            {
                var cardUI = Instantiate(card,parent);
                var cardViz = cardUI.GetComponent<CardVisual>();
                cardViz.SetCard(allcards.allCard[i]);
            }
        }
    }
}

