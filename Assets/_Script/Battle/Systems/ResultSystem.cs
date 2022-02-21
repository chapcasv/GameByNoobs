using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        [SerializeField] ResultLastMatch resultWin;

        private PlayModeRewards _playModeRewards;

        public PlayModeRewards PlayModeRewards { set => _playModeRewards = value; }

        public void PlayerVictory()
        {
            ResultMatch.Result = resultWin;
            ResultMatch.Result.CoinReward = _playModeRewards.GetCoinWinReward;

            OnPlayerVictory?.Invoke();
        }

        public void PlayerDefeated()
        {
            ResultMatch.Result = resultWin;
            ResultMatch.Result.CoinReward = _playModeRewards.GetCoinLoseReward;

            OnPlayerDefeated?.Invoke();
        }
    }
}

