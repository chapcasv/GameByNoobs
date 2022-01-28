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
        private CardInfoVisual _cardInfoVisual;
        private BaseUnit _lastUnitSpawn;

        [SerializeField] private CalPreMitigation[] defaultCalDamage;

        public void Constructor(UnitsDatabaseSO udb, Transform player, Transform enemy, MemberSystem MS, CardInfoVisual infoVisual)
        {
            _unitData = udb;
            _playerZone = player;
            _enemyZone = enemy;
            _memberSystem = MS;
            _cardInfoVisual = infoVisual;

            _lastUnitSpawn = null;
        }

        public BaseUnit GetLastUnitSpawn() => _lastUnitSpawn;

        public void SetLastUnitSpawn() => _lastUnitSpawn = null;
        

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
                _lastUnitSpawn = InstantiateUnit(unit, unit.CardID, spawnNode, team, prefab, _playerZone);
                AddDefaultCalDamage(_lastUnitSpawn);
                _memberSystem.IncreaseMember();

                return true;
            }
            else if (team == UnitTeam.Enemy)
            {
                _lastUnitSpawn = InstantiateUnit(unit, unit.CardID, spawnNode, team, prefab, _enemyZone);
                AddDefaultCalDamage(_lastUnitSpawn);
                return true;
            }
            else return false;
        }

        private BaseUnit InstantiateUnit(CardUnit unit, int id, Node spawnNode, UnitTeam team, BaseUnit prefab, Transform parrent)
        {
            BaseUnit newUnit = Instantiate(prefab, parrent);
            newUnit.Setup(spawnNode, unit, id, team, _cardInfoVisual);

            DictionaryTeamBattle.AddUnit(team, newUnit);

            return newUnit;
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
        private void AddDefaultCalDamage(BaseUnit unit)
        {
            var unitAtkSystem = unit.GetAtkSystem;
            for (int i = 0; i < defaultCalDamage.Length; i++)
            {
                unitAtkSystem.AddBaseCaculator(defaultCalDamage[i]);
            }
        }
    }
}

