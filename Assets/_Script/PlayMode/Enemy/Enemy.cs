using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Raid/Enemy")]
    public class Enemy : ScriptableObject
    {
        public Card enemy;
        [Range(32, 64)]
        [Tooltip("Position enemy spawn in board")]
        public int Pos = 32;

    }
}
