using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class ShowChildCard : MonoBehaviour
    {
        [SerializeField] RectTransform content;
        [SerializeField] CardDeckUI cardPrefab;
        [SerializeField] GetBaseProperties getBaseProperties;
        [SerializeField] List<CardDeckUI> listCardUI;
        [SerializeField] private Button B_checkBox;
        [SerializeField] private Button B_displayLockCard;
        [SerializeField] GameObject filterPopUp;
        private Dictionary<bool, List<CardDeckUI>> dictLocked;
        private ALLCard cardCollections;

        public void Init(ALLCard all)
        {
            cardCollections = all;
        }
        private void Start()
        {
            B_checkBox.onClick.AddListener(DisplayFilter);
            B_displayLockCard.onClick.AddListener(ShowLockCard);
            InitAllCard();
        }

        private void ShowLockCard()
        {
            var listCardLocked = dictLocked[false];

            foreach (var cardUI in listCardLocked)
            {
                bool isActive = cardUI.gameObject.activeInHierarchy;
                cardUI.gameObject.SetActive(!isActive);
            }
        }

        private void DisplayFilter()
        {
            bool active = filterPopUp.activeInHierarchy;
            filterPopUp.SetActive(!active);
        }

        private void InitAllCard()
        {
            cardCollections.ReloadUnlock();
            List<Card> sortedList = SortByCost();
            InstantiateCardUI(sortedList);
            InitDictLocked(listCardUI);
        }

        private void InitDictLocked(List<CardDeckUI> listCardUI)
        {
            dictLocked = new Dictionary<bool, List<CardDeckUI>>
            {
                [true] = new List<CardDeckUI>(),
                [false] = new List<CardDeckUI>()
            };

            foreach (var card in listCardUI)
            {
                bool isUnlock = card.IsUnlocked;
                dictLocked[isUnlock].Add(card);
            }
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
                cardUI.Init(getBaseProperties);

                cardUI.SetCard(c);
                listCardUI.Add(cardUI);
            }
        }


    }

}
