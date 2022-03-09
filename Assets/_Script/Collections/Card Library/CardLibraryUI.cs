using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using PH.Save;

namespace PH
{
    public class CardLibraryUI : CollectionUI
    {
        [Header("=== DERIVED CLASS Properties ===")]
        [SerializeField] CardInfoCollection cardInformation;

        [Header("Currency")]
        [SerializeField] TextMeshProUGUI coin;
        [SerializeField] TextMeshProUGUI diamond;

        [Header("Button")]
        [SerializeField] Button B_Unlock;
        [SerializeField] Button B_Buy;

        private CardVizCollection cardUIselect;
        private CardLibraryLogic _logic;

        public void Constructor(CardLibraryLogic logic)
        {
            _logic = logic;
            _logic.Constructor(this, cardInformation);
            cardInformation.Initialized();
        }

        protected override void Start()
        {
            base.Start();
            DisplayCurrency();
            LoadInfoFirstCardActive();
            
        }

        private void DisplayCurrency()
        {
            coin.text = SaveSystem.LoadCoin().ToString();
            diamond.text = SaveSystem.LoadDiamond().ToString();
        }

        protected override void InstantiateCardUI(List<Card> sortedList)
        {
            for (int i = 0; i < sortedList.Count; i++)
            {
                var c = sortedList[i];

                var cardUI = Instantiate(cardPrefab, content);
                cardUI.Init(_get, _logic);;
                cardUI.SetCard(c);
                listCardUI.Add(cardUI);
            }
        }

        protected override void DisplayCardLocked()
        {
            base.DisplayCardLocked();
            //After change display mode,
            //load infomation first card avtive in list card UI
            LoadInfoFirstCardActive();
        }

        private void LoadInfoFirstCardActive()
        {
            var cardUI = listCardUI.Where(c => c.gameObject.activeInHierarchy).FirstOrDefault();
            cardUI.OnClickCallBack();
            SelectCardCollectionUI(cardUI);
        }

        public void SelectCardCollectionUI(CardVizCollection cardUI)
        {
            CardLibraryManager.CardSelected = cardUI.GetCard;
            cardUIselect = cardUI;
            SetInteractableButton(cardUI);
        }

        private void SetInteractableButton(CardVizCollection cardUI)
        {
            B_Unlock.interactable = !cardUI.IsUnlocked;
            B_Buy.interactable = cardUI.IsUnlocked;
        }

        public void Unlock(bool successful)
        {
            if (successful)
            {
                dictUnlocked[false].Remove(cardUIselect);
                dictUnlocked[true].Add(cardUIselect);

                //Reload visual after unlock
                cardUIselect.SetCard(CardLibraryManager.CardSelected);

                //Interactable unlock
                SetInteractableButton(cardUIselect);
                DisplayCurrency();
            }
        }

        public void Buy(bool successful)
        {
            if (successful)
            {
                cardUIselect.SetCard(CardLibraryManager.CardSelected);
                DisplayCurrency();
            }
        }
    }
}

