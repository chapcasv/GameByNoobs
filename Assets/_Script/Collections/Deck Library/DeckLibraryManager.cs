using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public static Deck DeckSelected { get; set; }

        private void Awake()
        {
            deckLibraryUI.Init(allcard, get);
            deckLibraryUI.Constructor(logic,deckManager);
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

    }
}

