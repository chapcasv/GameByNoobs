using PH.State;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class ButtonMainRight : MonoBehaviour
    {
        private Button btn;

        // Start is called before the first frame update
        void Start()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(Click);
        }

        private void Click()
        {
            StateSystem.CurrentState.RightClick();
        }
    }
}

