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
        }

        private void LoadPlayerDeck()
        {
            playerLocalSO.ReloadDecks();
            deckLibraryUI.InitDeck(playerLocalSO.Decks);
        }

        public static void SaveCurrentDeck()
        {   
            var playerDecks = SaveSystem.LoadDecks();
            playerDecks[IndexCurrentDeck] = ConvertDeck.DeckToPlayerDeck(CurrentDeck);
            SaveSystem.SaveDecks(playerDecks);
            //Test
            SaveSystem.SaveCurrentDeck(ConvertDeck.DeckToPlayerDeck(CurrentDeck));
        }

    }
}

