using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "new Wave", menuName = "ScriptableObject/Raid/Wave")]
    public class Wave : ScriptableObject
    {
        public List<Enemy> enemys;
        [Range(0, 200)]
        [Tooltip("Gold bonus after clear all enemy in wave")]
        public int GoldBonus;
    }
}
