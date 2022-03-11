using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PH
{

    public interface IPopUpManager
    {
        void Initialize();

        void ShowPopUpWindow(PopupType type, string message = "", string title = "", Action OnAction = null);
    }
    public enum PopupType
    {
        MESSAGE,
        CONFIRMATION
    }
    public interface IPopupWindow
    {
        PopupType Type { get; }
        void Show(string message = "", string title = "", Action OnAction = null);
        void Hide();
    }
        

}
