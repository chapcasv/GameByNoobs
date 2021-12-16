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
            PhaseSystem.RunTimeBar(maxTime); //Anim Spawn

            EnemyCacheNode();
            PlayerUnitCacheNode();
        }

        private void EnemyCacheNode()
        {
            var allEnemies = DictionaryTeamBattle.GetAllUnits(UnitTeam.Enemy);
            foreach (var e in allEnemies)
            {
                DictionaryTeamBattle.CacheNode(e);
            }
        }

        private void PlayerUnitCacheNode()
        {
            var allUnits = DictionaryTeamBattle.GetAllUnits(UnitTeam.Player);
            foreach (var e in allUnits)
            {
                DictionaryTeamBattle.CacheNode(e);
            }
        }
    }
}

