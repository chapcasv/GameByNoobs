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

        public event Action OnEnemyLifeChange;
        public event Action OnPlayerLifeChange;

        private int _enemyLife;
        private int _playerLife;

        public int GetEnemyLife() => _enemyLife;
        public int GetPlayerLife() => _playerLife;

        public void AtkTo(UnitTeam team)
        {
            var allUnit = DictionaryTeamBattle.GetUnitsAgainst(team);

            foreach (BaseUnit unit in allUnit)
            {
                int dmg = unit.GetDamageLife();

                if(team == UnitTeam.Player)
                {
                    DecreasePlayerLife(dmg);
                }
                else DecreaseEnemyLife(dmg);
            }
        }


        public bool PlayerLifeIsZero()
        {
            if (_playerLife <= 0) return true;
            else return false;
        }

        public bool EnemyLifeIsZero()
        {
            if(_enemyLife <= 0) return true;
            else return false;
        }

        private void DecreaseEnemyLife(int value)
        {
            _enemyLife -= value;
            OnEnemyLifeChange?.Invoke();
        }

        private void DecreasePlayerLife(int value)
        {
            _playerLife -= value;
            OnPlayerLifeChange?.Invoke();
        }

        public void SetData(PVE_Raid raid)
        {
            _enemyLife = raid.EnemyLife;
            _playerLife = raid.PlayerLife;
        }
    }
}

