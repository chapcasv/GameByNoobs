using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.Save;

namespace PH
{

    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Deck System")]
    public class DeckSystem : ScriptableObject
    {
        //need fix
        public event Action<Card> OnDraw;
        public event Action OnDropCard;
        public event Action<Card> OnAddCardHand;
        public event Action<bool> ReloadAfterDrop;
        public event Action<int> OnChangeHandCardAmount;
        [SerializeField] PlayerLocalSO data;

        [SerializeField] Deck deckAfterShuffle;
        [SerializeField] ALLCard allCard;

        [NonSerialized] Deck _currentDeck;
        [NonSerialized] Dictionary<TypeMode, List<Card>> _dictionaryCardType;
        [NonSerialized] Dictionary<int, List<Card>> _dictionaryCardCost;
       
        private Deck deckBeforeShuffle;
        private Card _lastCardDrop;
        private GetBaseProperties _getBaseProperties;
        private UITextPopUp _uiTextPopUp;
        public List<Card> CardsInHand { get; private set; }
        public GetBaseProperties GetBaseProperties { set => _getBaseProperties = value; }
        public Card GetLastCardDrop => _lastCardDrop;
        public Deck CurrentDeck => _currentDeck;
        public UITextPopUp SetTextPopUp { set => _uiTextPopUp = value; }
        private const string OutOfAmount = "Giới Hạn Bài Tối Đa";
        public void DrawStartCard()
        {
            Card card = _currentDeck.GetCard(0);
            _currentDeck.Remove(card);
            CardsInHand.Add(card);
            OnChangeHandCardAmount?.Invoke(CardsInHand.Count);
        }

        public bool DrawCard()
        {
            List<Card> listCard = _currentDeck.GetListCard();
            bool resultDraw = DrawFormList(listCard);
            return resultDraw;
        }

        public bool DrawCardFilter(TriggerInputDrawCard input)
        {
            List<Card> filterList = new List<Card>();

            GetFilterType(input, filterList);

            GetFilterCost(input, filterList);

            int numberDraw = input.NumbderCardDraw;

            bool resultDraw = false;

            for (int i = 0; i < numberDraw; i++)
            {
                resultDraw = DrawFormList(filterList);
            }
            return resultDraw;

        }

        //Draw first card in list
        private bool DrawFormList(List<Card> listCard)
        {
            if (listCard.Count > 0)
            {
                Card card = listCard[0];
                RemoveAfterDraw(card);
                AddCardDraw(card);
                return true;
            }
            else return false;
        }

        public void AddCardDraw(Card card)
        {   
            if(CardsInHand.Count < GameConst.MAX_CARD_IN_HAND)
            {
                CardsInHand.Add(card);
                OnChangeHandCardAmount?.Invoke(CardsInHand.Count);
                //Reload card hand UI
                OnDraw?.Invoke(card);
            }
            else if (CardsInHand.Count == GameConst.MAX_CARD_IN_HAND)
            {
                _uiTextPopUp.Set(OutOfAmount);
                return;
            }
        }

        private void GetFilterType(TriggerInputDrawCard input, List<Card> newList)
        {
            if (input.UseDrawByType)
            {
                FilterByType(input.TypeWantDraw, newList);
            }
            else
            {
                FilterByType(TypeMode.ALL, newList);
            }
        }

        private void GetFilterCost(TriggerInputDrawCard input, List<Card> newList)
        {
            if (input.UseDrawByCost)
            {
                int cost = input.ValueCardCost;
                CostMode costMode = input.GetCostMode;

                FilterByCost(cost, costMode, newList);
            }
        }

        private void FilterByCost(int cost, CostMode costMode, List<Card> newList)
        {
            if(costMode == CostMode.EQUAL)
            {
                var oldList = _dictionaryCardCost[cost];
                Filter(newList, oldList);
            }
            else if(costMode == CostMode.LOWER)
            {
                for (int i = 0; i < cost; i++)
                {
                    var oldList = _dictionaryCardCost[cost];
                    Filter(newList, oldList);
                }
            }
            else if(costMode == CostMode.UPPER)
            {
                for (int i = cost; i < GameConst.MAX_CARD_COST; i++)
                {
                    var oldList = _dictionaryCardCost[cost];
                    Filter(newList, oldList);
                }
            }
        }

