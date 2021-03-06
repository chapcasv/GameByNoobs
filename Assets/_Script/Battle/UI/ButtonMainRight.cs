using PH.State;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class ButtonMainRight : MonoBehaviour
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
            StateSystem.CurrentState.RightClick();
            UIExtension.ScaleOnClick(rect,UIExtension.defaultScale);
        }

        private void OnDestroy()
        {
            btn.onClick.RemoveListener(Click);
        }
    }
}

