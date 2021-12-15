using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Raid/Enemy")]
    public class Enemy : ScriptableObject
    {
        [SerializeField] CardUnit enemy;

        [Range(32, 63)]
        [Tooltip("Position enemy spawn in board")]
        [SerializeField] int pos = 32;

        public int Pos { get => pos;}
        public CardUnit GetEnemy { get => enemy;}
    }
}
