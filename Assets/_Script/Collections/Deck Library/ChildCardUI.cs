using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PH
{
    public class ChildCardUI : MonoBehaviour
    {
        private const string Message = "Bạn muốn lưu lại thay đổi ?";
        private const string Title = "Lưu Thay Đổi";
        [SerializeField] private Button B_done;
        [SerializeField] RectTransform contentChild;
        [SerializeField] CardChildViz pfCardChildViz;
        [SerializeField] TMP_InputField inputDeckName;
        [SerializeField] TextMeshProUGUI totalCardsInDeck;

        private bool changedCard;
        private GameObject _deckScreen;
        private GameObject _deckEditor;
        private List<CardChildViz> allCardChild;
        private GetBaseProperties _get;
        private ALLCard _allCard;
        private IPopUpManager popUpManager;
        public List<CardChildViz> GetAllCardChild => allCardChild;

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
            inputDeckName.text = DeckLibraryManager.CurrentDeck.deckName;
            DisplayTotalCard();
            LoadCardChild();
            ThirdParties.Find<IPopUpManager>(out popUpManager);
            changedCard = false;
        }

        public void DisplayTotalCard()
        {
            changedCard = true;
            totalCardsInDeck.text = DeckLibraryManager.CurrentDeck.AmountCard().ToString();
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
                cardChildViz.OnClickChild += DisplayTotalCard;
                cardChildViz.gameObject.SetActive(false);
                allCardChild.Add(cardChildViz);
            }
        }

        public void LoadCardChild()
        {
            List<CardInDeck> cardsInDeck = GetListCard();
            LoadByList(cardsInDeck);
        }

        private static List<CardInDeck> GetListCard()
        {
            var currentDeck = DeckLibraryManager.CurrentDeck;

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
            if (changedCard)
            {
                popUpManager.ShowPopUpConfirm(Message, Title, SaveChange, Hide);
            }
            else
            {
                Hide();
            }
        }
        private void SaveChange()
        {
            DeckLibraryManager.SaveCurrentDeck();
            Hide();
        }
        private void Hide()
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

