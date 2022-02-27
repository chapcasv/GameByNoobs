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
        [SerializeField] ALLCard cardCollections;

        [SerializeField] GetBaseProperties getBaseProperties;

        [Header("Currency")]
        [SerializeField] TextMeshProUGUI coin;
        [SerializeField] TextMeshProUGUI diamond;


        [Header("Button")]
        [SerializeField] Button backButton;
        [SerializeField] Button OnUnlockedButton;
        [SerializeField] Button UnlockButton;
        [SerializeField] Button BuyButton;

        [Header("CheckBox")]
        [SerializeField] GameObject checkBoxScreen;
        [SerializeField] Toggle checkbox;

        [SerializeField] List<CardCollectionUI> listCard;
        private List<Card> clone;

        private void Start()
        {
            AddListener();
            DisplayCurrency();
            SortByCost();
            InstantiateCardCollectionUI();
            ShowUnlockedCard();
        }

        private void AddListener()
        {
            backButton.onClick.AddListener(BackMainMenu);

            checkbox.onValueChanged.AddListener(OpenCheckBox);
            OnUnlockedButton.onClick.AddListener(ShowLockCardCallBack);
            UnlockButton.onClick.AddListener(UnlockButtoncallBack);
            BuyButton.onClick.AddListener(BuyButtonCallBack);
        }

        #region Currency

        private void DisplayCurrency()
        {
            int coin = SaveSystem.LoadCoin();
            this.coin.text = coin.ToString();
        }

        #endregion

        private void SortByCost()
        {
            clone = new List<Card>(cardCollections.allCard);
            clone = clone.OrderBy(x => x.Cost).ToList();
        }

        private void InstantiateCardCollectionUI()
        {
            for (int i = 0; i < clone.Count; i++)
            {
                var c = clone[i];

                var card = Instantiate(cardPrefab, content);
                card.SetCard(c);
                int price = getBaseProperties.GetPrice(c);
                card.Init(c, price, cardInformation);
                card.OnClickCardCollection = SelectCardCollectionUI;
                listCard.Add(card);
            }
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

        private void BuyButtonCallBack()
        {
            throw new NotImplementedException();
        }

        private void UnlockButtoncallBack()
        {

        }

        private void OpenCheckBox(bool show)
        {
            checkBoxScreen.SetActive(!show);
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


        private void BackMainMenu()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }

        private void SelectCardCollectionUI(CardCollectionUI card)
        {
            UnlockButton.interactable = !card.IsUnlocked;
            BuyButton.interactable = card.IsUnlocked;
        }

        private void OnDestroy()
        {
            backButton.onClick.RemoveAllListeners();

            checkbox.onValueChanged.RemoveAllListeners();
            OnUnlockedButton.onClick.RemoveAllListeners();
            UnlockButton.onClick.RemoveAllListeners();
            BuyButton.onClick.RemoveAllListeners();
        }

    }
}

