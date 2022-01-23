using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Team Fight")]
    public class TeamFight : Phase
    {
        public override bool IsComplete()
        {
            if (forceExit)
            {
                DictionaryTeamBattle.OnTeamDefeat -= ChangePhase;
                forceExit = false;
                return true;
            }
            return false;
        }

        protected override void OnStartPhase()
        {
            PhaseSystem.RunTimeBar(maxTime);

            var playerUnit = DictionaryTeamBattle.GetAllUnits(UnitTeam.Player);
            var enemyUnit = DictionaryTeamBattle.GetAllUnits(UnitTeam.Enemy);

            bool bothTeamHaveMember = CheckMember(playerUnit, enemyUnit);

            if (!bothTeamHaveMember) return;

            DictionaryTeamBattle.OnTeamDefeat += ChangePhase;

            SetInTeamFight(playerUnit, enemyUnit);
        }

        private void SetInTeamFight(List<BaseUnit> playerUnit, List<BaseUnit> enemyUnit)
        {
            foreach (var unit in playerUnit)
            {
                unit.InTeamFight = true;
            }

            foreach (var enemy in enemyUnit)
            {
                enemy.InTeamFight = true;
            }
        }

        private void ChangePhase(UnitTeam team)
        {   
            PhaseSystem.StopTimeBar();
        }

        private bool CheckMember(List<BaseUnit> playerUnit, List<BaseUnit> enemyUnit)
        {   
            bool playerDontHaveUnitInBoard = playerUnit.Count == 0;
            bool enemyDontHaveUnitInBoard = enemyUnit.Count == 0;

            if (playerDontHaveUnitInBoard && !enemyDontHaveUnitInBoard)
            {
                PhaseSystem.ResultLastRound = ResultLastRound.PlayerLose;
                forceExit = true;
                return false;
            }
            else if (!playerDontHaveUnitInBoard && enemyDontHaveUnitInBoard)
            {

                PhaseSystem.ResultLastRound = ResultLastRound.PlayerWin;
                forceExit = true;
                return false;
            }
            else if (playerDontHaveUnitInBoard && enemyDontHaveUnitInBoard)
            {
                PhaseSystem.ResultLastRound = ResultLastRound.Draw;
                forceExit = true;
                return false;
            }
            else return true;
        }
    }
}

