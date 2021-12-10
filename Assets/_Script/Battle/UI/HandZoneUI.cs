using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
 
    public class HandZoneUI : MonoBehaviour
    {
        [SerializeField] GameObject[] cardHand;
        [SerializeField] LocalPlayer localPlayer;
        private CardViz[] _cardVizs;
        private CardInstance[] _cardInstance;

        private void Awake()
        {
            Init();
            AddListerner();
        }

        private void AddListerner()
        {
            StartCardSystem.OnComplete += LoadHandZone;
            localPlayer.OnDrawCard += LoadHandZone;
        }

        private void Init()
        {
            _cardVizs = new CardViz[cardHand.Length];
            _cardInstance = new CardInstance[cardHand.Length];

            for (int i = 0; i < _cardVizs.Length; i++)
            {
                _cardVizs[i] = cardHand[i].GetComponent<CardViz>();
                _cardInstance[i] = cardHand[i].GetComponent<CardInstance>();
            }
        }

        private void LoadHandZone()
        {   
            SetViz(localPlayer.CardsInHand);
            SetCardsInstance(localPlayer.CardsInHand);
            SetActive();
        }

        private void SetActive()
        {

            for (int i = 0; i < _cardVizs.Length; i++)
            {
                if (_cardVizs[i].IsHaveCard())
                {
                    cardHand[i].SetActive(true);
                }
                else
                {
                    cardHand[i].SetActive(false);
                }
            }
        }

        private void SetCardsInstance(List<Card> cardsInHand)
        {
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                _cardInstance[i].Card = cardsInHand[i];
            }
        }

        private void SetViz(List<Card> cardsInHand)
        {
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                _cardVizs[i].SetCard(cardsInHand[i]);
            }
        }
    }
}

