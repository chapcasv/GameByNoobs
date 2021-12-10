using UnityEngine;
using UnityEngine.EventSystems;


namespace PH
{
    public class CardInstance : MonoBehaviour, IBeginDragHandler, IPointerDownHandler, IDragHandler, IEndDragHandler
    {

        [SerializeField] DragCardSeleted cardSeleted;
        public Card Card { get; set; }


        public void OnBeginDrag(PointerEventData eventData)
        {
            cardSeleted.CurrentCard = Card;
            this.Card = null;
            cardSeleted.Cache = this;
            cardSeleted.LoadCard();
            gameObject.SetActive(false);

        }
        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnEndDrag(PointerEventData eventData) { }


    }
}

