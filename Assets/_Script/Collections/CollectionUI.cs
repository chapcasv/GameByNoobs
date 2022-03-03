using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PH
{
    public abstract class CollectionUI : MonoBehaviour
    {   
        [Header("=== BASE CLASS Properties ===")]
        [SerializeField] protected CardVizCollection cardPrefab;
        [SerializeField] protected RectTransform content;

        [SerializeField] protected List<CardVizCollection> listCardUI;

        [Header("Button")]
        [SerializeField] protected Button B_Back;
        [SerializeField] protected Button B_DisplayCardLocked;

        [Header("Filter Mode")]
        [SerializeField] protected GameObject filterPopUp;
        [SerializeField] protected Button B_DisplayFilterPopUp;

        protected Dictionary<bool, List<CardVizCollection>> dictUnlocked;
        protected GetBaseProperties _get;

        protected ALLCard allCards;


        public void Init(ALLCard all, GetBaseProperties get)
        {
            allCards = all;
            _get = get;
        }

        protected virtual void Start()
        {
            AddListener();
            InitAllCard();
        }

        protected void InitAllCard()
        {
            allCards.ReloadUnlock();

            var allCard = allCards.allCard;

            List<Card> sortedList = CollectionMethods.SortByCost(allCard);
            InstantiateCardUI(sortedList);
            InitDictLocked(listCardUI);
        }

        protected abstract void InstantiateCardUI(List<Card> sortedList);

        protected virtual void AddListener()
        {
            B_Back.onClick.AddListener(BackMainMenu);
            B_DisplayFilterPopUp.onClick.AddListener(DisplayFilter);
            B_DisplayCardLocked.onClick.AddListener(DisplayCardLocked);
        }

        protected void DisplayFilter()
        {
            bool active = filterPopUp.activeInHierarchy;
            filterPopUp.SetActive(!active);
        }

        protected virtual void DisplayCardLocked()
        {
            var listCardLocked = dictUnlocked[false];
            CollectionMethods.DisplayCardLocked(listCardLocked);
        }

        protected void InitDictLocked(List<CardVizCollection> listCardUI)
        {
            dictUnlocked = new Dictionary<bool, List<CardVizCollection>>
            {
                [true] = new List<CardVizCollection>(),
                [false] = new List<CardVizCollection>()
            };

            foreach (var card in listCardUI)
            {
                bool isUnlock = card.IsUnlocked;
                dictUnlocked[isUnlock].Add(card);
            }
        }

        private void BackMainMenu()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }

        protected virtual void OnDestroy()
        {
            RemoveListener();
        }

        protected virtual void RemoveListener()
        {
            B_Back.onClick.RemoveAllListeners();
            B_DisplayFilterPopUp.onClick.RemoveAllListeners();
            B_DisplayCardLocked.onClick.RemoveAllListeners();
        }

    }
}

