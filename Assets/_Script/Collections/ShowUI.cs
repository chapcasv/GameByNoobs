using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class ShowUI : MonoBehaviour
    {
        public RectTransform parent;
        public RectTransform cardPrefabs;
        public ALLCard allcards;

        private void Start()
        {
            ShowCard(cardPrefabs, parent);
        }

        private void ShowCard(RectTransform card, RectTransform parent)
        {
            for (int i = 0; i < allcards.allCard.Count; i++)
            {
                var cardUI = Instantiate(card).GetComponent<CardViz>();
                
                cardUI.SelectCard(allcards.allCard[i]);
                cardUI.transform.parent = parent;
              

            }
            Debug.Log("a");
        }
    }
}

