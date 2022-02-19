using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace PH
{
    public class CardCollection : MonoBehaviour, IPointerDownHandler
    {
       
        private CardIVizCollection _cardInfoViz;
        public Card Card { get; set; }
      
        public CardIVizCollection CardInfomation { set => _cardInfoViz = value; }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.clickCount > 1)
            {
                _cardInfoViz.SetCard(Card);
            }
        }

        //Only use OnBeginDrag
        public void OnDrag(PointerEventData eventData) { }

        public void OnEndDrag(PointerEventData eventData) { }
    }

}
