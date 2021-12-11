using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Team/PlayerTeam")]
    public class LocalPlayer : ScriptableObject
    {
        [SerializeField] PlayerSO data;
        [SerializeField] PlayerCurrentDeck currentDeck;

        public event Action OnDrawCard;
        public event Action OnDropCard;

        public List<Card> CardsInHand { get ; private set; }

        public void Init()
        {
            currentDeck.InitializePlayerDeck(data.CurrentDeck);
            CardsInHand = new List<Card>();
        }

        public void DrawCard()
        {
            CardsInHand.Add(currentDeck.DrawCard());
            OnDrawCard?.Invoke();
        }

        public void DropCard(Card card)
        {
            CardsInHand.Remove(card);
            OnDropCard?.Invoke();
        }

        public void DrawStartCard() => CardsInHand.Add(currentDeck.DrawCard());

        public void ReplaceCardHand(int index)
        {
            Card cardReplace = CardsInHand[index];
            Card newCard = currentDeck.Replace(cardReplace);
            CardsInHand[index] = newCard;
        }


       




    }
}

