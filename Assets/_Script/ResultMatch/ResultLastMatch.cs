using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    public abstract class ResultLastMatch : ScriptableObject
    {
        private int coinReward;
        [SerializeField] string resultText;

        public string ResultText { get => resultText;}
        public int CoinReward { get => coinReward; set => coinReward = value; }
    }
}

