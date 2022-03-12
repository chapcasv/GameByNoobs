using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "new Wave", menuName = "ScriptableObject/Raid/Wave")]
    public class Wave : ScriptableObject
    {
        public EnemyProperties[] enemies;
    }
}
