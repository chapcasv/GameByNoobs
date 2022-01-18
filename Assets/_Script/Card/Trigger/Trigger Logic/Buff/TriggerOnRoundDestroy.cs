using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PH
{
    public class TriggerOnRoundDestroy : CardDropTriggerLogic
    {
        [SerializeField] AfterTeamFight after;

        public override bool CanTrigger(Node dropNode, BoardSystem boardSystem, Card card, CardDropTriggerInput input, UnitTeam team = UnitTeam.Player)
        {
            after.OnClearItemSlot += DestroyItemOnEndRound;
            throw new NotImplementedException();
        }

        private void DestroyItemOnEndRound()
        {
            
        }
    }
}

