using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class ConvertRank 
    {
        public static PlayerRank ToPlayerRank(Rank rank)
        {
            PlayerRank player = new PlayerRank()
            {
                rankTier = rank.GetRankTier,
                rankLevel = rank.GetRankLevel,
                currentPoint = rank.CurrentPoint
            };
            return player;
        }

        public static Rank Form(PlayerRank playerRank)
        {
            int rankTier = playerRank.rankTier;
            int rankLevel = playerRank.rankLevel;
            int currentPoint = playerRank.currentPoint;
            Rank rank = new Rank(rankTier, rankLevel, currentPoint);
            return rank;
        }
    }

}
