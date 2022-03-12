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
        [SerializeField] Image rankLabel;
        [SerializeField] Image costLabel;
        [SerializeField] Image faction;
        [SerializeField] Image coin;
        [SerializeField] TextMeshProUGUI cardName;
        [SerializeField] TextMeshProUGUI cost;
        [SerializeField] TextMeshProUGUI title;

        [SerializeField] TextMeshProUGUI deckName;
        
        private DeckManager _deckManager;
        private GetBaseProperties _get;
        private Deck _deck;
        private int _index;
        private Button button;

        public Sprite GetAvatar => avatar.sprite;
        public Sprite GetFaction => faction.sprite;
        public Color GetColor => rankLabel.color;
        public string GetName => cardName.text;
        public string GetTitle => title.text;
       
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
            cardName.text = _get.GetName(c);
            cost.text = _get.GetCost(c).ToString();
            title.text = _get.GetTitle(c);
            GetCardFaction(c);
            rankLabel.color = c.GetRank.BaseColor;
            costLabel.color = c.GetRank.BaseColor;
        }

        private void GetCardFaction(Card card)
        {
            var factions = card.GetFaction();
            if (factions.Length == 0)
            {
                return;
            }
            else { faction.sprite = factions[0].Icon; }
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


