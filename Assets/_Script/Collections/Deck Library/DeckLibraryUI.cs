using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class DeckLibraryUI : CollectionUI
    {
        [Header("=== DERIVED CLASS Properties ===")]

        [SerializeField] Button B_Setup;
        [SerializeField] GameObject deck;
        [SerializeField] GameObject showChildCard;
        [SerializeField] GameObject pfDeckVisual;
        [SerializeField] Transform contentDeck;

        private DeckLibraryLogic _logic;

        public void Constructor(DeckLibraryLogic logic)
        {
            _logic = logic;
        }

        protected override void Start()
        {
            base.Start();
        }

        public void InitDeck(List<Deck> decks)
        {
            foreach (var deck in decks)
            {
                Instantiate(pfDeckVisual, contentDeck);
                Debug.Log(deck.deckName);
            }
        }

        protected override void AddListener()
        {
            base.AddListener();
            B_Setup.onClick.AddListener(SetUpDeckCallBack);
        }

        private void SetUpDeckCallBack()
        {
            showChildCard.SetActive(true);
            deck.SetActive(false);
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
            B_Setup.onClick.RemoveAllListeners();
        }
    }

}
