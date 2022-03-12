using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PH
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UITextPopUp : MonoBehaviour
    {
        [SerializeField] float moveSpeed;
        [SerializeField] float delayIdle;

        private TextMeshProUGUI text;
        private Color color;
        private Vector2 originalPos;
        private bool textUsing;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            ThirdParties.Register<UITextPopUp>(this);

            Cache();
            gameObject.SetActive(false);
        }

        private void Cache()
        {
            originalPos = text.rectTransform.anchoredPosition;
            color = text.color;
            textUsing = false;
        }

        public void Set(string value)
        {
            if (textUsing) return;

            textUsing = true;
            text.text = value;
            gameObject.SetActive(true);

            text.rectTransform.DOAnchorPosY(0, moveSpeed).SetDelay(delayIdle);
            text.DOFade(0, moveSpeed).SetDelay(moveSpeed/2 + delayIdle).OnComplete(Complete);
        }

        private void Complete()
        {
            gameObject.SetActive(false);
            textUsing = false;
            text.color = color;
            text.rectTransform.anchoredPosition = originalPos;
        }
    }
}

