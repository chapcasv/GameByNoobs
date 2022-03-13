using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class BtnScale : MonoBehaviour
    {
        private Button btn;
        private RectTransform rect;

        void Start()
        {
            btn = GetComponent<Button>();
            rect = GetComponent<RectTransform>();
            btn.onClick.AddListener(Scale);
        }

        private void Scale()
        {
            UIExtension.ScaleOnClick(rect, UIExtension.defaultScale);
        }

        private void OnDestroy()
        {
            btn.onClick.RemoveListener(Scale);
        }


    }
}

