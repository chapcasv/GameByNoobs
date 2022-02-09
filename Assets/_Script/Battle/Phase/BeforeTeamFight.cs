using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Before Team Fight")]
    public class BeforeTeamFight : Phase
    {
        public event Action OnEnterBeforeTeamFight;

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
            OnEnterBeforeTeamFight?.Invoke(); //UI
            PhaseSystem.SpawnEnemy();
            PhaseSystem.RunTimeBar(maxTime); //Anim Spawn

            PlayerCacheUnit();
        }


        private void PlayerCacheUnit()
        {
            var allUnits = DictionaryTeamBattle.GetAllUnits(UnitTeam.Player);
            foreach (var unit in allUnits)
            {
                PlayerCacheUnitData.CacheUnitData(unit);
            }
        }
    }
}

