using UnityEngine;
using System;
using UnityEngine.Events;

namespace PH 
{   
    [RequireComponent(typeof(DropCardSelected))]
    public class DragCardSelected : MonoBehaviour
    {
        public Action OnBeginDrag;

        private CoinSystem _coinSystem;
        private DeckSystem _deckSystem;
        private GetBaseProperties _getBaseProperties;

        private DropCardSelected _drop;
        private Transform _transform;
        private CardSelectedVisual _cardViz;
        private Card currentCard;
        private CardInstance cache;

        public Card CurrentCard { set => currentCard = value; }
        public CardInstance CardInstanceCache {set => cache = value; }

        public void Constructor(CoinSystem CS, DeckSystem DS, GetBaseProperties GBP)
        {
            _coinSystem = CS;
            _deckSystem = DS;
            _getBaseProperties = GBP;

            _drop = GetComponent<DropCardSelected>();
            _transform = GetComponent<Transform>();
            _cardViz = GetComponent<CardSelectedVisual>();
            _drop.CoinSystem = _coinSystem;
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (PhaseSystem.CurrentPhase as PlayerControl)
            {
                _transform.position = Input.mousePosition;
                _drop.MoveRadar();
                _drop.HightLightTileUnder();
                if (Input.GetMouseButtonUp(0)) OnEndDrag();
            }
            else
            {
                EffectGridMap.Instance.StopHighLightMap();
                BackToHand();
            }
        }

        public void LoadCard()
        {
            EffectGridMap.Instance.HighLightMap();
            _transform.position = Input.mousePosition;
            _cardViz.SetCard(currentCard);
            gameObject.SetActive(true);

            OnBeginDrag?.Invoke();
        }

        private void OnEndDrag()
        {
            EffectGridMap.Instance.StopHighLightMap();

            int cost = GetCostCurrentCard();

            if (cost == int.MaxValue) throw new System.Exception("Cant get card cost");

            if (_drop.CanDrop(cost))
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
            cache.Card = currentCard;
            cache.OnDrop(_deckSystem);
            gameObject.SetActive(false);
        }

        private void BackToHand()
        {
            cache.Card = currentCard;
            cache.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private int GetCostCurrentCard()
        {
            int cost = _getBaseProperties.GetCost(currentCard);
            return cost;
        }
    }
}
