using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class ConfirmPopUp : MonoBehaviour, IPopupWindow
    {
        [SerializeField] private Button B_confirm;
        [SerializeField] private Button B_close;
        [SerializeField] private TextMeshProUGUI comfirmText;

        [SerializeField] private PopupType type = PopupType.CONFIRMATION;
        public PopupType Type => type;


        private Action OnAction;

        void Start()
        {
            B_close.onClick.AddListener(Hide);
            B_confirm.onClick.AddListener(ConfirmCallBack);
        }
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void Show(string message = "", Action OnAction = null)
        {
            comfirmText.text = message;
            this.gameObject.SetActive(true);
            this.OnAction = OnAction;
        }
        private void ConfirmCallBack()
        {
            OnAction?.Invoke();
            this.gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            B_close.onClick.RemoveListener(Hide);
            B_confirm.onClick.RemoveListener(ConfirmCallBack);
        }
    }

}
