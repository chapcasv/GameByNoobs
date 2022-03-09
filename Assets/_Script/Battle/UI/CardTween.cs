using DG.Tweening;
using System;
using UnityEngine;

namespace PH
{
    public class CardTween : MonoBehaviour
    {
        public event Action OnEndDraw;

        [SerializeField] CardVisual cardFront;
        [SerializeField] RectTransform cardBack;

        private const float TIME_TO_CENTER = 0.7f;
        private const float TIME_TO_HANDZONE = 0.5f;
        private const float STAY_CENTER = 0.4f;

        //private Camera cam;
        private Vector3 oldPos;
        private Vector3 oldRotation;
        private RectTransform rect;
        private bool flip = false;

        private void Awake()
        {
            //cam = Camera.main;
            rect = GetComponent<RectTransform>();
            oldPos = rect.position;
            oldRotation = rect.eulerAngles;
            cardBack.gameObject.SetActive(false);
            cardFront.gameObject.SetActive(false);
        }

        public void Move(Card card, Vector3 posHandZone, Vector3 posCreate)
        {
            cardFront.SetCard(card);

            ActiveFrontByPos(posCreate);

            cardBack.gameObject.SetActive(false);

            rect.DOScale(Vector3.one, STAY_CENTER).SetEase(Ease.OutBack)
                .OnComplete(() => ToHandZone(posHandZone));
        }

        private void ActiveFrontByPos(Vector3 posCreate)
        {
            rect.localScale = Vector3.zero;
            rect.anchoredPosition = posCreate;
            rect.eulerAngles = Vector2.zero;

            cardFront.gameObject.SetActive(true);
        }

        public void Move(Card card, Vector3 posHandZone)
        {
            cardFront.SetCard(card);
            cardBack.gameObject.SetActive(true);

            rect.DORotate(Vector3.zero, TIME_TO_CENTER);

            rect.DOAnchorPos(Vector2.zero, TIME_TO_CENTER, true)
                .OnComplete(() => ToHandZone(posHandZone));
        }

        private void ToHandZone(Vector3 posHandZone)
        {
            rect.DOAnchorPos(posHandZone, TIME_TO_HANDZONE, true).SetDelay(STAY_CENTER).OnComplete(Fade);
        }


        private void Fade()
        {
            cardBack.gameObject.SetActive(false);
            cardFront.gameObject.SetActive(false);

            flip = false;
            rect.eulerAngles = oldRotation;
            rect.position = oldPos;

            //reload Handzone
            OnEndDraw?.Invoke();
        }

        private void Update()
        {
            if(rect.eulerAngles.y <= 90 && !flip)
            {
                flip = true;
                cardBack.gameObject.SetActive(false);
                cardFront.gameObject.SetActive(true);
            }
        }
    }
}

