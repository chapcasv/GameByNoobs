using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using System;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/After Team Fight")]
    public class AfterTeamFight : Phase
    {
        [SerializeField] RecallTrigger playerRecall;
        [SerializeField] RecallTrigger enemyRecall;

        private LifeSystem _ls;
        private WaveSystem _ws;
        private ResultSystem _rs;

        public override void Init(PhaseSystem phaseSystem)
        {
            base.Init(phaseSystem);
            _ls = PhaseSystem.GetLifeSystem;
            _ws = PhaseSystem.GetWaveSystem;
            _rs = PhaseSystem.GetResultSystem;
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
            PhaseSystem.RunTimeBar(maxTime);

            var teamDefeat = PhaseSystem.ResultLastRound;
            _ls.AtkByResultLastRound(teamDefeat);

            if (_ls.PlayerLifeIsZero())
            {
                PlayerDefeat();
            }
            else if (_ls.EnemyLifeIsZero())
            {
                PlayerVictory();
            }
            else if (_ws.IsLastWave())
            {
                PlayerDefeat();
            }
            else
            {
                //Continue phase
                DestroyEnemy();
                RecallPlayerUnit();
                PhaseSystem.RewardClearWave();
                _ws.IncreaseIndex();
            }
        }

        private void PlayerDefeat()
        {
            PhaseSystem.BattleIsEnd = true;
            PhaseSystem.CurrentPhase = null;
            _rs.PlayerDefeated();
        }

        private void PlayerVictory()
        {
            PhaseSystem.BattleIsEnd = true;
            PhaseSystem.CurrentPhase = null;
            _rs.PlayerVictory();
        }

        private void DestroyEnemy()
        {
            var enemies = DictionaryTeamBattle.GetAllUnits(UnitTeam.Enemy);

            foreach (var e in enemies)
            {
                VFXManager.Instance.RecallUnit(e, enemyRecall);
            }
            DictionaryTeamBattle.Clear(UnitTeam.Enemy);
        }

        private void RecallPlayerUnit()
        {
            var units = DictionaryTeamBattle.GetAllUnits(UnitTeam.Player);

            foreach (var unit in units)
            {
                VFXManager.Instance.RecallUnit(unit, playerRecall);
            }
        }
    }
}

