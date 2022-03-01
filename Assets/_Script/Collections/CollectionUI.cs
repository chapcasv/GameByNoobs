using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PH
{
    public class CollectionUI : MonoBehaviour
    {   
        [Header("=== Base Class Properties ===")]
        [SerializeField] protected CardVizCollection cardPrefab;
        [SerializeField] protected RectTransform content;

        [SerializeField] protected List<CardVizCollection> listCardUI;

        [Header("Button")]
        [SerializeField] protected Button B_Back;
        [SerializeField] protected Button B_DisplayCardLocked;

        [Header("Filter Mode")]
        [SerializeField] protected GameObject filterPopUp;
        [SerializeField] protected Button B_DisplayFilterPopUp;

        protected Dictionary<bool, List<CardVizCollection>> dictLocked;
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
        }

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
            var listCardLocked = dictLocked[false];
            CollectionExtension.DisplayCardLocked(listCardLocked);
        }

        protected void InitDictLocked(List<CardVizCollection> listCardUI)
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

        private void BackMainMenu()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }

        protected virtual void OnDestroy()
        {
            B_Back.onClick.RemoveAllListeners();
            B_DisplayFilterPopUp.onClick.RemoveAllListeners();
            B_DisplayCardLocked.onClick.RemoveAllListeners();
        }

      
    }
}

