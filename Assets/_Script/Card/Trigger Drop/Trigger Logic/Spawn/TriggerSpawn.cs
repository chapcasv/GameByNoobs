using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class TriggerSpawn : CardDropTriggerLogic
    {
        protected void AddHistory(CardUnit unit, UnitTeam team)
        {
            CardDropHistory.AddCardUnitSpawn(unit, team);
        }
    }
}

