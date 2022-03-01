using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class DeckLibraryUI : CollectionUI
    {
        [Header("=== DERIVED CLASS Properties ===")]
        [SerializeField] DeckLibraryLogic logic;
        [SerializeField] Button B_Setup;
        [SerializeField] GameObject deck;

        protected override void Start()
        {
            base.Start();
        }

        protected override void AddListener()
        {
            base.AddListener();
            B_Setup.onClick.AddListener(SetUpDeckCallBack);
        }

        private void SetUpDeckCallBack()
        {
            deck.SetActive(false);
        }

        protected override void InstantiateCardUI(List<Card> sortedList)
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

        protected override void RemoveListener()
        {
            base.RemoveListener();
            B_Setup.onClick.RemoveAllListeners();
        }
    }

}
