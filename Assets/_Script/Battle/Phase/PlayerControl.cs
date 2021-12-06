using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/PlayerControl ")]
    public class PlayerControl : Phase
    {
        public override bool IsComplete()
        {
            return false;
        }

        public override void OnEndPhase()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStartPhase()
        {
            throw new System.NotImplementedException();
        }
    }
}

