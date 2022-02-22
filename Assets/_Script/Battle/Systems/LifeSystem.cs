using UnityEngine;
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
        private const int PLAYER_LIFE = 20;
        public int GetEnemyLife() => _enemyLife;
        public int GetPlayerLife() => _playerLife;

        public void AtkByResultLastRound(ResultLastRound result)
        {
            switch (result)
            {
                case ResultLastRound.PlayerWin:
                    AtkTo(UnitTeam.Enemy);
                    break;
                case ResultLastRound.PlayerLose:
                    AtkTo(UnitTeam.Player);
                    break;
                case ResultLastRound.Draw:
                    AtkTo(UnitTeam.Enemy);
                    AtkTo(UnitTeam.Player);
                    break;
            }
        }

        private void AtkTo(UnitTeam team)
        {
            var allUnit = DictionaryTeamBattle.GetUnitsAgainst(team);

            foreach (BaseUnit unit in allUnit)
            {
                int dmg = unit.GetDmgLife;

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

        public void SetData(PlayModeEnemy enemy)
        {
            _enemyLife = enemy.Life;
            _playerLife = PLAYER_LIFE;
        }
    }
}

