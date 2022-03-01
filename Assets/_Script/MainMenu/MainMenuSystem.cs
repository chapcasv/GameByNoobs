using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using PH.Save;
using System;

namespace PH
{
    public class MainMenuSystem : MonoBehaviour
    {
        [SerializeField] PlayerLocalSO player;
        [SerializeField] RankSystem rankSystem;

        [SerializeField] Button B_Play;
        [SerializeField] Button B_Collection;
        [SerializeField] Button B_Deck;
        [Header("UI")]
        [SerializeField] TextMeshProUGUI coin;
        [SerializeField] TextMeshProUGUI playerName;
        [SerializeField] GameObject selectPlayMode;
        [Header("Rank")]
        [SerializeField] Image rankIcon;
        [SerializeField] TextMeshProUGUI rankTierLevel;

        private void Awake()
        {
            B_Play.onClick.AddListener(() => ShowPlayMode());
            B_Collection.onClick.AddListener(() => GoToCollection());
            B_Deck.onClick.AddListener(GoToDeck);
        }

        private void GoToDeck()
        {
            SceneManager.LoadScene(SceneSelect.DeckLibrary.ToString());
        }

        void Start()
        {
            LoadPlayer();
        }

        private void LoadPlayer()
        {
            coin.text = SaveSystem.LoadCoin().ToString();
            playerName.text = SaveSystem.LoadName();

            LoadRank();
        }

        private void LoadRank()
        {
            Rank rank = ConvertRank.Form(SaveSystem.LoadRank());

            RankInstance rankInstance = rankSystem.GetRank(rank.GetRankTier);

            string tier = rankInstance.RankName;
            string level = rank.GetRankLevelString();

            rankTierLevel.text = tier + " " + level;
            rankIcon.sprite = rankInstance.Icon;
        }

        public void ShowPlayMode()
        {
            selectPlayMode.SetActive(true);
        }

        public void GoToCollection()
        {
            SceneManager.LoadScene(SceneSelect.CardLibrary.ToString());
        }

        private void OnDisable()
        {
            B_Collection.onClick.RemoveAllListeners();
            B_Play.onClick.RemoveAllListeners();
        }
    }
}

