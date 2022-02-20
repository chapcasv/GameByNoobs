using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Before Team Fight")]
    public class BeforeTeamFight : Phase
    {
        public event Action OnEnterPhase;

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
            OnEnterPhase?.Invoke(); //UI

            PhaseSystem.RunTimeBar(maxTime); //Anim Spawn

            var allUnits = DictionaryTeamBattle.GetAllUnits(UnitTeam.Player);

            PlayerCacheUnit(allUnits);

            FlipRotation(allUnits);

            var currentWave = _ws.GetCurrentWave();
            _bs.SpawnEnemyInWave(currentWave);
        }


        private void PlayerCacheUnit(List<BaseUnit> allUnits)
        {
            foreach (var unit in allUnits)
            {
                PlayerCacheUnitData.CacheUnitData(unit);
            }
        }

        private void FlipRotation(List<BaseUnit> allUnits)
        {
            foreach (var unit in allUnits)
            {
                unit.GetUnitTransform.RotationTowardEnemyBase();
            }
        }
    }
}

