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

            PhaseSystem.AtkLifeTeamDefeat();

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
                DestroyEnemy();
                ReLoadPlayerUnit();

                PhaseSystem.RewardClearWave();
                PhaseSystem.IncreaseWaveIndex();
            }

        }

        private void DestroyEnemy()
        {
            var enemies = DictionaryTeamBattle.GetAllUnits(UnitTeam.Enemy);

            foreach (var e in enemies)
            {
                Destroy(e.gameObject);
            }
            DictionaryTeamBattle.Clear(UnitTeam.Enemy);
        }

        private void ReLoadPlayerUnit()
        {
            var allUnitPlayer = PlayerCacheUnitData.GetAllUnit();

            foreach (var unit in allUnitPlayer)
            {
                PlayerCacheUnitData.ReuseUnit(unit);
                SetInTeamFight(unit);
            }
        }

        private void SetInTeamFight(BaseUnit unit) => unit.InTeamFight = false;
    }
}

