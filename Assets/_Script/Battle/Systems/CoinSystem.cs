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
        private const int START_COIN = 10; //Only for test

        private int _playerCoin;
        private int _enemyCoin;

        public int GetCoin() => _playerCoin;
        public int GetEnemyCoin() => _enemyCoin;

        public void Add(int value)
        {
            _playerCoin += value;
            OnCoinValueChange?.Invoke();
        }
        public void Sub(int value)
        {
            _playerCoin -= value;
            OnCoinValueChange?.Invoke();
        }

        public void SetData()
        {
            _playerCoin = START_COIN;
            _enemyCoin = START_COIN;
        }

        public void IncreaseCoinByRound(int roundIndex)
        {
            _playerCoin += roundIndex;
            _enemyCoin += roundIndex;
        }
    }
}

