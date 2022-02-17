using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [System.Serializable]
    public class EnemyProperties 
    {
        public Enemy enemy;
        [Range(32, 63)]
        [Tooltip("Position enemy spawn in board")]
        public int Pos = 32;
    }
}

