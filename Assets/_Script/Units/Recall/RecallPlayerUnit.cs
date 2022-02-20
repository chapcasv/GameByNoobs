using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Unit/Recall Trigger/Player")]
    public class RecallPlayerUnit : RecallTrigger
    {
        public override void Execute(BaseUnit unit)
        {
            unit.gameObject.SetActive(false);
        }
    }
}

