using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SO;
using UnityEngine.UI;

namespace PH
{
    
    public class StartCardPhase : MonoBehaviour
    {
        public static bool RunTimeBar;
        public static event Action OnComplete;
        public event Action OnStartCard;
        public event Action OnReplace;
        [SerializeField] DeckSystem deckSystem;
        [SerializeField] Button btnComplete;

        private const float time = 20f;

        public float Time { get => time;}

        private void Awake()
        {
            deckSystem.InitializePlayerDeck();
            btnComplete.onClick.AddListener(StopTimeBar);
        }

        private void OnDisable()
        {
            btnComplete.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            RunTimeBar = true;
            DrawStartCard();
            OnStartCard?.Invoke();
        }


        private void DrawStartCard()
        {
            //Player start with 4 card
            for (int i = 0; i < 4; i++)
            {
                deckSystem.DrawStartCard();
            }
        }
        
        public Card[] GetStartCard()
        {
            Card[] startCards = new Card[4];

            for (int i = 0; i < deckSystem.CardsInHand.Count; i++)
            {
                startCards[i] = deckSystem.CardsInHand[i];
            }
            return startCards;
        }

        //Tracking ===Start Card ===
        public void Replace(Transform slot)
        {
            int startCardIndex = GetSlotIndex(slot);
            deckSystem.ReplaceCardHand(startCardIndex);
            OnReplace?.Invoke();
        }

        private int GetSlotIndex(Transform slot) => slot.GetSiblingIndex();

        private void StopTimeBar() => RunTimeBar = false;

        public void Complete()
        {
            OnComplete?.Invoke();
            Hiden();
        }

        private void Hiden() => gameObject.SetActive(false);

    }
}

