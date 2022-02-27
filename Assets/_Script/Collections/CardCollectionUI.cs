using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using PH.Save;

namespace PH
{
    public class CardCollectionUI : CardVisual
    {
        [SerializeField] Image[] iconAmount;
        public event Action<CardCollectionUI> OnClickCardCollection;
        public Action OnClick;
        public Image lockImg;

        private Button main;
        private CardVizInfoCollection _cardInformation;
        private bool isUnlock;
        private int cost;
        private int price;
        private GetBaseProperties _get;

        public bool IsUnlocked { get => isUnlock; }
        public int Cost { get => cost; }
        public int Price { get => price; }
   
        public void Init(GetBaseProperties get, CardVizInfoCollection cardInformation)
        {
            _cardInformation = cardInformation;
            _get = get;

            OnClick = OnClickCallBack;
            main = GetComponent<Button>();
            
            if(main != null)
            {
                main.onClick.AddListener(() => OnClick?.Invoke());
            }
        }

        public override void SetCard(Card c)
        {
            isUnlock = c.Unlocked;

            if (isUnlock)
            {
                var playerCards = SaveSystem.LoadCards();

                foreach (var card in playerCards)
                {
                    if (card.ID == c.CardID)
                    {
                        SetCardByPlayerCard(c, card);
                        break;
                    }
                }
            }
            else SetCardByCard(c);
        }

        private void SetCardByCard(Card c)
        {
            SetProperties(c);
            SetCardAmount();
        }

        private void SetCardByPlayerCard(Card card, PlayerCard player)
        {
            SetProperties(card);
            SetCardAmount(player);
        }

        private void SetProperties(Card c)
        {
            cost = _get.GetCost(c);
            price = _get.GetPrice(c);
            _card = c;

            base.LoadCard(c);
            c.OnSetUnlocked(this);
        }

        private void SetCardAmount(PlayerCard card)
        {
            int amount = card.Amount;

            for (int i = 0; i < amount; i++)
            {
                iconAmount[i].gameObject.SetActive(true);
            }

            for (int i = amount; i < iconAmount.Length; i++)
            {
                iconAmount[i].gameObject.SetActive(false);
            }
        }

        private void SetCardAmount()
        {
            for (int i = 0; i < iconAmount.Length; i++)
            {
                iconAmount[i].gameObject.SetActive(false);
            }
        }

        private void OnClickCallBack()
        {
            _cardInformation.LoadCardInformation(_card, this);
            OnClickCardCollection?.Invoke(this);
        }

        //27/02/2022 0 reference
        public TypeMode GetTypeCardOnCollection()
        {
            var type = _card.GetCardType();
            return type;
        }
    }
}
