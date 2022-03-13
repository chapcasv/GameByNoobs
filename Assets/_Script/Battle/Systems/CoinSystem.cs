using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Coin System")]
    public class CoinSystem : ScriptableObject
    {
        public event Action OnCoinValueChange;
        public event Action OnEnemyCoinChange;
        private const int START_COIN = 1; //Only for test

        private int _playerCoin;
        private int _enemyCoin;

        public int GetCoin() => _playerCoin;
        public int GetEnemyCoin() => _enemyCoin;

        public void IncreasePlayer(int value)
        {
            _playerCoin += value;
            OnCoinValueChange?.Invoke();
        }

        public void DecreasePlayer(int value)
        {
            _playerCoin -= value;
            OnCoinValueChange?.Invoke();
        }

        public void IncreaseEnemy(int value)
        {
            _enemyCoin += value;
            OnEnemyCoinChange?.Invoke();
        }

        // 0 references 21/02/2022
        public void DecreaseEnemy(int value)
        {
            _enemyCoin -= value;
            OnEnemyCoinChange?.Invoke();
        }

        public void SetData()
        {
            _playerCoin = START_COIN;
            _enemyCoin = START_COIN;
        }

        public void IncreaseCoinByRound(int roundIndex)
        {   
            //round 1 index = 0
            roundIndex += 1;

            IncreasePlayer(roundIndex);
            IncreaseEnemy(roundIndex);

        }
    }
}