        private void FilterByType(TypeMode type, List<Card> newList)
        {
            var oldList = _dictionaryCardType[type];
            Filter(newList, oldList);
        }

        private void Filter(List<Card> newList, List<Card> oldList)
        {
            foreach (var card in oldList)
            {
                if (!newList.Contains(card))
                {
                    newList.Add(card);
                }
            }
        }

        public void AddCardToHand(Card card)
        {
            if (CardsInHand.Count < GameConst.MAX_CARD_IN_HAND)
            {
                CardsInHand.Add(card);
                OnChangeHandCardAmount?.Invoke(CardsInHand.Count);
                //Reload card hand UI
                OnAddCardHand?.Invoke(card);
            }
            else if(CardsInHand.Count == GameConst.MAX_CARD_IN_HAND)
            {
                _uiTextPopUp.Set(OutOfAmount);
                return;
            }
        }

        public void DropCard(Card card)
        {
            CardsInHand.Remove(card);
            _lastCardDrop = card;
            OnChangeHandCardAmount?.Invoke(CardsInHand.Count);
            //trigger on board
            OnDropCard?.Invoke();

            ReloadAfterDrop?.Invoke(card.ReLoadAfterDrop);
        }

        public void ReplaceCardHand(int index)
        {
            Card cardReplace = CardsInHand[index];
            Card newCard = Replace(cardReplace);
            CardsInHand[index] = newCard;
        }

        public Card Replace(Card cardWantReplace)
        {
            _currentDeck.Add(cardWantReplace);
            int indexRandom = UnityEngine.Random.Range(0, _currentDeck.AmountCard());
            Card newCard = _currentDeck.GetCard(indexRandom);
            _currentDeck.RemoveAt(indexRandom);
            return newCard;
        }


        #region Deck

        public void InitializePlayerDeck()
        {

            _dictionaryCardType = new Dictionary<TypeMode, List<Card>>();
            _dictionaryCardCost = new Dictionary<int, List<Card>>();

            for (int i = 0; i < GameConst.MAX_CARD_COST; i++)
            {
                _dictionaryCardCost[i] = new List<Card>();
            }

            _dictionaryCardType[TypeMode.ALL] = new List<Card>();
            _dictionaryCardType[TypeMode.UNIT] = new List<Card>();
            _dictionaryCardType[TypeMode.ITEM] = new List<Card>();
            _dictionaryCardType[TypeMode.SPELL] = new List<Card>();

            CopyPlayerCurrentDeck();
            Shuffle();

            _currentDeck = deckAfterShuffle;
            CardsInHand = new List<Card>();
        }

        private void CopyPlayerCurrentDeck()
        {
            var currentDeck = SaveSystem.LoadCurrentDeck();
            Deck deckDefault = ConvertDeck.PlayerDeckToDeck(currentDeck, allCard);
            deckDefault.ReloadListCard();
            deckBeforeShuffle = deckDefault;
        }

        private void Shuffle()
        {
            var deckName = deckBeforeShuffle.deckName;
            deckAfterShuffle.NewDeck(deckName);

            int deckCount = deckBeforeShuffle.AmountCard();

            for (int i = 0; i < deckCount; i++)
            {
                int randomIndexCard = UnityEngine.Random.Range(0, deckBeforeShuffle.AmountCard());
                Card card = deckBeforeShuffle.GetCard(randomIndexCard);

                deckAfterShuffle.Add(card);
                AddDictionaryType(card);
                AddDictionaryCost(card);

                deckBeforeShuffle.RemoveAt(randomIndexCard);
            }
        }

        #endregion

        #region Dictionary

        private void AddDictionaryType(Card card)
        {
            var type = card.GetCardType();
            _dictionaryCardType[type].Add(card);
            _dictionaryCardType[TypeMode.ALL].Add(card);
        }

        private void AddDictionaryCost(Card card)
        {
            int cost = card.Cost;
            _dictionaryCardCost[cost].Add(card);
        }

        private void RemoveAfterDraw(Card card)
        {
            _currentDeck.Remove(card);
            _dictionaryCardType[card.GetCardType()].Remove(card);

            int cost  = _getBaseProperties.GetCost(card);
            _dictionaryCardCost[cost].Remove(card);
        }

        #endregion
    }
}

