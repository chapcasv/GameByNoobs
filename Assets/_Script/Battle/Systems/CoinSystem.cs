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
        private int _coin;
        public event Action OnCoinValueChange;
        public int GetCoin() => _coin;

        public void Add(int value)
        {
            _coin += value;
            OnCoinValueChange?.Invoke();
        }
        public void Sub(int value)
        {
            _coin -= value;
            OnCoinValueChange?.Invoke();
        }

        public void SetData(PVE_Raid raid) => _coin = raid.PlayerStartCoin;

    }
}

