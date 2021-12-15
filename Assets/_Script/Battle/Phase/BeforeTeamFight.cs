using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Before Team Fight")]
    public class BeforeTeamFight : Phase
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
            PhaseSystem.SpawnEnemy();
            forceExit = true;
        }

        
    }
}
