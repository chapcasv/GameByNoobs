using PH.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] RankSystem rankSystem;
        [SerializeField] PlayerLocalSO playerLocalSO;
        [SerializeField] RewardMatchUI UI;

        private Rank currentRank;
        private float timeToDisplayReward = 1f;
        private bool isDisplay = false;

        private void Awake()
        {
            currentRank = ConvertRank.Form(SaveSystem.LoadRank());
            int coinCurrent = SaveSystem.LoadCoin();
            int diamondCurrent = SaveSystem.LoadDiamond();
            string rankName = rankSystem.GetRank(currentRank.GetRankTier).RankName;
            string level = currentRank.GetRankLevelString();
            int rankPoint = currentRank.CurrentPoint;

            UI.Constructor(rankSystem, playerLocalSO, coinCurrent, diamondCurrent, rankName,level,rankPoint);
        }


        private void Update()
        {
            if (!isDisplay)
            {
                timeToDisplayReward -= Time.deltaTime;
                if (timeToDisplayReward < 0)
                {
                    isDisplay = true;
                    AddReward();
                }
            }
        }


        private void AddReward()
        {
            AddCoinReward();
            AddDiamondReward();
            AddRankPoint();
        }


        private void AddCoinReward()
        {
            int coinRewardValue = ResultMatch.Result.CoinReward;

            playerLocalSO.Coin = SaveSystem.LoadCoin();
            playerLocalSO.Coin += coinRewardValue;
            bool saveSuccessful = playerLocalSO.SaveCoinData(coinRewardValue);

            if (saveSuccessful)
            {
                UI.DisplayCoinReward(coinRewardValue);
            }
        }

        private void AddDiamondReward()
        {
            int diamondRewardValue = ResultMatch.Result.DiamondReward;

            playerLocalSO.Diamond = SaveSystem.LoadDiamond();
            playerLocalSO.Diamond += diamondRewardValue;
            SaveSystem.SaveDiamond(playerLocalSO.Diamond);

            UI.DisplayDiamonReward(diamondRewardValue);
        }

        private void AddRankPoint()
        {
            int bonusPoint = ResultMatch.Result.RankPointReward;
            currentRank.AddRankPoint(bonusPoint);
            playerLocalSO.Rank = currentRank;
            UI.DisplayRank(bonusPoint);

            //Need Fix
            SaveSystem.SaveRank(playerLocalSO);

        }
    }
}

