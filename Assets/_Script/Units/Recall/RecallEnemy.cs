using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Unit/Recall Trigger/Enemy")]
    public class RecallEnemy : RecallTrigger
    {
        public override void Execute(BaseUnit unit)
        {
            unit.CurrentNode.SetOccupied(false);
            Destroy(unit.gameObject);
        }
    }
}

