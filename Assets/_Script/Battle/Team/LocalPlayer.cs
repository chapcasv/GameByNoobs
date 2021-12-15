using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SO;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Team/PlayerTeam")]
    public class LocalPlayer : ScriptableObject
    {
        public event Action OnDrawCard;
        public event Action OnDropCard;
        public event Action OnMemberAmountChange;
        public event Action OnCoinNumberChange;
        public event Action OnLifeValueChange;

        [SerializeField] PlayerSO data;
        [SerializeField] PlayerCurrentDeck currentDeck;
        [SerializeField] IntReference memberAmount;
        [SerializeField] IntReference coin;
        [SerializeField] IntReference life;

        [NonSerialized]
        private bool _isInit = false;

        public int GetLife => life.Value;
        public int GetMemberAmount => memberAmount.Value;
        public int GetCoin => coin.Value;

        public List<Card> CardsInHand { get ; private set; }

        public void Init(int startCoin, int startLife)
        {
            if (_isInit) return;

            currentDeck.InitializePlayerDeck(data.CurrentDeck);
            coin.Value = startCoin;
            life.Value = startLife;
            memberAmount.Value = 0;
            CardsInHand = new List<Card>();

            _isInit = true;
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

        public void SetCoin(int value)
        {
            coin.Value = value;
            OnCoinNumberChange?.Invoke();
        }

        public void SetLife(int value)
        {
            life.Value = value;
            OnLifeValueChange?.Invoke();
        }

        public void IncreaseMember()
        {
            memberAmount.Value++;
            OnMemberAmountChange?.Invoke();
        }

        public void SubTractMember()
        {
            memberAmount.Value--;
            OnMemberAmountChange?.Invoke();
        }
 

    }
}

