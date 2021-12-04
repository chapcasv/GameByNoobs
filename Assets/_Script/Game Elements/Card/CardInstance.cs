using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GameElements;
using UnityEngine.EventSystems;

namespace PH
{
    public class CardInstance : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        [SerializeField] GameElementLogic _currentLogic;

        public LayerMask mask;
        public Canvas canvas;
        public GameObject radar;
        public Card card;
        [HideInInspector] public RectTransform rect;
        [HideInInspector] public CanvasGroup canvasGr;
        [HideInInspector] public Camera cam;


        private void Start()
        {
            rect = GetComponent<RectTransform>();
            canvasGr = GetComponent<CanvasGroup>();
            cam = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _currentLogic.OnBeginDrag(this,eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _currentLogic.OnDrag(this,eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
           _currentLogic.OnEndDrag(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _currentLogic.OnClick(this);
        }
    }
}

