using PH.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class CardDeckUI : CardVisual
    {
        [SerializeField] Image[] iconAmount;
        public Image lockImg;

        private bool isUnlock;
        private int cost;
        private int price;
        private GetBaseProperties _get;
        public bool IsUnlocked { get => isUnlock; }
        public int Cost { get => cost; }
        public int Price { get => price; }
        public void Init(GetBaseProperties get)
        {
            _get = get;

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
            c.SetUnlocked(this);
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

    }
}

