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

        public int GetCoinLoseReward => coinLoseReward;
        
        public int GetCoinWinReward => coinWinReward;
    }
}


