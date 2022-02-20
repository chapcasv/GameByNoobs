using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
 
namespace PH
{
    public class SettingBattleUI : UIScreen
    {
        [System.Serializable]
        public class TopButtonTap
        {
            public Button main;
            public GameObject selectImg;
            public Action OnSelect;
            public GameObject Screen;
            public void Initialize()
            {
                main.onClick.AddListener(OnClickCallBack);
            }

            private void OnClickCallBack()
            {
                selectImg.SetActive(true);
                Screen.SetActive(true);
                OnSelect?.Invoke();
            }
            public void DeSelect()
            {
                selectImg.SetActive(false);
                Screen.SetActive(false);
            }
        }
        [SerializeField] private GameObject main;
        [Header("Bot Button")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button supportButton, exitGameButton, surdererButton;
        

        [Header("Top Button Tab")]
        [SerializeField] private TopButtonTap generalButtonTab;
        [SerializeField] private TopButtonTap musicButtonTab;
        [SerializeField] private TopButtonTap videoButtonTab;
        [SerializeField] private TopButtonTap keyBindingButtonTab;
        
        public override void Initialize()
        {
            base.Initialize();
            closeButton.onClick.AddListener(OnCloseSettingCallBack);
            supportButton.onClick.AddListener(OnSupportButtonCallBack);
            exitGameButton.onClick.AddListener(OnExitgameCallBack);
            surdererButton.onClick.AddListener(OnSurdererCallBack);

            keyBindingButtonTab.Initialize();
            generalButtonTab.Initialize();
            videoButtonTab.Initialize();
            musicButtonTab.Initialize();
            
            generalButtonTab.OnSelect = OnGeneralSetting;
            musicButtonTab.OnSelect = OnMusicSetting;
            videoButtonTab.OnSelect = OnAudioSetting;
            keyBindingButtonTab.OnSelect = OnKeyBindingSetting;
        }

       

        private void OnSurdererCallBack()
        {
            throw new NotImplementedException();
        }

       

        private void OnSupportButtonCallBack()
        {
            throw new NotImplementedException();
        }

        private void OnGeneralSetting()
        {
            musicButtonTab.DeSelect();
            videoButtonTab.DeSelect();
            keyBindingButtonTab.DeSelect();

        }
        private void OnMusicSetting()
        {
            generalButtonTab.DeSelect();
            videoButtonTab.DeSelect();
            keyBindingButtonTab.DeSelect();
        }

        private void OnAudioSetting()
        {
            generalButtonTab.DeSelect();
            musicButtonTab.DeSelect();
            keyBindingButtonTab.DeSelect();
        }
        private void OnKeyBindingSetting()
        {
            musicButtonTab.DeSelect();
            videoButtonTab.DeSelect();
            generalButtonTab.DeSelect();
        }

        private void OnCloseSettingCallBack()
        {
            this.Hide();
        }

        public override void Show()
        {
            base.Show();
            main.transform.localScale = Vector3.zero;
            main.transform.DOScale(1.0f, 0.3f);
        }

        public override void Hide()
        {

            main.transform.localScale = Vector3.one;
            main.transform.DOScale(0.0f, 0.3f).OnComplete(
                () =>
                {
                    base.Hide();
                }
                );
        }

        private void OnExitgameCallBack()
        {
            Application.Quit();
        }
    }
}
