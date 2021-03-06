using UnityEngine;
using UnityEngine.EventSystems;


namespace PH
{
    public class CardInstance : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        private DragCardSelected _cardSeleted;
        private CardInfoBattle _cardInfoViz;
        public Card Card { get; set; }
        public DragCardSelected CardSeleted { get => _cardSeleted; set => _cardSeleted = value; }
        public CardInfoBattle CardInfomation {set => _cardInfoViz = value; }

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
        //Only use OnBeginDrag
        public void OnDrag(PointerEventData eventData) { }

        public void OnEndDrag(PointerEventData eventData) { }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount > 1)
            {
                _cardInfoViz.SetCard(Card);
            }
        }
    }
}

