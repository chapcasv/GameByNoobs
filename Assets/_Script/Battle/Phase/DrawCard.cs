using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using System;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Draw Card")]
    public class DrawCard : Phase
    {
        public override bool IsComplete()
        {
            if (forceExit)
            {
                forceExit = false;
                return true;
            }
            return false;
        }
        
        protected override void OnStartPhase()
        {
            PhaseSystem.PlayerDrawCard();

            //Wait for animation draw Card
            forceExit = true;
        }
  
    }
}

