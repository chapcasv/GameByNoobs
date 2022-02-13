using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Start Card")]
    public class StartCard : Phase
    {
        [SerializeField] DeckSystem deckSystem;

        public event Action OnStartCard;
        public event Action OnReplace;
        public event Action OnComplete;

        public override bool IsComplete()
        {
            if (forceExit)
            {   
                //Hiden UI StartCard
                OnComplete?.Invoke();

                PhaseSystem.CompleteStartCard();
                forceExit = false;
                return true;
            }
            return false;
        }

        public override void OnStartPhase()
        {
            deckSystem.InitializePlayerDeck();
            DrawStartCard();

            PhaseSystem.RunTimeBarStartCard(maxTime);

            //Load Start card UI
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

        //Call by Btn Replace
        public void Replace(Transform slot)
        {
            int startCardIndex = GetSlotIndex(slot);
            deckSystem.ReplaceCardHand(startCardIndex);

            //Display new card
            OnReplace?.Invoke();
        }
        private int GetSlotIndex(Transform slot) => slot.GetSiblingIndex();
    }
}

