using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "new Input Summon", menuName = "ScriptableObject/Card/Trigger Drop/Input/Summon")]
    public class TriggerInputSummon : CardDropTriggerInput
    {
        [SerializeField] CardUnit unitSummon;

        public CardUnit GetUnit => unitSummon;
    }
}

