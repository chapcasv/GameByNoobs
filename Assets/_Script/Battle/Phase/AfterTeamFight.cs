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
        public event Action<BaseUnit> OnReLoadUnit;
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
                ReLoadPlayerUnit();
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
                e.CurrentNode.SetOccupied(false);
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
                unit.RemoveOneRoundAddOn();
                OnReLoadUnit?.Invoke(unit);
            }
        }
       
        private void SetInTeamFight(BaseUnit unit) => unit.InTeamFight = false;
    }
}

