using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Team Fight")]
    public class TeamFight : Phase
    {
        public override bool IsComplete()
        {
            
            return false;
        }

        protected override void OnStartPhase()
        {

            Debug.Log(PhaseSystem.CurrentPhase);
        }
    }
}

