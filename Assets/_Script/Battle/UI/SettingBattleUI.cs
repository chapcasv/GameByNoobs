using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace PH
{
    public class SettingBattleUI : UIScreen
    {
        [SerializeField] private GameObject main;
        [SerializeField] private GameObject Overlay;
        [SerializeField] private Button closeButton;

        public override void Initialize()
        {
            base.Initialize();
            closeButton.onClick.AddListener(OnCloseSettingCallBack);
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
      

    }

}
