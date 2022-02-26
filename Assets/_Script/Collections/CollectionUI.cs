using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PH
{
    public class CollectionUI : MonoBehaviour
    {
        [SerializeField] private CardVizInfoCollection cardInformation;
        

        [SerializeField] private CardCollectionUI cardPrefab;
        [SerializeField] private ALLCard allCards;
        [SerializeField] private Button backButton;
        [SerializeField] private Button OnUnlockedButton;
        [SerializeField] private GetBaseProperties getBaseProperties;
        [SerializeField] private Button UnlockButton;
        [SerializeField] private Button BuyButton;
        [Header("CheckBox")]
        [SerializeField] private GameObject checkBoxScreen;
        [SerializeField] private Button checkbox;

        [SerializeField] private List<CardCollectionUI> listCard;
        [SerializeField]private List<CardCollectionUI> LockCards;
        
        private void Start()
        {
            backButton.onClick.AddListener(BackMainMenu);

            checkbox.onClick.AddListener(OpenCheckBox);
            OnUnlockedButton.onClick.AddListener(ShowLockCardCallBack);
            UnlockButton.onClick.AddListener(UnlockButtoncallBack);
            BuyButton.onClick.AddListener(BuyButtonCallBack);
            PopUpCardCollection();
            ArrangeListCard();
            ShowUnlockedCard();
            
        }

        private void BuyButtonCallBack()
        {
            throw new NotImplementedException();
        }

        private void UnlockButtoncallBack()
        {
            throw new NotImplementedException();
        }

        private void OpenCheckBox()
        {
            bool active = checkBoxScreen.activeInHierarchy;
            checkBoxScreen.SetActive(!active);
        }


        private void ShowLockCardCallBack()
        {
            bool active = LockCards[0].gameObject.activeInHierarchy;
            for (int i = 0; i < LockCards.Count; i++)
            {
                LockCards[i].gameObject.SetActive(!active);
            }
            LoadInformation();
        }

        private void ShowUnlockedCard()
        {
            for (int i = 0; i < listCard.Count; i++)
            {
                var unlock = listCard[i].IsUnlocked;
                listCard[i].OnRefreshCard(unlock);
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
                item.OnClickCardCollection = SelectCardCollectionUI;
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
            var firstCard = allCards.allCard[0];
            cardPrefab.SetCard(firstCard);
            cardPrefab.Init(firstCard, getBaseProperties, cardInformation);
            if (!cardPrefab.IsUnlocked)
            {
                LockCards.Add(cardPrefab);
            }
            listCard.Add(cardPrefab);
            for (int i = 1; i < allCards.allCard.Count; i++)
            {
                var card = allCards.allCard[i];
                var _cardCollection = Instantiate(cardPrefab);
                _cardCollection.SetCard(card);
                
                _cardCollection.Init(card, getBaseProperties, cardInformation);
                if (!_cardCollection.IsUnlocked)
                {
                    LockCards.Add(_cardCollection);
                }
                listCard.Add(_cardCollection);
            }
        }

        private void BackMainMenu()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }
        private void SelectCardCollectionUI(CardCollectionUI card)
        {
            UnlockButton.interactable = !card.IsUnlocked;
            BuyButton.interactable = card.IsUnlocked;
        }
        private void OnDisable()
        {
            backButton.onClick.RemoveAllListeners();
            checkbox.onClick.RemoveAllListeners();
            OnUnlockedButton.onClick.RemoveAllListeners();
            UnlockButton.onClick.RemoveAllListeners();
            BuyButton.onClick.RemoveAllListeners();
        }

    }
}

