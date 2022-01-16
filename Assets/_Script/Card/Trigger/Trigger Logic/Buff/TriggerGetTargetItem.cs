using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PH
{
    public class TriggerGetTargetItem : CardDropTriggerLogic
    {
        [SerializeField] AfterTeamFight after;

        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, CardDropTriggerInput input, UnitTeam team = UnitTeam.Player)
        {   

            //after.OnStartPhase += destroys item
            throw new System.NotImplementedException();
        }
        //destroys item
      
    }
}

