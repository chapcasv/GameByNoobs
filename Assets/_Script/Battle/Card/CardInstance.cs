using UnityEngine;
using UnityEngine.EventSystems;


namespace PH
{
    public class CardInstance : MonoBehaviour, IBeginDragHandler, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        private DragCardSelected _cardSeleted;
        private CardInfoVisual _cardInfoViz;
        public Card Card { get; set; }
        public DragCardSelected CardSeleted { get => _cardSeleted; set => _cardSeleted = value; }
        public CardInfoVisual CardInfomation {set => _cardInfoViz = value; }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!(PhaseSystem.CurrentPhase as PlayerControl)) 
                return;

            _cardInfoViz.gameObject.SetActive(false);
            CardSeleted.CurrentCard = Card;
            this.Card = null;
            CardSeleted.CardInstanceCache = this;
            CardSeleted.LoadCard();
            gameObject.SetActive(false);
        }

        public void OnDrop(DeckSystem deckSystem) => deckSystem.DropCard(Card);   

        public void OnPointerDown(PointerEventData eventData)
        {
            _cardInfoViz.SetCard(Card);
        }

        //Only use OnBeginDrag
        public void OnDrag(PointerEventData eventData) { }

        public void OnEndDrag(PointerEventData eventData) { }


    }
}

