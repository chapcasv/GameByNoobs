using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using PH.Save;

namespace PH
{
    public class ResultMatchUI : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI coinRewardText;
        [SerializeField] TextMeshProUGUI rankReward;
        [SerializeField] Image rankIcon;

        [SerializeField] Button b_back;
        [SerializeField] Button b_againt;

        private PlayerLocalSO _playerLocalSO;
        private RankSystem _rankSystem;

        void Start()
        {
            AddListerner();
            DisplayCurrentRankIcon();
        }

        public void Constructor(RankSystem rs, PlayerLocalSO playerLocalSO)
        {
            _rankSystem = rs;
            _playerLocalSO = playerLocalSO;
        }

        public void DisplayCoinReward(int coinRewardValue)
        {
            coinRewardText.text = "Tiền Xu +"+ coinRewardValue;
        }

        private void DisplayCurrentRankIcon()
        {
            Rank currentRank = ConvertRank.Form(SaveSystem.LoadRank());
            RankInstance instance = _rankSystem.GetRank(currentRank.GetRankTier);
            rankIcon.sprite = instance.Icon;
        }

        public void DisplayRank(string tier, string level, string bonus)
        {
            rankReward.text = $"{tier} {level} + {bonus}đ";
        }


        private void AddListerner()
        {
            b_back.onClick.AddListener(() => SceneManager.LoadScene(SceneSelect.MainMenu.ToString()));
            b_againt.onClick.AddListener(() => SceneManager.LoadScene(SceneSelect.PVP.ToString()));
        }

        private void OnDisable()
        {
            b_back.onClick.RemoveAllListeners();
            b_againt.onClick.RemoveAllListeners();
        }
    }
}


