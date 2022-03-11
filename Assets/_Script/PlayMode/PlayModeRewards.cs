using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Play Mode/Rewards")]
    public class PlayModeRewards : ScriptableObject
    {
        [SerializeField] int coinLoseReward;
        [SerializeField] int coinWinReward;
        [SerializeField] int diamondLoseReward;
        [SerializeField] int diamondWinReward;

        public int GetCoinLoseReward => coinLoseReward;
        public int GetDiamondLoseReward => diamondLoseReward;
        
        public int GetCoinWinReward => coinWinReward;

        public int GetDiamondWinReward => diamondWinReward;
    }
}


