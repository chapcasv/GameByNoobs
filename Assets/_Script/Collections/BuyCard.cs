using PH.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Collection/Buy Card")]
    public class BuyCard : ScriptableObject
    {
        [SerializeField] GetBaseProperties get;

        private PlayerCard currentPlayerCard;

        public bool Buy(Card card, PlayerLocalSO playerLocalSO, ALLCard aLLCard)
        {
            CheckUnlocked(card);

            if (EnoughCoin(card))
            {
                if (IsHaveSlotFree())
                {
                    return Execute(card, playerLocalSO, aLLCard);
                }
                else return false;
            }
            else return false;
        }

        private bool Execute(Card card, PlayerLocalSO playerLocalSO, ALLCard aLLCard)
        {
            var playerCards = SaveSystem.LoadCards();

            if (Subtract(card, playerLocalSO))
            {
                foreach (var playerCard in playerCards)
                {
                    if(playerCard.ID == currentPlayerCard.ID)
                    {
                        playerCard.Amount++;
                        SaveSystem.SaveCards(playerCards);
                        return true;
                    }
                }
                Debug.Log("error");
                return false;
            }
            else return false;
        }

        private bool EnoughCoin(Card card)
        {
            var coin = SaveSystem.LoadCoin();
            Debug.Log(card);
            var price = get.GetPrice(card);
            return coin >= price;
        }

        private bool IsHaveSlotFree()
        {
            return currentPlayerCard.Amount < 3;
        }

        private bool Subtract(Card card, PlayerLocalSO playerLocalSO)
        {
            var coin = SaveSystem.LoadCoin();
            var price = get.GetPrice(card);

            if (price <= 0)
            {
                throw new Exception("Price error !!!");
            }
            else
            {
                coin -= price;
                playerLocalSO.Coin = coin;
                return playerLocalSO.SaveCoinData(-price);
            }
        }

        private void CheckUnlocked(Card card)
        {
            var PlayerCards = SaveSystem.LoadCards();

            bool isUnlock = false;

            foreach (var playerCard in PlayerCards)
            {
                if (playerCard.ID == card.CardID)
                {
                    isUnlock = true;
                    currentPlayerCard = playerCard;
                    break;
                }
            }

            if (!isUnlock)
            {
                throw new Exception("Cant buy card locked");
            }
        }
    }
}

