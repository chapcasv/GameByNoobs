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
        private DeckSystem _deckSystem;
        public override void Init(PhaseSystem phaseSystem)
        {
            base.Init(phaseSystem);
            _deckSystem = phaseSystem.GetDeckSystem;
        }

        public override bool IsComplete()
        {
            if (forceExit)
            {
                forceExit = false;
                return true;
            }
            return false;
        }
        
        public override void OnStartPhase()
        {   

            _deckSystem.DrawCard();
            PhaseSystem.RunTimeBar(maxTime); //Anim draw Card
        }
    }
}

