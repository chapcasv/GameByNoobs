using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PH
{
    public class ShowChildCardUI : MonoBehaviour
    {
        [SerializeField] private ALLCard allCards;
        [SerializeField] private CardDeckUI cardPrefab;
        [SerializeField] private Button lockCardButton;
        [SerializeField] private Button checkBoxButton;
        [SerializeField] private GetBaseProperties getBaseProperties;
        [SerializeField] private GameObject CheckBoxObj;
        [SerializeField] private List<CardDeckUI> LockCards;
        [SerializeField] private List<CardDeckUI> listCard;

        private void Start()
        {
            lockCardButton.onClick.AddListener(ShowLockCardCallBack);
            checkBoxButton.onClick.AddListener(OpenCheckBoxCallBack);
            PopUpCardCollection();
            ArrangeListCard();

        }

        private void OpenCheckBoxCallBack()
        {
            bool active = CheckBoxObj.activeInHierarchy;
            CheckBoxObj.SetActive(!active);
        }

        private void ShowLockCardCallBack()
        {
            bool active = LockCards[0].gameObject.activeInHierarchy;
            for (int i = 0; i < LockCards.Count; i++)
            {
                LockCards[i].gameObject.SetActive(!active);
            }
        }
        private void OnDisable()
        {
            lockCardButton.onClick.RemoveAllListeners();
            checkBoxButton.onClick.RemoveAllListeners();
        }
        private void PopUpCardCollection()
        {
            var firstCard = allCards.allCard[0];
            cardPrefab.SetCard(firstCard);
            cardPrefab.Init(firstCard, getBaseProperties);
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
                _cardCollection.Init(card, getBaseProperties);
                if (!_cardCollection.IsUnlocked)
                {
                    LockCards.Add(_cardCollection);
                }
                listCard.Add(_cardCollection);
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
            CardDeckUI temp;

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

    }
}

