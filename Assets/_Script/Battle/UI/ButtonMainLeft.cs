using PH.State;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class ButtonMainLeft : MonoBehaviour
    {
        private Button btn;

        void Start()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(Click);
        }

        private void Click()
        {
            StateSystem.CurrentState.LeftClick();
        }
    }
}

