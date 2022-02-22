using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class PlayModeEnemy : ScriptableObject
    {
        [SerializeField] protected string enemyName;
        [Range(1, 99)]
        [SerializeField] protected int life = 20;
        [SerializeField] protected Sprite enemyAvatar;

        public string EnemyName { get => enemyName; set => enemyName = value; }
        public int Life { get => life; set => life = value; }

        public Wave[] Waves;
        public Sprite EnemyAvatar { get => enemyAvatar; set => enemyAvatar = value; }

        public GameObject boardPrefab;
    }
}

