using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Rank/Rank System")]
    public class RankSystem : ScriptableObject
    {
        [SerializeField] RankInstance[] allRanks;

        public RankInstance GetRank(int rankTier)
        {
            return allRanks[rankTier];
        }
    }
}

