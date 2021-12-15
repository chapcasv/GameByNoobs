using UnityEngine;
using UnityEngine.EventSystems;


namespace PH
{
    public class CardInstance : MonoBehaviour, IBeginDragHandler, IPointerDownHandler, IDragHandler, IEndDragHandler
    {

        [SerializeField] DragCardSelected cardSeleted;
        public Card Card { get; set; }

        public void OnBeginDrag(PointerEventData eventData)
        {
            cardSeleted.CurrentCard = Card;
            this.Card = null;
            cardSeleted.CardInstanceCache = this;
            cardSeleted.LoadCard();
            gameObject.SetActive(false);
        }

        public void OnDrop(LocalPlayer localPlayer) => localPlayer.DropCard(Card);   

        public void OnPointerDown(PointerEventData eventData)
        {

        }

        //Only use OnBeginDrag
        public void OnDrag(PointerEventData eventData) { }

        public void OnEndDrag(PointerEventData eventData) { }


    }
}
