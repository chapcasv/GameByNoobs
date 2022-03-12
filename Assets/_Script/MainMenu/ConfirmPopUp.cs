using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class ConfirmPopUp : MonoBehaviour, IPopupWindow, IConfirmWindow
    {
        [SerializeField] private Button B_confirm;
        [SerializeField] private Button B_cancel;
        [SerializeField] private TextMeshProUGUI comfirmText;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private RectTransform main;
        [SerializeField] Vector2 statPos;

        [SerializeField] private PopupType type = PopupType.CONFIRMATION;
        [SerializeField]float during = 0.3f;
        public PopupType Type => type;


        private event Action OnConfirm;
        private event Action OnCancel;
        void OnEnable()
        {
            main.transform.localPosition = statPos;
            main.localScale = new Vector2(0.35f, 0.35f);
            B_cancel.onClick.AddListener(Cancel);
            B_confirm.onClick.AddListener(Confirm);
        }

        public void Show(string message = "", string title = "", Action OnConfirm = null)
        {
            comfirmText.text = message;
            titleText.text = title;
            this.gameObject.SetActive(true);
            ShowAnimation();
        }
        private void ShowAnimation()
        {
            main.transform.DOLocalMove(Vector2.zero, during);
            main.transform.DOScale(1f, during);
        }

        public void GetEvent(Action OnConfirm, Action OnCancel)
        {
            this.OnConfirm = OnConfirm;
            this.OnCancel = OnCancel;
        }
        private void Cancel()
        {
            OnCancel?.Invoke();
            Hide();
        }
        private void Confirm()
        {
            OnConfirm?.Invoke();
            Hide();
        }
        public void Hide()
        {
            this.gameObject.SetActive(false);

        }

        private void OnDisable()
        {
            B_cancel.onClick.RemoveAllListeners();
            B_confirm.onClick.RemoveAllListeners();
        }
    }

}
