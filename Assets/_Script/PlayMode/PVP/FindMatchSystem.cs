using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PH
{
    public class FindMatchSystem : MonoBehaviour
    {
        [SerializeField] PVPMode defaultMode;
        [SerializeField] PlayModeEnemy defaultEnemy;

        [Header("UI")]
        [SerializeField] TextMeshProUGUI currentMode;

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
            CheckCurrentMode();
            B_ModeSubAI.Select();
            SetModeUpAI(); 
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
#if UNITY_EDITOR
                PlayModeData.CurrentMode = defaultMode;
                Debug.LogWarning("Current Mode is null !!!");
#endif
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
            PlayModeData.CurrentMode.ModeSub.SetEnemy(defaultEnemy);
            GoToBattle();
        }

        private void Back()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }

        private void GoToBattle()
        {
            SceneManager.LoadScene(SceneSelect.Battle.ToString());
        }

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
