using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Game State/Start Card")]
    public class StartCardState : GameState
    {
        public override void LeftClick()
        {
            ForceExitStartCardPhase();
        }

        public override void RightClick()
        {
            ForceExitStartCardPhase();
        }

        private void ForceExitStartCardPhase()
        {
            PhaseSystem.UseTimeBar = false;
        }
    }
}

