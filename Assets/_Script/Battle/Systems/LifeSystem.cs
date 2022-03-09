using UnityEngine;
using System;
using System.Collections.Generic;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Life System")]
    public class LifeSystem : ScriptableObject
    {
        public event Action OnEnemyLifeChange;
        public event Action OnPlayerLifeChange;

        private NotifyResultPhase _notify;
        private int enemyLife;
        private int playerLife;
        private const int PLAYER_LIFE = 20;
        public int GetEnemyLife() => enemyLife;
        public int GetPlayerLife() => playerLife;

        public void AtkByResultLastRound(ResultLastRound result)
        {
            int totalDmg;

            switch (result)
            {
                case ResultLastRound.PlayerWin:
                    totalDmg = AtkTo(UnitTeam.Enemy);
                    _notify.SetWin(totalDmg);
                    break;
                case ResultLastRound.PlayerLose:
                    totalDmg = AtkTo(UnitTeam.Player);
                    _notify.SetLose(totalDmg);
                    break;
                case ResultLastRound.Draw:
                    AtkTo(UnitTeam.Enemy);
                    AtkTo(UnitTeam.Player);
                    break;
            }
        }

        private int AtkTo(UnitTeam team)
        {
            var allUnit = DictionaryTeamBattle.GetUnitsAgainst(team);

            if(team == UnitTeam.Player)
            {
                int totalDmg = GetTotalDmg(allUnit);
                DecreasePlayerLife(totalDmg);
                return totalDmg;
            }
            else
            {
                int totalDmg = GetTotalDmg(allUnit);
                DecreaseEnemyLife(totalDmg);
                return totalDmg;
            }
        }

        private int GetTotalDmg(List<BaseUnit> unitAgain)
        {
            int totalDmg = 0;

            foreach (var unit in unitAgain)
            {
                int dmg = unit.GetDmgLife;
                totalDmg += dmg;
            }
            return totalDmg;
        }

        public bool PlayerLifeIsZero()
        {
            if (playerLife <= 0) return true;
            else return false;
        }

        public bool EnemyLifeIsZero()
        {
            if(enemyLife <= 0) return true;
            else return false;
        }

        private void DecreaseEnemyLife(int value)
        {
            enemyLife -= value;
            OnEnemyLifeChange?.Invoke();
        }

        private void DecreasePlayerLife(int value)
        {
            playerLife -= value;
            OnPlayerLifeChange?.Invoke();
        }

        public void SetData(PlayModeEnemy enemy, NotifyResultPhase notify)
        {
            enemyLife = enemy.Life;
            playerLife = PLAYER_LIFE;
            _notify = notify;
        }
    }
}

