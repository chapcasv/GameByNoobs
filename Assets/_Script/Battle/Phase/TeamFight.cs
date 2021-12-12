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
            
            return false;
        }

        protected override void OnStartPhase()
        {
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
    }
}

