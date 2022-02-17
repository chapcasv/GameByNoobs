using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class PVP_Enemy : ScriptableObject
    {
        [SerializeField] string enemyName;
        [Range(1, 99)]
        [SerializeField] int life = 20;
        [SerializeField] Sprite enemyAvatar;

        public string EnemyName { get => enemyName; set => enemyName = value; }
        public int Life { get => life; set => life = value; }
        public Wave[] Waves;
        public Sprite EnemyAvatar { get => enemyAvatar; set => enemyAvatar = value; }
    }
}

