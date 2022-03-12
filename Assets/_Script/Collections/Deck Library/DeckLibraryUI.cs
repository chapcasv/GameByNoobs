using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class DeckLibraryUI : CollectionUI
    {
        [Header("=== DERIVED CLASS Properties ===")]

        [SerializeField] Button B_Editor;
        [SerializeField] GameObject deck;
        [SerializeField] GameObject deckEditor;
        [SerializeField] DeckVisual pfDeckVisual;
        [SerializeField] Transform contentDeck;
        private List<DeckVisual> allDecks;
        private ChildCardUI _childCardUI;
        private DeckLibraryLogic _logic;
        private DeckManager _deckManager;

        public void Constructor(DeckLibraryLogic logic, DeckManager deckManager, ChildCardUI childCardUI)
        {
            _logic = logic;
            _childCardUI = childCardUI;
            _logic.OnClickLogic += _childCardUI.DisplayTotalCard;
            _deckManager = deckManager;
            _childCardUI.Constructor(deck, deckEditor, _get, allCards);
            AddListenerCardChild();
        }

        private void AddListenerCardChild()
        {
            var allChild = _childCardUI.GetAllCardChild;

            foreach (var child in allChild)
            {
                child.OnClick += ReloadCardVizUsing;
            }
        }

        protected override void Start()
        {
            base.Start();
            
        }

        public void InitDeck(List<Deck> decks)
        {
            allDecks = new List<DeckVisual>();
            for (int i = 0; i < decks.Count; i++)
            {
                decks[i].ReloadListCard();
                var deckVisual = Instantiate(pfDeckVisual, contentDeck);
                deckVisual.Init(_get, _deckManager);
                deckVisual.SetDeck(decks[i],i);
                allDecks.Add(deckVisual);
            }
            SelectFirstDeck();
        }
        public void ReLoadAllDeck(List<Deck> decks)
        {
            for (int i = 0; i < decks.Count; i++)
            {
                allDecks[i].SetDeck(decks[i], i);
            }
            SelectFirstDeck();

        }
        private void SelectFirstDeck()
        {
            var firstDeck = contentDeck.GetChild(0);
            var deckViz = firstDeck.GetComponent<DeckVisual>();
            if (deckViz != null) deckViz.OnClick();
        }

        protected override void AddListener()
        {
            base.AddListener();
            B_Editor.onClick.AddListener(CurrentDeckEditor);
        }

        private void CurrentDeckEditor()
        {
            deck.SetActive(false);
            deckEditor.SetActive(true);
            SetCardUsing();
        }

        private void SetCardUsing()
        {
            var cardsInDeck = DeckLibraryManager.CurrentDeck.GetCardInDecks;

            //Only set using for card unlocked
            var cardUnlock = dictUnlocked[true];

            foreach (var cardUI in cardUnlock)
            {
                Card cardData = cardUI.GetCard;
                CardInDeck cardInDeck = GetCardInDeck(cardData, cardsInDeck);

                if(cardInDeck != null)
                {   
                    //This card data using in deck selected
                    //Reload card UI by using amount
                    cardUI.SetCard(cardInDeck);
                }
                else
                {   
                    //This card data not use in deck seleted
                    //Reload card Ui with using amount zero
                    CardInDeck newCard = new CardInDeck(cardData, 0);
                    cardUI.SetCard(newCard);
                }
            }
        }

        private CardInDeck GetCardInDeck(Card c, List<CardInDeck> cardsInDeck)
        {
            CardInDeck newCard = null;

            foreach (var card in cardsInDeck)
            {
                if (card.Card.CardID == c.CardID)
                {
                    return newCard = card;
                }
            }
            return newCard;
        }

        public void ReloadCardVizUsing(CardInDeck cardInDeck)
        {   
            //Only reload card unlocked
            var listCardUnlock = dictUnlocked[true];

            foreach (var c in listCardUnlock)
            {
                if(c.GetCardInDeck.Card.CardID == cardInDeck.Card.CardID)
                {
                    c.SetCard(cardInDeck);
                    break;
                }
            }
        }

        protected override void InstantiateCardUI(List<Card> sortedList)
        {
            for (int i = 0; i < sortedList.Count; i++)
            {
                var c = sortedList[i];

                var cardUI = Instantiate(cardPrefab, content);
                cardUI.Init(_get, _logic);

                cardUI.SetCard(c);
                listCardUI.Add(cardUI);
            }
        }

        protected override void RemoveListener()
        {
            base.RemoveListener();
            B_Editor.onClick.RemoveAllListeners();
            _logic.OnClickLogic -= _childCardUI.DisplayTotalCard;
            RemoveListenerCardChild();
        }

        private void RemoveListenerCardChild()
        {
            var allChild = _childCardUI.GetAllCardChild;

            foreach (var child in allChild)
            {
                child.OnClick -= ReloadCardVizUsing;
            }
        }
    }

}
