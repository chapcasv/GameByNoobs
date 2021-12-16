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

            DictionaryTeamBattle.OnTeamDefeat += ChangePhase;

            var allUnitPlayer = DictionaryTeamBattle.GetAllUnits(UnitTeam.Player);
            foreach (var unit in allUnitPlayer)
            {
                unit.InTeamFight = true;
            }

            var allEnemies = DictionaryTeamBattle.GetAllUnits(UnitTeam.Enemy);
            foreach (var enemy in allEnemies)
            {
                enemy.InTeamFight = true;
            }
        }

        private void ChangePhase(UnitTeam team)
        {   
            PhaseSystem.StopTimeBar();
        }
    }
}

