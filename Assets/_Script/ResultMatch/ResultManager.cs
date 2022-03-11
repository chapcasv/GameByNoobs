using PH.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] RankSystem rankSystem;
        [SerializeField] PlayerLocalSO playerLocalSO;
        [SerializeField] ResultMatchUI UI;

        private void Awake()
        {
            UI.Constructor(rankSystem, playerLocalSO);
        }

        private void Start()
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


        }

        private void AddRankPoint()
        {
            Rank currentRank = ConvertRank.Form(SaveSystem.LoadRank());

            int bonusPoint = 90;

            currentRank.AddRankPoint(bonusPoint);

            playerLocalSO.Rank = currentRank;

            string rankName = rankSystem.GetRank(currentRank.GetRankTier).RankName;
            string level = currentRank.GetRankLevelString();
            UI.DisplayRank(rankName,level, bonusPoint.ToString());

            //Need Fix
            SaveSystem.SaveRank(playerLocalSO);

        }
    }
}

