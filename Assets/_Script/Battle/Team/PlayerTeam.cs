using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Team/PlayerTeam")]
    public class PlayerTeam : Team
    {

        [SerializeField] PlayerSO data;
        public List<BaseEntiny> cardsInBoard;
        public List<Card> cardsInHand;
        public PlayerCurrentDeck playerCurrentDeck;

        public void Init()
        {
            playerCurrentDeck.InitializePlayerDeck(data.CurrentDeck);
            cardsInBoard = new List<BaseEntiny>();
            cardsInHand = new List<Card>();
        }

        public void DrawCard(int number = 1)
        {
            for (int i = 1; i <= number; i++)
            {
                cardsInHand.Add(playerCurrentDeck.DrawCard());
            }
        }



    }
}

