using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using PH.Save;

namespace PH
{
    public class CardVizCollection : CardVisual
    {
        [SerializeField] Image[] iconBought;
        [SerializeField] Image[] iconUsing;
 
        public Image lockImg;

        private Button main;
        private bool isUnlock;
        private int price;
        private PlayerCard _playerCard;
        private GetBaseProperties _get;
        private CollectionLogic _logic;
        private CardInDeck _cardInDeck;

        public CardInDeck GetCardInDeck => _cardInDeck;
        public bool IsUnlocked { get => isUnlock; }
        public int Price { get => price; }
        public int Bought => _playerCard.Bought;
   
        public void Init(GetBaseProperties get, CollectionLogic logic)
        {
            _get = get;
            _logic = logic;

            main = GetComponent<Button>();
            
            if(main != null)
            {
                main.onClick.AddListener(() => OnClickCallBack());
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
            price = _get.GetPrice(c);
            _card = c;

            base.LoadCard(c);
            c.OnSetUnlocked(this);
        }

        private void SetCardAmount(PlayerCard card)
        {
            _playerCard = card;
            int bought = _playerCard.Bought;

            for (int i = 0; i < bought; i++)
            {
                iconBought[i].gameObject.SetActive(true);
            }

            for (int i = bought; i < iconBought.Length; i++)
            {
                iconBought[i].gameObject.SetActive(false);
            }
        }

        private void SetCardAmount()
        {
            for (int i = 0; i < iconBought.Length; i++)
            {
                iconBought[i].gameObject.SetActive(false);
            }
        }

        public void SetCard(CardInDeck cardInDeck)
        {
            _cardInDeck = cardInDeck;
            ReloadUsing();
        }

        private void ReloadUsing()
        {
            int amount = _cardInDeck.usingAmount;

            for (int i = 0; i < amount; i++)
            {
                iconUsing[i].gameObject.SetActive(true);
            }

            for (int i = amount; i < iconUsing.Length; i++)
            {
                iconUsing[i].gameObject.SetActive(false);
            }
        }

        public void OnClickCallBack() => _logic.OnClick(_card, this);

        //27/02/2022 0 reference
        public TypeMode GetTypeCardOnCollection()
        {
            var type = _card.GetCardType();
            return type;
        }
    }
}
