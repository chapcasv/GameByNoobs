using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/PlayerControl")]
    public class PlayerControl : Phase
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
            PhaseSystem.RunTimeBar(maxTime);
        }
 
    }
}

