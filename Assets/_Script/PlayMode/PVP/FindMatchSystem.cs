using PH.Loader;
using PH.Save;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class FindMatchSystem : MonoBehaviour
    {
        [SerializeField] PVPMode defaultMode;
        [SerializeField] PlayModeEnemy defaultEnemy;

        [Header("UI")]
        [SerializeField] TextMeshProUGUI currentMode;
        [SerializeField] TMP_Dropdown listDeck;

        [Header("Mode Sub")]
        [SerializeField] ModeSubAI modeSubAI;
        [SerializeField] ModeSubNormal modeSubNormal;
        [SerializeField] ModeSubRank modeSubRank;
        
        [Header("Button")]
        [SerializeField] Button B_Back;
        [SerializeField] Button B_FindMatch;

        [SerializeField] Button B_ModeSubAI;
        [SerializeField] Button B_ModeSubNormal;
        [SerializeField] Button B_ModeSubRank;

        private void Awake()
        {
            Addlistener();
#if UNITY_EDITOR
            CheckCurrentMode();
#endif
            B_ModeSubAI.Select();
            SetModeUpAI();
            InitListDeck();
        }

        private void Addlistener()
        {
            B_Back.onClick.AddListener(() => Back());
            B_FindMatch.onClick.AddListener(() => FindMatch());
            B_ModeSubAI.onClick.AddListener(() => SetModeUpAI());
            B_ModeSubNormal.onClick.AddListener(() => SetModeSubNormal());
            B_ModeSubRank.onClick.AddListener(() => SetModeSubRank());
        }

        private void CheckCurrentMode()
        {
            if(PlayModeData.CurrentMode == null)
            {
                PlayModeData.CurrentMode = defaultMode;
                Debug.LogWarning("Current Mode is null !!!");
            }
        }

        private void InitListDeck()
        {
            var playerDecks = SaveSystem.LoadDecks();

            listDeck.options = new List<TMP_Dropdown.OptionData>();

            foreach (var deck in playerDecks)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData
                {
                    text = deck.deckName
                };

                listDeck.options.Add(optionData);
            }
        }

        private void SetModeUpAI()
        {
            PlayModeData.CurrentMode.ModeSub = modeSubAI;
            currentMode.text = PlayModeData.CurrentMode.ModeSub.Discription;
        }

        private void SetModeSubNormal()
        {
            PlayModeData.CurrentMode.ModeSub = modeSubNormal;
            currentMode.text = PlayModeData.CurrentMode.ModeSub.Discription;
        }

        private void SetModeSubRank()
        {
            PlayModeData.CurrentMode.ModeSub = modeSubRank;
            currentMode.text = PlayModeData.CurrentMode.ModeSub.Discription;
        }

        private void FindMatch()
        {
            int index = listDeck.value;
            CollectionMethods.SetCurrentDeck(index);

            PlayModeData.CurrentMode.ModeSub.SetEnemy(defaultEnemy);
            GoToBattle();
        }

        private void Back() => LoadSystem.Load(SceneSelect.MainMenu);

        private void GoToBattle() => LoadSystem.Load(SceneSelect.Battle);

        private void OnDisable()
        {
            RemoveListener();
        }

        private void RemoveListener()
        {
            B_Back.onClick.RemoveAllListeners();
            B_FindMatch.onClick.RemoveAllListeners();
            B_ModeSubAI.onClick.RemoveAllListeners();
            B_ModeSubNormal.onClick.RemoveAllListeners();
            B_ModeSubRank.onClick.RemoveAllListeners();
        }
    }
}
