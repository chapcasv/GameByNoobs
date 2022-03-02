using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.Save;
using System;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Collection/Unlock Card")]
    public class UnlockCard : ScriptableObject
    {
        [SerializeField] GetBaseProperties get; 

        public bool Unlock(Card card, PlayerLocalSO playerLocalSO, ALLCard aLLCard)
        {
            CheckUnlock(card);

            if (CollectionMethods.EnoughCoin(get.GetPrice(card)))
            {
                return Execute(card, playerLocalSO, aLLCard);
            }
            else return false;
        }

        private void CheckUnlock(Card card)
        {
            var PlayerCards = SaveSystem.LoadCards();

            foreach (var playerCard in PlayerCards)
            {
                if(playerCard.ID == card.CardID)
                {
                    throw new Exception("Card is unlocked");
                }
            }
        }
    

        private bool Execute(Card card, PlayerLocalSO playerLocalSO, ALLCard aLLCard)
        {
            var playerCards = SaveSystem.LoadCards();
            PlayerCard newCard = ConvertCard.CardToPlayerCard(card);

            if (Subtract(card, playerLocalSO))
            {
                playerCards.Add(newCard);

                SaveSystem.SaveCards(playerCards);

                playerLocalSO.Cards = ConvertCard.PlayerCardsToCards(playerCards, aLLCard);

                card.Unlocked = true;
                return true;
            }
            else return false;
            
        }

        private bool Subtract(Card card, PlayerLocalSO playerLocalSO)
        {
            var coin = SaveSystem.LoadCoin();
            var price = get.GetPrice(card);

            if(price <= 0)
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
    }
}

