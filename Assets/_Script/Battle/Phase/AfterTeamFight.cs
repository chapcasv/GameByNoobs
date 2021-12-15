using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/After Team Fight")]
    public class AfterTeamFight : Phase
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
            Debug.Log("After Team Fight");
            if (PhaseSystem.IsEndBattle())
            {

            }
            else 
            {
                PhaseSystem.IncreaseCurrentWaveIndex();
                forceExit = true;
            }
            
        }
    }
}

