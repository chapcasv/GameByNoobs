using System;

namespace PH
{
    
    public class Rank 
    {
        protected const int LEVEL_TO_NEXT_TIER = 5;
        protected const int POINT_TO_NEXT_RANK = 100;

        protected int rankTier;
        protected int rankLevel;
        protected int currentPoint;

        public int CurrentPoint => currentPoint;
        public int GetRankLevel => rankLevel;
        public int GetRankTier => rankTier;


        public Rank()
        {
            rankTier = 0;
            rankLevel = 0;
            currentPoint = 0;
        }

        public Rank(int rankTier, int rankLevel, int currentPoint)
        {
            this.rankTier = rankTier;
            this.rankLevel = rankLevel;
            this.currentPoint = currentPoint;
        }

        public void AddRankPoint(int value)
        {   
            currentPoint += value;

            while(currentPoint >= POINT_TO_NEXT_RANK)
            {
                UpRankLevel();
                currentPoint -= POINT_TO_NEXT_RANK;
            }
        }

        private void UpRankLevel()
        {
            rankLevel++;

            if (rankLevel >= LEVEL_TO_NEXT_TIER)
            {
                UpTier();
            }
        }

        private void UpTier()
        {
            if (rankTier == GameConst.RankTierLimit) return;

            rankTier++;
            rankLevel = 0;
        }

        public string GetRankLevelString()
        {
            switch (rankLevel)
            {
                case 0:
                    return "V";
                case 1:
                    return "IV";
                case 2:
                    return "III";
                case 3:
                    return "II";
                case 4:
                    return "I";
                default:
                    throw new Exception("Rank level out range");
            }       
        }
    }
}


