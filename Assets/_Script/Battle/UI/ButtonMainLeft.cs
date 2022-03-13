using PH.State;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class ButtonMainLeft : MonoBehaviour
    {
        private Button btn;
        private RectTransform rect;

        void Start()
        {
            btn = GetComponent<Button>();
            rect = GetComponent<RectTransform>();
            btn.onClick.AddListener(Click);
        }

        private void Click()
        {
            StateSystem.CurrentState.LeftClick();
            UIExtension.ScaleOnClick(rect, UIExtension.defaultScale);
        }


        private void OnDestroy()
        {
            btn.onClick.RemoveListener(Click);
        }
    }
}

