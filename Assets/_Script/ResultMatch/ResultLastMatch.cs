using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    public abstract class ResultLastMatch : ScriptableObject
    {
        [SerializeField] string resultText;

        private int coinReward;
        private int diamondReward;

        public string ResultText { get => resultText;}
        public int CoinReward { get => coinReward; set => coinReward = value; }
        public int DiamondReward { get => diamondReward; set => diamondReward = value; }
    }
}

