using PH.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PH
{
    public static class CollectionMethods 
    {
        private static string iconCoin = "<sprite=" + "\"CoinTextMesh\"" + " index=0>";

        public static int GetBought(Card card)
        {
            var playerCard = SaveSystem.LoadCards();

            foreach (var c in playerCard)
            {
                if(c.ID == card.CardID)
                {
                    return c.Bought;
                }
            }
            return 0;
        }

        public static List<Card> SortByCost(List<Card> allCard)
        {
            List<Card> clone = new List<Card>(allCard);
            clone = clone.OrderBy(x => x.Cost).ToList();
            return clone;
        }

        public static List<CardInDeck> SortByCost(List<CardInDeck> listCard)
        {
            List<CardInDeck> clone = new List<CardInDeck>(listCard);
            clone = clone.OrderBy(x => x.Card.Cost).ToList();
            return clone;
        }

        public static void DisplayCardLocked(List<CardVizCollection> listCardLocked)
        {
            foreach (var cardUI in listCardLocked)
            {
                bool isActive = cardUI.gameObject.activeInHierarchy;
                cardUI.gameObject.SetActive(!isActive);
            }
        }

        public static Card GetCardHightRank(List<CardInDeck> cardInDecks)
        {
            var sortByRank = cardInDecks.OrderBy(x => x.Card.GetRank.RankTier).ToList().LastOrDefault();
            return sortByRank.Card;
        }

        public static Card GetCardHightRank(List<Card> cards)
        {
            var sortByRank = cards.OrderBy(x => x.GetRank.RankTier).ToList().LastOrDefault();
            return sortByRank;
        }


        public static bool AddPlayerCard(ref List<PlayerCard> playerCards, PlayerCard newCard)
        {
            foreach (var playerCard in playerCards)
            {
                if (playerCard.ID == newCard.ID)
                {
                    if (playerCard.Bought < GameConst.MAX_AMOUNT_CARD_INSTANCE)
                    {
                        playerCard.Bought++;
                        return true;
                    }
                    else return false;
                }
            }
            throw new Exception("Data cards unlocked dont have new card");
        }

        public static bool EnoughCoin(int price)
        {
            var coin = SaveSystem.LoadCoin();
            return coin >= price;
        }

        public static void SetCurrentDeck(int index)
        {
            var playerDeck = SaveSystem.LoadDecks();
            SaveSystem.SaveCurrentDeck(playerDeck[index]);
        }

        public static string UnlockFormat(Card card, GetBaseProperties get)
        {
            var cardName = get.GetName(card);
            var cardPrice = get.GetPrice(card);
            var colorBase = card.GetRank.BaseColor;

            var colorHex = "#" + ColorUtility.ToHtmlStringRGB(colorBase) ;

            return "Mở khóa " + "<color="  + colorHex + ">" + cardName + "</color>"+ $" sẽ tiêu tốn {cardPrice} {iconCoin}";
        }
    }
}

