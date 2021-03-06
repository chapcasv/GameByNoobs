using PH.Save;
using System;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Collection/Buy Card")]
    public class BuyCard : ScriptableObject
    {
        [SerializeField] GetBaseProperties get;

        private PlayerCard currentPlayerCard;
        private UITextPopUp _uITextPopUp;

        public UITextPopUp SetUITextPopUp { set => _uITextPopUp = value; }

        public bool Buy(Card card, PlayerLocalSO playerSO)
        {
            currentPlayerCard = null;

            CheckUnlocked(card);

            bool enoughCoin = CollectionMethods.EnoughCoin(get.GetPrice(card));

            if (enoughCoin)
            {
                if (HaveSlotFree())
                {
                    return Execute(card, playerSO);
                }
                else
                {
                    _uITextPopUp.Set(CollectionMethods.DontHaveSlot);
                    return false;
                }
            }
            else
            {
                _uITextPopUp.Set(CollectionMethods.DontEnoughCoin);
                return false;
            }
        }

        private bool Execute(Card card, PlayerLocalSO playerSO)
        {
            var playerCards = SaveSystem.LoadCards();

            if (Subtract(card, playerSO))
            {
                var newPlayerCard = ConvertCard.CardToPlayerCard(card);
                bool canAdd = CollectionMethods.AddPlayerCard(ref playerCards, newPlayerCard);

                if (canAdd)
                {
                    SaveSystem.SaveCards(playerCards);
                    playerSO.Cards.Add(card);
                    _uITextPopUp.Set(CollectionMethods.BuySuccessful);
                    return true;
                }
                else
                {
                    _uITextPopUp.Set(CollectionMethods.DontHaveSlot);
                    return false;
                }
            }
            else
            {
                _uITextPopUp.Set(CollectionMethods.DontEnoughCoin);
                return false;
            }
        }

        private bool HaveSlotFree()
        {
            return currentPlayerCard.Bought < GameConst.MAX_AMOUNT_CARD_INSTANCE;
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

