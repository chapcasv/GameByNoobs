using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class DeckLibraryUI : CollectionUI
    {   
        [Header("=== Derived Class Properties ===")]
        [SerializeField] DeckLibraryLogic logic;

        protected override void Start()
        {
            base.Start();
            InitAllCard();
        }


        private void InitAllCard()
        {
            allCards.ReloadUnlock();
            var allCard = allCards.allCard;
            List<Card> sortedList = CollectionExtension.SortByCost(allCard);
            InstantiateCardUI(sortedList);
            InitDictLocked(listCardUI);
        }

        private void InstantiateCardUI(List<Card> sortedList)
        {
            for (int i = 0; i < sortedList.Count; i++)
            {
                var c = sortedList[i];

                var cardUI = Instantiate(cardPrefab, content);
                cardUI.Init(_get, logic);

                cardUI.SetCard(c);
                listCardUI.Add(cardUI);
            }
        }


    }

}
