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
        [SerializeField] ChildCardUI childCard;

        private DeckLibraryLogic _logic;
        private DeckManager _deckManager;

        public void Constructor(DeckLibraryLogic logic, DeckManager deckManager)
        {
            _logic = logic;
            _deckManager = deckManager;
            childCard.Constructor(deck, deckEditor, _get, allCards);
        }

        protected override void Start()
        {
            base.Start();
        }

        public void InitDeck(List<Deck> decks)
        {
            foreach (var deck in decks)
            {
                var deckVisual = Instantiate(pfDeckVisual, contentDeck);
                deckVisual.Init(_get, _deckManager);
                deckVisual.SetDeck(deck);
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
        }
    }

}
