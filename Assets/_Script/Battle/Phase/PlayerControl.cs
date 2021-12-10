using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        public override void OnStartPhase()
        {

        }

        public override void OnEndPhase()
        {
            
        }

        
    }
}

