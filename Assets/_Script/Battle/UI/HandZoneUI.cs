using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class HandZoneUI : MonoBehaviour
    {
        [SerializeField] DragCardSelected dragCardSelected;

        [Header("State")]
        [SerializeField] ControlState controlState;
        [SerializeField] TeamFightState teamFightState;

        [Header("Phase")]
        [SerializeField] BeforeTeamFight beforeTeamFight;

        [SerializeField] GameObject[] cardHand;
        [SerializeField] DeckSystem deckSystem;

        private CardVisual[] _cardVizs;
        private CardInstance[] _cardInstance;
        private CardInfoVisual _cardInfoViz;
        private PlayerDragLogic _playerDragLogic;
        private Animator anim;
        private GridLayoutGroup gridLayout;
        private const int SPACING_X_DEFAULT = 100;
        private const int SPACING_Y_DEFAULT = 0;
        private const int SPACING_X_OFFSET = 30;
        private const int NUMBER_CARD_HAND_DEFAULT = 5;
        private bool isShowHandZone = false;

        public void Setter(CardInfoVisual value, PlayerDragLogic playerDragLogic)
        {
            _cardInfoViz = value;
            _playerDragLogic = playerDragLogic;

            _playerDragLogic.OnBeginDrag += HidenHandZone;
            _playerDragLogic.OnEndDrag += DisplayHandZone;
        }

        private void Awake()
        {
            Init();
            AddListerner();
            anim = GetComponent<Animator>();
            gridLayout = GetComponent<GridLayoutGroup>();
        }

        private void AddListerner()
        {
            deckSystem.OnDrawCard += LoadHandZone;
            deckSystem.OnDropCard += LoadHandZone;
            deckSystem.OnAddCardHand += LoadHandZone;
            controlState.OnLeftClick += DisplayHandZone;
            teamFightState.OnLeftClick += DisplayHandZone;
            beforeTeamFight.OnEnterBeforeTeamFight += HidenHandZone;
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
            SortUI(deckSystem.CardsInHand.Count);
            SetActive();

            if (!isShowHandZone)  DisplayHandZone(); 
        }

        private void SortUI(int cardHandCount)
        {   
            //If player have 5+ card hand, sort logic will change
            if(cardHandCount > NUMBER_CARD_HAND_DEFAULT)
            {
                int cardOut = cardHandCount - NUMBER_CARD_HAND_DEFAULT;

                int spacingX = SPACING_X_DEFAULT - (cardOut * SPACING_X_OFFSET);

                if(spacingX < 0)
                {
                    spacingX = 0;
                }

                gridLayout.spacing = new Vector2(spacingX, SPACING_Y_DEFAULT);
            }
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

        private void DisplayHandZone()
        {   
            //Need fix this line
            if (PhaseSystem.CurrentPhase as BeforeTeamFight) return;

            isShowHandZone = !isShowHandZone;
            anim.SetBool("IsShow", isShowHandZone);
        }

        private void HidenHandZone()
        {   
            isShowHandZone = false;
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
            controlState.OnLeftClick -= DisplayHandZone;
            teamFightState.OnLeftClick -= DisplayHandZone;
            beforeTeamFight.OnEnterBeforeTeamFight -= HidenHandZone;

            //Add by setter
            _playerDragLogic.OnBeginDrag -= HidenHandZone;
            _playerDragLogic.OnEndDrag -= DisplayHandZone;
        }
    }
}

