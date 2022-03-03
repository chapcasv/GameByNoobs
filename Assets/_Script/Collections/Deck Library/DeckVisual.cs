using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace PH
{
    public class DeckVisual : MonoBehaviour
    {
        [SerializeField] Image avatar;
        [SerializeField] TextMeshProUGUI deckName;

        private DeckManager _deckManager;
        private GetBaseProperties _get;
        private Deck _deck;
        private int _index;
        private Button button;

        public Sprite GetAvatar => avatar.sprite;
        public string GetDeckName => deckName.text;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => OnClick());
        }

        public void Init(GetBaseProperties get, DeckManager manager)
        {
            _get = get;
            _deckManager = manager;
        }

        public void SetDeck(Deck deck, int index)
        {
            _deck = deck;
            _index = index;
            LoadDeck();
        }

        private void LoadDeck()
        {
            deckName.text = _deck.deckName;
            SetAvatar(_deck.GetCardInDecks);
        }
        private void SetAvatar(List<CardInDeck> cardsInDeck)
        {
            Card c = CollectionMethods.GetCardHightRank(cardsInDeck);
            avatar.sprite = _get.GetArt(c);
        }


        public void OnClick()
        {
            DeckLibraryManager.CurrentDeck = _deck;
            DeckLibraryManager.IndexCurrentDeck = _index;
            _deckManager.SetCurrentDeck(this);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}


