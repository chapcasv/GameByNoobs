using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PH.Save;

namespace PH
{
    public class DeckLibraryManager : MonoBehaviour
    {
        [SerializeField] ALLCard allcard;
        [SerializeField] DeckLibraryUI deckLibraryUI;
        [SerializeField] DeckLibraryLogic logic;
        [SerializeField] GetBaseProperties get;
        [SerializeField] PlayerLocalSO playerLocalSO;
        [SerializeField] DeckManager deckManager;
        [SerializeField] ChildCardUI childCardUI;
        [SerializeField] DeckHolder deckHolder;

        private UITextPopUp UITextPopUp;
        public static Deck CurrentDeck { get; set; }
        public static int IndexCurrentDeck { get; set; }

        private void Awake()
        {
            logic.SetChildCardUI(childCardUI);
            deckLibraryUI.Init(allcard, get);
            deckLibraryUI.Constructor(logic,deckManager,childCardUI);
            
        }

        private void Start()
        {
            LoadPlayerDeck();
           
            deckHolder.OnReloadAllDeck += ReloadAllDeck;
            SetTextPopUp();
        }
        private void SetTextPopUp()
        {
            ThirdParties.Find<UITextPopUp>(out var UITextPopUp);
            logic.SetTextPopUp = UITextPopUp;
        }
        private void LoadPlayerDeck()
        {
            playerLocalSO.ReloadDecks();
            deckLibraryUI.InitDeck(playerLocalSO.Decks);
        }
        private void ReloadAllDeck()
        {
            playerLocalSO.ReloadDecks();
            deckLibraryUI.ReLoadAllDeck(playerLocalSO.Decks);

        }
        public static void SaveCurrentDeck()
        {
            ThirdParties.Find<UITextPopUp>(out var UITextPopUp);
            var playerDecks = SaveSystem.LoadDecks();
            UITextPopUp.Set(CollectionMethods.SaveDeck);
            playerDecks[IndexCurrentDeck] = ConvertDeck.DeckToPlayerDeck(CurrentDeck);
            SaveSystem.SaveDecks(playerDecks);
            //Test
            SaveSystem.SaveCurrentDeck(ConvertDeck.DeckToPlayerDeck(CurrentDeck));
        }
        private void OnDestroy()
        {
            deckHolder.OnReloadAllDeck -= ReloadAllDeck;
        }

    }
}

