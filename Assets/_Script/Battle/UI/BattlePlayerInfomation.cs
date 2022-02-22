using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PH
{
    public class BattlePlayerInfomation : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI enemyName;
        [SerializeField] TextMeshProUGUI playerName;

        private PlayModeEnemy _currentEnemy;
        private PlayerLocalSO _player;

        public void Constructor(PlayModeEnemy enemy, PlayerLocalSO player)
        {
            _currentEnemy = enemy;
            _player = player;

            enemyName.text = _currentEnemy.EnemyName;
            playerName.text = _player.GetPlayerName();
        }
    }
}

