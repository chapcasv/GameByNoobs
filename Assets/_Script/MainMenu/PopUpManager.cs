using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PH
{

    public interface IPopUpManager
    {
        //quản lí các popup window, duy nhất 
        void Initialize();

        void ShowPopUpWindow(PopupType type, string message = "", string title = "", Action OnClickPopup = null);

        void ShowPopUpConfirm(string message = "", string title = "", Action OnConfirm = null, Action Cancel = null);
    }
    public enum PopupType
    {
        MESSAGE,
        CONFIRMATION
    }


    public interface IPopupWindow
    {
        //tất cả pop up window phải implement this.
        PopupType Type { get; }
        void Show(string message = "", string title = "", Action OnClickPopup = null);
        void Hide();
        
    }
    public interface IConfirmWindow
    {
        //tùy theo mục đích của pop up cần confirm.
        void GetEvent(Action OnConfirm, Action OnCancel);
    }
        

}
