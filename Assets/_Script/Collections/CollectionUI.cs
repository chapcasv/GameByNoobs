using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PH
{
    public class CollectionUI : MonoBehaviour
    {
        [SerializeField] private CardIVizCollection cardInformation;
        [SerializeField] private CardCollectionUI cardPrefab;
        [SerializeField] private ALLCard cardCollections;
        [SerializeField] private Button backButton;
        [SerializeField] private Button OnUnlockedButton;
        [SerializeField] private GetBaseProperties getBaseProperties;

        [Header("CheckBox")]
        [SerializeField] private GameObject checkBoxScreen;
        [SerializeField] private Toggle checkbox;

        [SerializeField] private List<CardCollectionUI> listCard;
        
        private void Start()
        {
            backButton.onClick.AddListener(BackMainMenu);

            checkbox.onValueChanged.AddListener(OpenCheckBox);
            OnUnlockedButton.onClick.AddListener(ShowLockCardCallBack);
            PopUpCardCollection();
            ShowUnlockedCard();
            ArrangeListCard();
        }

        private void OpenCheckBox(bool show)
        {
            checkBoxScreen.SetActive(!show);
        }


        private void ShowLockCardCallBack()
        {
            
            for (int i = 0; i < listCard.Count; i++)
            {
                bool unlocked = listCard[i].IsUnlocked;
                listCard[i].gameObject.SetActive(!unlocked);
            }
            LoadInformation();
        }

        private void ShowUnlockedCard()
        {
            for (int i = 0; i < listCard.Count; i++)
            {
                if (listCard[i].IsUnlocked == true)
                {
                    listCard[i].OnRefreshCard(true);
                }
                else
                {
                    listCard[i].OnRefreshCard(false);
                }
            }
            LoadInformation();
        }

        private void LoadInformation()
        {
            foreach (var item in listCard)
            {
                if (item.gameObject.activeInHierarchy)
                {
                    item.OnClick?.Invoke();
                    break;
                }
            }
        }

        private void ArrangeListCard()
        {
            SortByCost();
            foreach (var item in listCard)
            {
                item.gameObject.transform.SetParent(cardPrefab.transform.parent);
            }
        }

        private void SortByCost()
        {
            CardCollectionUI temp;

            for (int i = 0; i < listCard.Count; i++)
            {
                for (int j = i + 1; j < listCard.Count; j++)
                {
                    if (listCard[j].Cost < listCard[i].Cost)
                    {
                        temp = listCard[i];
                        listCard[i] = listCard[j];
                        listCard[j] = temp;
                    }
                }
            }
        }

        private void PopUpCardCollection()
        {
            var firstCard = cardCollections.allCard[0];
            cardPrefab.SetCard(firstCard);
            cardPrefab.Init(firstCard, getBaseProperties, cardInformation);
            
            listCard.Add(cardPrefab);
            for (int i = 1; i < cardCollections.allCard.Count; i++)
            {
                var card = cardCollections.allCard[i];
                var _cardCollection = Instantiate(cardPrefab);
                _cardCollection.SetCard(card);
                _cardCollection.Init(card, getBaseProperties, cardInformation);
                listCard.Add(_cardCollection);
            }
        }

        private void BackMainMenu()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }
    }
}

