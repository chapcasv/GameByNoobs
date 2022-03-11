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

    /// <summary>
    /// Need fix class
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Result System")]
    public class ResultSystem : ScriptableObject
    {   
        public event Action OnPlayerVictory;
        public event Action OnPlayerDefeated;

        [SerializeField] ResultLastMatch resultWin;

        private PlayModeRewards _playModeRewards;

        public PlayModeRewards PlayModeRewards { set => _playModeRewards = value; }

        public void PlayerVictory()
        {
            ResultMatch.Result = resultWin;
            ResultMatch.Result.CoinReward = _playModeRewards.GetCoinWinReward;
            ResultMatch.Result.DiamondReward = _playModeRewards.GetDiamondWinReward;

            OnPlayerVictory?.Invoke();
        }

        public void PlayerDefeated()
        {
            ResultMatch.Result = resultWin;
            ResultMatch.Result.CoinReward = _playModeRewards.GetCoinLoseReward;
            ResultMatch.Result.DiamondReward = _playModeRewards.GetDiamondLoseReward;

            OnPlayerDefeated?.Invoke();
        }
    }
}

