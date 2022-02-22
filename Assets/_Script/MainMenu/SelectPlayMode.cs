using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PH
{
    public class SelectPlayMode : MonoBehaviour
    {
        [Header("Play Mode")]
        [SerializeField] PVPMode pvpMode;

        [Header("Button")]
        [SerializeField] Button B_BG;
        [SerializeField] Button B_PVPMode;
        [SerializeField] Button B_PVEMode;
        [SerializeField] Button B_CoOpMode;

        private void Awake()
        {
            B_BG.onClick.AddListener(() => Hiden());
            B_PVPMode.onClick.AddListener(() => SetPVPMode());
            B_PVEMode.onClick.AddListener(() => SetPVPMode());
            B_CoOpMode.onClick.AddListener(() => SetPVPMode());

            Hiden();
        }

        private void Hiden() => gameObject.SetActive(false);

        private void SetPVPMode()
        {
            PlayModeData.CurrentMode = pvpMode;
            GoToFindMatch();
        }

        private void GoToFindMatch()
        {
            SceneManager.LoadScene(SceneSelect.FindMatch.ToString());
        }

        private void OnDestroy()
        {
            B_BG.onClick.RemoveAllListeners();
            B_PVPMode.onClick.RemoveAllListeners();
            B_PVEMode.onClick.RemoveAllListeners();
            B_CoOpMode.onClick.RemoveAllListeners();
        }
    }
}

