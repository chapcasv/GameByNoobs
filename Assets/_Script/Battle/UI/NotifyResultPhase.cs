using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class NotifyResultPhase : MonoBehaviour
    {
        [SerializeField] Image label;
        [SerializeField] TextMeshProUGUI resultText;
        [SerializeField] TextMeshProUGUI lpLost;
        [SerializeField] Ease ease;

        private const float FADE_SPEED = 0.3f;
        private const float SCALE_SPEED = 0.7f;
        private readonly Vector2 SCALE = new Vector2(0.7f, 0.8f);
        private RectTransform rect;
        private Vector3 scale;
        private Color originalColor;

        void Start()
        {
            rect = GetComponent<RectTransform>();
            scale = rect.localScale;
            originalColor = label.color;
            gameObject.SetActive(false);
        }

        public void SetLose(int totalDmg)
        {
            label.color = Color.gray;
            resultText.text = " Thất Bại";
            lpLost.text = "Nhận sát thương từ địch: "+ totalDmg;
            gameObject.SetActive(true);

            ScaleFirst();
        }

        public void SetWin(int totalDmg)
        {   
            label.color = Color.yellow;
            resultText.text = " Chiến Thắng";
            lpLost.text = "Gây sát thương lên địch: "+ totalDmg;
            gameObject.SetActive(true);

            ScaleFirst();
        }

        private void ScaleFirst()
        {
            rect.DOScale(SCALE, SCALE_SPEED).SetEase(ease).OnComplete(Fade);
        }

        private void Fade()
        {
            resultText.DOFade(0, FADE_SPEED).SetDelay(0.3f);
            lpLost.DOFade(0, FADE_SPEED).SetDelay(0.3f);
            label.DOFade(0, FADE_SPEED).SetDelay(0.3f).OnComplete(Reload);
        }

        private void Reload()
        {
            gameObject.SetActive(false);
            rect.localScale = scale;
            label.color = originalColor;
            resultText.color = originalColor;
            lpLost.color = originalColor;
        }
    }
}

