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
    public class CollectionUI : MonoBehaviour
    {
        [SerializeField] CardVizInfoCollection cardInformation;
        [SerializeField] RectTransform content;
        [SerializeField] CardCollectionUI cardPrefab;
        [SerializeField] GetBaseProperties getBaseProperties;

        [Header("Currency")]
        [SerializeField] TextMeshProUGUI coin;
        [SerializeField] TextMeshProUGUI diamond;

        [Header("Button")]
        [SerializeField] Button B_Back;
        [SerializeField] Button B_DisplayCardLocked;
        [SerializeField] Button B_Unlock;
        [SerializeField] Button B_Buy;

        [Header("CheckBox")]
        [SerializeField] GameObject filterPopUp;
        [SerializeField] Toggle checkbox;
        [SerializeField] List<CardCollectionUI> listCardUI;

        private ALLCard cardCollections;
        private Dictionary<bool, List<CardCollectionUI>> dictLocked;
        private CardCollectionUI cardUIselect;

        public void Constructor(ALLCard all)
        {
            cardCollections = all;
        }

        private void Start()
        {
            AddListener();
            DisplayCurrency();
            InitAllCard();
            LoadInfoFirstCardActive();
        }

        private void AddListener()
        {
            B_Back.onClick.AddListener(BackMainMenu);

            checkbox.onValueChanged.AddListener(DisplayFilter);
            B_DisplayCardLocked.onClick.AddListener(DisplayCardLocked);
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
            cardCollections.ReloadUnlock();

            List<Card> sortedList = SortByCost();
            InstantiateCardUI(sortedList);
            InitDictLocked(listCardUI);
        }

        private List<Card> SortByCost()
        {
            List<Card> clone = new List<Card>(cardCollections.allCard);
            clone = clone.OrderBy(x => x.Cost).ToList();
            return clone;
        }

        private void InstantiateCardUI(List<Card> sortedList)
        {
            for (int i = 0; i < sortedList.Count; i++)
            {
                var c = sortedList[i];

                var cardUI = Instantiate(cardPrefab, content);
                cardUI.Init(getBaseProperties, cardInformation);;
                cardUI.OnClickCardCollection += SelectCardCollectionUI;
                cardUI.SetCard(c);
                listCardUI.Add(cardUI);
            }
        }

        private void InitDictLocked(List<CardCollectionUI> listCardUI)
        {
            dictLocked = new Dictionary<bool, List<CardCollectionUI>>
            {
                [true] = new List<CardCollectionUI>(),
                [false] = new List<CardCollectionUI>()
            };

            foreach (var card in listCardUI)
            {
                bool isUnlock = card.IsUnlocked;
                dictLocked[isUnlock].Add(card);
            }
        }
        #endregion

        private void DisplayCardLocked()
        {
            var listCardLocked = dictLocked[false];

            foreach (var cardUI in listCardLocked)
            {
                bool isActive = cardUI.gameObject.activeInHierarchy;
                cardUI.gameObject.SetActive(!isActive);
            }

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

        private void DisplayFilter(bool show)
        {
            filterPopUp.SetActive(!show);
        }

        private void BackMainMenu()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }

        private void SelectCardCollectionUI(CardCollectionUI cardUI)
        {
            CollectionManager.CardSelected = cardUI.GetCard;
            cardUIselect = cardUI;
            SetInteractableButton(cardUI);
        }

        private void SetInteractableButton(CardCollectionUI cardUI)
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
                cardUIselect.SetCard(CollectionManager.CardSelected);

                //Interactable unlock
                SetInteractableButton(cardUIselect);
                DisplayCurrency();
            }
        }

        public void Buy(bool successful)
        {
            if (successful)
            {
                cardUIselect.SetCard(CollectionManager.CardSelected);
                DisplayCurrency();
            }
        }
     

        private void OnDestroy()
        {
            B_Back.onClick.RemoveAllListeners();

            checkbox.onValueChanged.RemoveAllListeners();
            B_DisplayCardLocked.onClick.RemoveAllListeners();
        }

    }
}

