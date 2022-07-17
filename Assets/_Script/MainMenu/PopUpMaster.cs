using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class PopUpMaster : MonoBehaviour, IPopUpManager
    {
        protected IConfirmWindow confirm;
        protected IPopupWindow[] windows;
        private void Awake()
        {
            ThirdParties.Register<IPopUpManager>(this);
        }
        private void Start()
        {
          
            Initialize();
            
        }
        public void Initialize()
        {
            windows = GetComponentsInChildren<IPopupWindow>();
            confirm = GetComponentInChildren<IConfirmWindow>();
            for (int i = 0; i < windows.Length; i++)
            {
                windows[i].Hide();
            }
        }

        public void ShowPopUpWindow(PopupType type, string message = "", string title = "", Action OnClick = null)
        {
            IPopupWindow window = null;

            for (int i = 0; i < windows.Length; i++)
            {
                if(windows[i].Type == type)
                {
                    window = windows[i];
                }
                windows[i].Hide();
            }
            window.Show(message,title, OnClick);
        }

        public void ShowPopUpConfirm(string message = "", string title = "", Action OnConfirm = null, Action OnCancel = null)
        {
            ShowPopUpWindow(PopupType.CONFIRMATION, message, title);
            confirm.GetEvent(OnConfirm, OnCancel);
        }
        private void OnDestroy()
        {
            ThirdParties.Unregister<IPopUpManager>();
        }
    }
}

