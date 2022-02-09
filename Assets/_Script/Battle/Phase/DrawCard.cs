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
        public event Action OnEnterDrawCard;

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
            
            OnEnterDrawCard?.Invoke(); //UI Obj
            PhaseSystem.PlayerDrawCard();
            PhaseSystem.RunTimeBar(maxTime); //Anim draw Card
        }
  
    }
}

