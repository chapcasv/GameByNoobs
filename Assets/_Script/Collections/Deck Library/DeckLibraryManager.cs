using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

        private void Awake()
        {
            deckLibraryUI.Init(allcard, get);
            deckLibraryUI.Constructor(logic);
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

