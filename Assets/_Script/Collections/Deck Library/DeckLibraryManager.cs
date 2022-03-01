using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace PH
{
    public class DeckLibraryManager : MonoBehaviour
    {
        [SerializeField] ALLCard allcard;
        [SerializeField] DeckLibraryUI deckLibraryUI;
        [SerializeField] DeckLibraryLogic logic;
        [SerializeField] GetBaseProperties get;
        [SerializeField] GameObject showChildCard;

        private void Awake()
        {
            deckLibraryUI.Init(allcard, get);
            showChildCard.SetActive(true);
        }

    }
}

