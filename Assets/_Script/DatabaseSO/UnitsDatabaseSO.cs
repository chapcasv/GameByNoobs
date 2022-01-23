using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Database/Units")]
    public class UnitsDatabaseSO : ScriptableObject
    {
        [System.Serializable]
        public struct BaseUnitData
        {
            public BaseUnit prefab;
            public BaseUnitID unitID;
        }

        public List<BaseUnitData> allBaseUnits;
    }
}

