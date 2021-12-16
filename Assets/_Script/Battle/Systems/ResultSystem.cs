using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Result System")]
    public class ResultSystem : ScriptableObject
    {
        public event Action OnPlayerVictory;
        public event Action OnPlayerDefeated;

 
        public void PlayerVictory()
        {
            AddRewardPassRaid();
            OnPlayerVictory?.Invoke();
        }
        private void AddRewardPassRaid() { }

        public void PlayerDefeated()
        {
            OnPlayerDefeated?.Invoke();
        }
    }
}

