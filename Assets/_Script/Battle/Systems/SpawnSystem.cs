using PH.GraphSystem;
using System;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Spawn System")]
    public class SpawnSystem : ScriptableObject
    {
        private UnitsDatabaseSO _unitData;
        private Transform _playerZone;
        private Transform _enemyZone;
        private MemberSystem _memberSystem;

        public void Constructor(UnitsDatabaseSO udb, Transform player, Transform enemy, MemberSystem MS)
        {
            _unitData = udb;
            _playerZone = player;
            _enemyZone = enemy;
            _memberSystem = MS;
        }

        public void SpawnEnemyInWave(Wave wave)
        {
            var enemies = wave.enemies;

            for (int i = 0; i < enemies.Length; i++)
            {
                Node node = GridBoard.IntPositiontoNode(enemies[i].Pos);
                SpawnUnit(enemies[i].enemy.GetEnemy, node, UnitTeam.Enemy);
            }
        }

        public bool SpawnUnit(CardUnit unit, Node spawnNode, UnitTeam team = UnitTeam.Player)
        {
            BaseUnit prefab = GetUnit(unit.UnitID, _unitData);

            if (prefab == null)
            {
                throw new Exception("Cant Find prefab !!!");
            }

            if (team == UnitTeam.Player)
            {
                InstantiateUnit(unit, spawnNode, team, prefab, _playerZone);
                _memberSystem.IncreaseMember();

                return true;
            }
            else if (team == UnitTeam.Enemy)
            {
                InstantiateUnit(unit, spawnNode, team, prefab, _enemyZone);
                return true;
            }
            else return false;
        }

        private void InstantiateUnit(CardUnit unit, Node spawnNode, UnitTeam team, BaseUnit prefab, Transform parrent)
        {
            BaseUnit newUnit = Instantiate(prefab, parrent);
            newUnit.Setup(spawnNode, unit, team);
            DictionaryTeamBattle.AddUnit(team, newUnit);
        }

        private BaseUnit GetUnit(BaseUnitID ID, UnitsDatabaseSO unitsDatabase)
        {
            var allBaseUnits = unitsDatabase.allBaseUnits;

            foreach (var baseUnit in allBaseUnits)
            {
                if (baseUnit.unitID == ID)
                {
                    return baseUnit.prefab;
                }
            }
            return null;
        }
    }
}

