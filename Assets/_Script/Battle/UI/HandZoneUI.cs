using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace PH
{
    public class HandZoneUI : MonoBehaviour
    {
        [SerializeField] DragCardSelected dragCardSelected;

        [Header("State")]
        [SerializeField] ControlState controlState;
        [SerializeField] TeamFightState teamFightState;

        [Header("Phase")]
        [SerializeField] StartRoundPhase startRound;
        [SerializeField] BeforeTeamFight beforeTeamFight;

        [SerializeField] GameObject[] cardHand;
        [SerializeField] DeckSystem deckSystem;
        [SerializeField] CardTween cardDraw;

        private RectTransform[] cardHandRect;
        private CardVisual[] cardVizs;
        private CardInstance[] cardInstance;
        private CardInfoBattle _cardInfoViz;
        private PlayerDragLogic _playerDragLogic;
        private Animator anim;
        private GridLayoutGroup gridLayout;

        private float SPACING_X_DEFAULT = 30;
        private const float SPACING_Y_DEFAULT = 0;
        private const float SPACING_X_OFFSET = 10;
        private const float NUMBER_CARD_HAND_DEFAULT = 5;
        private bool isShowHandZone = false;

        public void Setter(CardInfoBattle value, PlayerDragLogic playerDragLogic)
        {
            _cardInfoViz = value;
            _playerDragLogic = playerDragLogic;

            _playerDragLogic.OnBeginDrag += HidenHandZone;
            _playerDragLogic.OnEndDrag += DisplayHandZone;
        }

        private void Awake()
        {
            gridLayout = GetComponent<GridLayoutGroup>();
            SPACING_X_DEFAULT = gridLayout.spacing.x;
            Init();
            AddListerner();
            anim = GetComponent<Animator>();
        }

        private void AddListerner()
        {
            deckSystem.OnDraw += AnimDraw;
            cardDraw.OnEndDraw += LoadHandZone;
            startRound.OnEnterPhase += LoadHandZone;
            deckSystem.ReloadAfterDrop += ReloadOnDrop;
            deckSystem.OnAddCardHand += AnimAdd;
            controlState.OnLeftClick += DisplayHandZone;
            teamFightState.OnLeftClick += DisplayHandZone;
            beforeTeamFight.OnEnterPhase += HidenHandZone;
            dragCardSelected.OnBeginDrag += HidenHandZone;
        }

        private void Init()
        {
            cardVizs = new CardVisual[cardHand.Length];
            cardInstance = new CardInstance[cardHand.Length];
            cardHandRect = new RectTransform[cardHand.Length];

            for (int i = 0; i < cardVizs.Length; i++)
            {
                cardVizs[i] = cardHand[i].GetComponent<CardVisual>();
                cardInstance[i] = cardHand[i].GetComponent<CardInstance>();
                cardHandRect[i] = cardHand[i].GetComponent<RectTransform>();
                cardInstance[i].CardSeleted = dragCardSelected;
                cardInstance[i].CardInfomation = _cardInfoViz;
            }
        }

        private void LoadHandZone()
        {
            SetViz(deckSystem.CardsInHand);
            SetCardsInstance(deckSystem.CardsInHand);
            SortUI(deckSystem.CardsInHand.Count);
            SetActive();

            if (!isShowHandZone) DisplayHandZone();
        }

        private void AnimDraw(Card card)
        {
            Vector3 pos = GetPosCardLeft();
            cardDraw.Move(card, pos);
        }

        private void AnimAdd(Card card)
        {
            Vector3 pos = GetPosCardLeft();
            cardDraw.Move(card, pos, Vector3.zero);
        }

        private Vector3 GetPosCardLeft()
        {
            var cardLeft = cardHand.Where(c => c.activeInHierarchy).ToList().LastOrDefault();
            int index = cardLeft.transform.GetSiblingIndex();
            var pos = cardHandRect[index].localPosition;
            return pos;
        }

        private void ReloadOnDrop(bool reload)
        {
            if (reload)
            {
                LoadHandZone();
            }
            else 
            { 
                if (!isShowHandZone) DisplayHandZone(); 
            }
        }

        private void SortUI(int cardHandCount)
        {
            //If player have 5+ card hand, sort logic will change
            if (cardHandCount > NUMBER_CARD_HAND_DEFAULT)
            {
                float cardOut = cardHandCount - NUMBER_CARD_HAND_DEFAULT;

                float spacingX = SPACING_X_DEFAULT - (cardOut * SPACING_X_OFFSET);

                if (spacingX < 0)
                {
                    spacingX = 0;
                }

                gridLayout.spacing = new Vector2(spacingX, SPACING_Y_DEFAULT);
            }
        }

        private void SetActive()
        {
            for (int i = 0; i < cardVizs.Length; i++)
            {
                if (cardInstance[i].Card != null)
                {
                    cardHand[i].SetActive(true);

                }
                else cardHand[i].SetActive(false);
            }
        }

        private void SetCardsInstance(List<Card> cardsInHand)
        {
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                cardInstance[i].Card = cardsInHand[i];
            }

            for (int i = cardsInHand.Count; i < cardHand.Length; i++)
            {
                cardInstance[i].Card = null;
            }
        }

        public void DisplayHandZone()
        {
            //Need fix this line
            if (PhaseSystem.CurrentPhase as BeforeTeamFight) return;

            isShowHandZone = !isShowHandZone;
            anim.SetBool("IsShow", isShowHandZone);
        }

        // 09/03/2022 0 references
        public void ShowHandZone()
        {
            isShowHandZone = true;
            anim.SetBool("IsShow", isShowHandZone);
        }

        public void HidenHandZone()
        {
            isShowHandZone = false;
            anim.SetBool("IsShow", isShowHandZone);
        }

        private void SetViz(List<Card> cardsInHand)
        {
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                cardVizs[i].SetCard(cardsInHand[i]);
            }
        }

        private void OnDisable()
        {
            RemoveListerner();
        }

        private void RemoveListerner()
        {
            cardDraw.OnEndDraw -= LoadHandZone;
            deckSystem.OnDraw -= AnimDraw;
            startRound.OnEnterPhase -= LoadHandZone;
            deckSystem.ReloadAfterDrop -= ReloadOnDrop;
            deckSystem.OnAddCardHand -= AnimAdd;
            controlState.OnLeftClick -= DisplayHandZone;
            teamFightState.OnLeftClick -= DisplayHandZone;
            beforeTeamFight.OnEnterPhase -= HidenHandZone;
            dragCardSelected.OnBeginDrag -= HidenHandZone;

            //Add by setter
            _playerDragLogic.OnBeginDrag -= HidenHandZone;
            _playerDragLogic.OnEndDrag -= DisplayHandZone;
        }
    }
}

