using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

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
            PhaseSystem.RunTimeBar(maxTime);

            if (PhaseSystem.PlayerLifeIsZero())
            {
                PhaseSystem.PlayerDefeated();
            }
            else if (PhaseSystem.EnemyLifeIsZero())
            {
                PhaseSystem.PlayerVictory();
            }
            else if (PhaseSystem.IsLastWave())
            {
                PhaseSystem.PlayerDefeated();
            }
            else
            {   
                //Continue phase
                ReLoadPlayerUnit();
                ReLoadEnemy();
                PhaseSystem.RewardClearWave();
                PhaseSystem.IncreaseWaveIndex();
            }

        }

        private void ReLoadEnemy()
        {
            var allEnemies = DictionaryTeamBattle.GetAllUnits(UnitTeam.Enemy);

            foreach (var enemy in allEnemies)
            {
                SetInTeamFight(enemy);
                ReLoadPosition(enemy);
            }
        }

        private  void ReLoadPlayerUnit()
        {
            var allUnitPlayer = DictionaryTeamBattle.GetAllUnits(UnitTeam.Player);

            foreach (var unit in allUnitPlayer)
            {
                SetInTeamFight(unit);
                ReLoadPosition(unit);
            }
        }

        private void ReLoadPosition(BaseUnit unit)
        {
            int cachePos = DictionaryTeamBattle.GetCachePos(unit);
            Node cacheNode = GridBoard.IntPositiontoNode(cachePos);
            unit.CurrentNode.SetOccupied(false);
            unit.CurrentNode = cacheNode;
            unit.transform.position = unit.CurrentNode.WorldPosition;
            unit.CurrentNode.SetOccupied(true);
        }

        private void SetInTeamFight(BaseUnit unit) => unit.InTeamFight = false;
    }
}

