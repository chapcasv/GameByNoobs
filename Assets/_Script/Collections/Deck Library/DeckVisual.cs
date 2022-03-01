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

        private GetBaseProperties _get;
        private Deck _deck;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => OnClick());
        }

        public void Init(GetBaseProperties get)
        {
            _get = get;
        }

        public void SetDeck(Deck deck)
        {
            _deck = deck;
            LoadDeck();
        }

        private void LoadDeck()
        {
            deckName.text = _deck.deckName;
            SetAvatar(_deck.CardsInDeck);
        }
        private void SetAvatar(List<Card> cardsInDeck)
        {
            Card c = GetCardHightRank(cardsInDeck);
            avatar.sprite = _get.GetArt(c);
        }

        private Card GetCardHightRank(List<Card> cardsInDeck)
        {
            var sortByRank = cardsInDeck.OrderBy(x => x.GetRank.RankTier).ToList().LastOrDefault();
            return sortByRank;
        }

        private void OnClick()
        {
            Debug.Log("Click " + _deck.deckName);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}


