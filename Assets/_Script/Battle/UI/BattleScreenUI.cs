using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class BattleScreenUI : UIScreen
    {
        [System.Serializable]
        public class BattleScreenElement
        {
            public Button ElementButton;
            public UIScreen Screen;
            public void Initialize()
            {
                ElementButton.onClick.AddListener(
                    () =>
                    {
                        Screen.Show();
                    }
                );
                Screen.Initialize();
            }

        }
        [SerializeField] private BattleScreenElement SettingElement;

        private void Start()
        {
            SettingElement.Initialize();
        }

    }

}
