using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class HandZoneUI : MonoBehaviour
    {
        [SerializeField] DragCardSelected dragCardSelected;
        [SerializeField] ControlState controlState;
        [SerializeField] GameObject[] cardHand;
        [SerializeField] DeckSystem deckSystem;

        private CardVisual[] _cardVizs;
        private CardInstance[] _cardInstance;
        private CardInfoVisual _cardInfoViz;
        private Animator anim;
        private bool isShowHandZone = true;

        public void SetCardInfomation(CardInfoVisual value)
        {
            _cardInfoViz = value;
        }

        private void Awake()
        {
            Init();
            AddListerner();
            anim = GetComponent<Animator>();
        }

        private void AddListerner()
        {
            deckSystem.OnDrawCard += LoadHandZone;
            deckSystem.OnDropCard += LoadHandZone;
            deckSystem.OnAddCardHand += LoadHandZone;
            controlState.OnLeftClick += MoveHandZone;
        }

        private void Init()
        {
            _cardVizs = new CardVisual[cardHand.Length];
            _cardInstance = new CardInstance[cardHand.Length];

            for (int i = 0; i < _cardVizs.Length; i++)
            {
                _cardVizs[i] = cardHand[i].GetComponent<CardVisual>();
                _cardInstance[i] = cardHand[i].GetComponent<CardInstance>();
                _cardInstance[i].CardSeleted = dragCardSelected;
                _cardInstance[i].CardInfomation = _cardInfoViz;
            }
        }

        private void LoadHandZone()
        {
            SetViz(deckSystem.CardsInHand);
            SetCardsInstance(deckSystem.CardsInHand);
            SetActive();
        }

        private void SetActive()
        {

            for (int i = 0; i < _cardVizs.Length; i++)
            {
                if (_cardInstance[i].Card != null)
                {
                    cardHand[i].SetActive(true);

                }
                else  cardHand[i].SetActive(false);
            }
        }

        private void SetCardsInstance(List<Card> cardsInHand)
        {
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                _cardInstance[i].Card = cardsInHand[i];
            }

            for (int i = cardsInHand.Count; i < cardHand.Length; i++)
            {
                _cardInstance[i].Card = null;
            }
        }

        private void MoveHandZone()
        {
            isShowHandZone = !isShowHandZone;

            anim.SetBool("IsShow", isShowHandZone);
        }

        private void SetViz(List<Card> cardsInHand)
        {
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                _cardVizs[i].SetCard(cardsInHand[i]);
            }
        }

        private void OnDisable()
        {
            RemoveListerner();
        }
        private void RemoveListerner()
        {
            deckSystem.OnDrawCard -= LoadHandZone;
            deckSystem.OnDropCard -= LoadHandZone;
            deckSystem.OnAddCardHand -= LoadHandZone;
            controlState.OnLeftClick -= MoveHandZone;
        }

      
    }
}

