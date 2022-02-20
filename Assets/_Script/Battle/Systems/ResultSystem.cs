using System;
using UnityEngine;

namespace PH
{
    public enum ResultLastRound
    {
        PlayerWin,
        PlayerLose,
        Draw
    }

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

