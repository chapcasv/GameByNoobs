using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.States;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/StartCard")]
    public class StartCard : Phase
    {
        public override bool IsComplete()
        {
            return false;
        }

        public override void OnStartPhase()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEndPhase()
        {
            throw new System.NotImplementedException();
        }

        
    }
}

