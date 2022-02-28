using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Trigger Drop Logic/Create Card/Giong Solid Logic")]
    public class GiongSolidCreateCard : CardDropTriggerLogic
    {
        [SerializeField] DeckSystem deckSystem;
        [SerializeField] int giongID = 8;

        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, TriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            if (HaveGiongInBoard(team))
            {
                CreateGiongWP(input);
            }
            else DrawCardGiong();

            return true;
        }


        private void CreateGiongWP(TriggerInput input)
        {
            InputCreateRandomCard inputRandom = (InputCreateRandomCard)input;

            Card[] cardArray = inputRandom.GetCard();

            for (int i = 0; i < cardArray.Length; i++)
            {
                deckSystem.AddCardToHand(cardArray[i]);
            }

        }

        private void DrawCardGiong()
        {
            Deck currentDeck = deckSystem.CurrentDeck;

            Card giong = currentDeck.FindCard(giongID);

            if(giong != null)
            {
                currentDeck.Remove(giong);
                deckSystem.AddCardToHand(giong);
            }

        }

        private bool HaveGiongInBoard(UnitTeam team)
        {
            bool haveGiongInBoard = false;

            var allUnit = DictionaryTeamBattle.GetAllUnits(team);

            foreach (var unit in allUnit)
            {
                if (unit.GetID == giongID)
                {
                    haveGiongInBoard = true;
                    break;
                }
            }
            return haveGiongInBoard;
        }
    }
}


