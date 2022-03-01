using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using PH.Save;

namespace PH
{
    public class CardLibraryUI : CollectionUI
    {
        [SerializeField] CardVizInfoCollection cardInformation;

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
        }

        protected override void Start()
        {
            base.Start();
            DisplayCurrency();
            InitAllCard();
            LoadInfoFirstCardActive();
        }


        #region Currency

        private void DisplayCurrency()
        {
            int coin = SaveSystem.LoadCoin();
            this.coin.text = coin.ToString();
        }

        #endregion

        #region Init all cards in collection
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
                cardUI.Init(_get, _logic);;
                cardUI.SetCard(c);
                listCardUI.Add(cardUI);
            }
        }

        private void InitDictLocked(List<CardVizCollection> listCardUI)
        {
            dictLocked = new Dictionary<bool, List<CardVizCollection>>
            {
                [true] = new List<CardVizCollection>(),
                [false] = new List<CardVizCollection>()
            };

            foreach (var card in listCardUI)
            {
                bool isUnlock = card.IsUnlocked;
                dictLocked[isUnlock].Add(card);
            }
        }
        #endregion

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
            cardUI.OnClick?.Invoke();
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
                dictLocked[false].Remove(cardUIselect);
                dictLocked[true].Add(cardUIselect);

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

