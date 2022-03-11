using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class PopUpMaster : MonoBehaviour, IPopUpManager
    {
        [SerializeField]protected IPopupWindow[] windows;
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

            for (int i = 0; i < windows.Length; i++)
            {
                windows[i].Hide();
            }
        }

        public void ShowPopUpWindow(PopupType type, string message = "", string title = "", Action OnAction = null)
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
            window.Show(message,title, OnAction);
        }
        
    }
}

