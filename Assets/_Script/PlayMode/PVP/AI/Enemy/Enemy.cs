using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Raid/Enemy")]
    public class Enemy : ScriptableObject
    {
        [SerializeField] CardUnit enemy;

        public CardUnit GetEnemy { get => enemy;}
    }
}
