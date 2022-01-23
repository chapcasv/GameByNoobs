using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "new Input Summon", menuName = "ScriptableObject/Card/Trigger Input/Summon")]
    public class TriggerInputSummon : TriggerInput
    {
        [SerializeField] CardUnit unitSummon;

        public CardUnit GetUnit => unitSummon;
    }
}

