using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Spawn Enemy Fight")]
    public class SpawnEnemyPhase : Phase
    {
        private WaveSystem _ws;
        private BoardSystem _bs;

        public override void Init(PhaseSystem phaseSystem)
        {
            base.Init(phaseSystem);

            _ws = PhaseSystem.GetWaveSystem;
            _bs = PhaseSystem.GetBoardSystem;
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
            var currentWave = _ws.GetCurrentWave();
            _bs.SpawnEnemyInWave(currentWave);
        }

       
    }
}

