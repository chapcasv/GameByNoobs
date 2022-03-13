using System;
using UnityEngine;

namespace PH
{
    [RequireComponent(typeof(DropCardSelected))]
    public class DragCardSelected : MonoBehaviour
    {
        public Action OnBeginDrag;

        [SerializeField] Canvas canvas;

        private CoinSystem _coinSystem;
        private DeckSystem _deckSystem;

        private UITextPopUp _textPopUp;
        private DropCardSelected _drop;
        private RectTransform _transform;
        private CardSelectedVisual _cardViz;
        private Card currentCard;
        private CardInstance cache;
        private float offsetX;

        public Card CurrentCard { set => currentCard = value; }
        public CardInstance CardInstanceCache { set => cache = value; }


        public void Constructor(CoinSystem CS, DeckSystem DS, BoardSystem BS, MemberSystem MS)
        {
            _coinSystem = CS;
            _deckSystem = DS;

            _drop = GetComponent<DropCardSelected>();
            _transform = GetComponent<RectTransform>();
            _cardViz = GetComponent<CardSelectedVisual>();
            _drop.CoinSystem = _coinSystem;
            _drop.BoardSystem = BS;
            _drop.Member = MS;

            gameObject.SetActive(false);
        }


        void Update()
        {
            if (PhaseSystem.CurrentPhase as PlayerControl)
            {
                FollowMouse();
                _drop.MoveRadar();
                _drop.HightLightTileUnder();
                if (Input.GetMouseButtonUp(0)) OnEndDrag();
            }
            else
            {
                VFXManager.Instance.StopHighLightMap();
                BackToHand();
            }
        }

        private void FollowMouse()
        {
            Vector3 pos = Input.mousePosition;
            _transform.position = new Vector3(pos.x + offsetX, pos.y, pos.z);
        }

        /// <summary>
        /// Change offset by screen size
        /// </summary>
        public void CalculatorOffsetX()
        {
            offsetX = _transform.sizeDelta.x * canvas.scaleFactor;
            offsetX /= 2;
        }

        public void LoadCard()
        {
            VFXManager.Instance.HighLightMap();
            _transform.position = Input.mousePosition;
            _cardViz.SetCard(currentCard);
            gameObject.SetActive(true);

            OnBeginDrag?.Invoke();
        }

        private void OnEndDrag()
        {
            VFXManager.Instance.StopHighLightMap();

            int cost = GetCostCurrentCard();
            var type = currentCard.GetCardType();


            if (cost == int.MaxValue) throw new Exception("Cant get card cost");

            if (_drop.CanDrop(cost, type))
            {
                if (_drop.TryDropCard(currentCard))
                {
                    _drop.DecraseCoin(cost);
                    ReLoadHandZone();
                }
                else BackToHand();
            }
            else BackToHand();
        }

        private void ReLoadHandZone()
        {
            VFXManager.Instance.HidenTileUnder();
            cache.Card = currentCard;
            cache.OnDrop(_deckSystem);
            gameObject.SetActive(false);
        }

        private void BackToHand()
        {
            VFXManager.Instance.HidenTileUnder();
            cache.Card = currentCard;
            cache.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private int GetCostCurrentCard()
        {
            int cost = currentCard.Cost;
            return cost;
        }

        public void SetSetUITextPopUp(UITextPopUp value)
        {
            _textPopUp = value;
            _drop.SetPopUp = _textPopUp;
        }
    }
}
