using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
namespace PH
{
    public class MessagePopUp : MonoBehaviour, IPopupWindow
    {
        [SerializeField] private PopupType type = PopupType.MESSAGE;
        public PopupType Type => type;


        [SerializeField] private TextMeshProUGUI MessageText;
        [SerializeField] private Button B_close;

        private void Start()
        {
            B_close.onClick.AddListener(Hide);
        }
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void Show(string message = "", Action OnAction = null)
        {
            this.gameObject.SetActive(true);
            this.MessageText.text = message;
        }
    }

}
