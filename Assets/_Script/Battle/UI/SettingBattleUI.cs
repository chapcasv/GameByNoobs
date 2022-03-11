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
                OnSelect += OnClickCallBack;
                main.onClick.AddListener(()=> OnSelect?.Invoke());
            }

            private void OnClickCallBack()
            {
                selectImg.SetActive(true);
                Screen.SetActive(true);
                
            }
            public void DeSelect()
            {
                selectImg.SetActive(false);
                Screen.SetActive(false);
            }
        }
        [SerializeField] private GameObject main;
        [Header("Bot Button")]
        [SerializeField] private Button B_accept;
        [SerializeField] private Button B_close;
        [SerializeField] private Button B_support, B_exit, B_surderer;
        

        [Header("Top Button Tab")]
        [SerializeField] private TopButtonTap B_generalTab;
        [SerializeField] private TopButtonTap B_keybindingTab;
        [SerializeField] private TopButtonTap B_videoTab;
        [SerializeField] private TopButtonTap B_musicTab;

        private IPopUpManager popUpManager;
        
        public override void Initialize()
        {
            base.Initialize();
            ThirdParties.Find<IPopUpManager>(out popUpManager);
            if(B_surderer != null)
            {
                B_surderer.onClick.AddListener(OnSurdererCallBack);
            }
            B_accept.onClick.AddListener(OnCloseSettingCallBack);
            B_support.onClick.AddListener(OnSupportButtonCallBack);
            B_exit.onClick.AddListener(OnExitgameCallBack);
         
            B_close.onClick.AddListener(OnCloseSettingCallBack);
            B_keybindingTab.Initialize();
            B_generalTab.Initialize();
            B_videoTab.Initialize();
            B_musicTab.Initialize();
            
            B_generalTab.OnSelect += OnGeneralSetting;
            B_musicTab.OnSelect += OnMusicSetting;
            B_videoTab.OnSelect += OnAudioSetting;
            B_keybindingTab.OnSelect += OnKeyBindingSetting;
            B_generalTab.OnSelect?.Invoke();
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
            B_musicTab.DeSelect();
            B_videoTab.DeSelect();
            B_keybindingTab.DeSelect();

        }
        private void OnMusicSetting()
        {
            B_generalTab.DeSelect();
            B_videoTab.DeSelect();
            B_keybindingTab.DeSelect();
        }

        private void OnAudioSetting()
        {
            B_generalTab.DeSelect();
            B_musicTab.DeSelect();
            B_keybindingTab.DeSelect();
        }
        private void OnKeyBindingSetting()
        {
            B_musicTab.DeSelect();
            B_videoTab.DeSelect();
            B_generalTab.DeSelect();
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
            popUpManager.ShowPopUpWindow(PopupType.CONFIRMATION, "Bạn muốn thoát trò chơi ?", "Thoát Trò Chơi", () =>
            {
                Application.Quit();
            });

        }
    }
}
