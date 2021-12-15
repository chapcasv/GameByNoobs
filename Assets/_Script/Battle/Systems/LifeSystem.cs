using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using System;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Life System")]
    public class LifeSystem : ScriptableObject
    {
        [SerializeField] IntReference _enemyLife;
        [SerializeField] IntReference _playerLife;

        public event Action OnEnemyLifeChange;
        public event Action OnPlayerLifeChange;

        public int GetEnemyLife() => _enemyLife.Value;
        public int GetPlayerLife() => _playerLife.Value;

        public bool PlayerLifeIsZero()
        {
            if (_playerLife.Value <= 0) return true;
            else return false;
        }

        public bool EnemyLifeIsZero()
        {
            if(_enemyLife.Value <= 0) return true;
            else return false;
        }

        public void DecreaseEnemyLife(int value)
        {
            _enemyLife.Value -= value;
            OnEnemyLifeChange?.Invoke();
        }

        public void DescreasePlayerLife(int value)
        {
            _playerLife.Value -= value;
            OnPlayerLifeChange?.Invoke();
        }

        public void SetData(PVE_Raid raid)
        {
            _enemyLife.Value = raid.EnemyLife;
            _playerLife.Value = raid.PlayerLife;
        }
    }
}

