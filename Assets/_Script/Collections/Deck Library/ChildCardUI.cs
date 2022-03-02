using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class ChildCardUI : MonoBehaviour
    {
        [SerializeField] private Button B_done;
        [SerializeField] RectTransform contentChild;
        [SerializeField] CardChildViz pfCardChildViz;

        private GameObject _deckScreen;
        private GameObject _deckEditor;
        private List<CardChildViz> allCardChild;
        private GetBaseProperties _get;
        private ALLCard _allCard;


        public void Constructor(GameObject deckScreen, GameObject deckEditor, GetBaseProperties get, ALLCard all)
        {
            _deckScreen = deckScreen;
            _deckEditor = deckEditor;
            _get = get;
            _allCard = all;
            InitCardChild();
        }

        private void OnEnable()
        {
            LoadCardChild();
        }


        private void Start()
        {
            B_done.onClick.AddListener(GoDeckCallBack);
        }

        //need fix
        private void InitCardChild()
        {
            allCardChild = new List<CardChildViz>();

            for (int i = 0; i < 20; i++)
            {
                var cardChildViz = Instantiate(pfCardChildViz, contentChild);
                cardChildViz.Setter(_get, _allCard);
                cardChildViz.gameObject.SetActive(false);
                allCardChild.Add(cardChildViz);
            }
        }

        private void LoadCardChild()
        {
            List<CardInDeck> cardsInDeck = GetListCard();

            LoadByList(cardsInDeck);
        }

        private static List<CardInDeck> GetListCard()
        {
            var currentDeck = DeckLibraryManager.DeckSelected;

            var sortedList = CollectionMethods.SortByCost(currentDeck.GetCardInDecks);

            currentDeck.SetCardInDecks(sortedList);

            var cardsInDeck = currentDeck.GetCardInDecks;
            return cardsInDeck;
        }

        private void LoadByList(List<CardInDeck> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                allCardChild[i].LoadCard(cards[i]);
            }
            for (int i = cards.Count; i < allCardChild.Count; i++)
            {
                allCardChild[i].gameObject.SetActive(false);
            }
        }

        private void GoDeckCallBack()
        {
            _deckScreen.SetActive(true);
            _deckEditor.SetActive(false);
        }

        private void OnDestroy()
        {
            B_done.onClick.RemoveAllListeners();
        }
    }
}

