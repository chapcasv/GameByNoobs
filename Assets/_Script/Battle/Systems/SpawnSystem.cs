using PH.GraphSystem;
using System;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Spawn System")]
    public class SpawnSystem : ScriptableObject
    {
        [SerializeField] private CalPreMitigation[] defaultCalDamage;

        public event Action OnSpawn;

        private UnitsDatabaseSO _unitData;
        private Transform _playerZone;
        private Transform _enemyZone;
        private MemberSystem _memberSystem;
        private BaseUnit _lastUnitSpawn;
        private EnemyDragLogic _eDragLogic;
        private PlayerDragLogic _pDragLogic;
        private Transform _team2Base;
        private Transform _team1Base;

        public void Constructor(UnitsDatabaseSO udb, Transform player, Transform enemy, MemberSystem MS, 
            Transform team2Base, Transform team1Base)
        {
            _unitData = udb;
            _playerZone = player;
            _enemyZone = enemy;
            _memberSystem = MS;
            _team2Base = team2Base;
            _team1Base = team1Base;

            _lastUnitSpawn = null;
        }

        public BaseUnit GetLastUnitSpawn() => _lastUnitSpawn;

        public EnemyDragLogic SetEnemyDragLogic { set => _eDragLogic = value; }

        public PlayerDragLogic SetPlayerDragLogic { set => _pDragLogic = value; }

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
                SpawnPlayerUnit(unit, spawnNode, team, prefab);
                return true;
            }
            else if (team == UnitTeam.Enemy)
            {
                SpawnEnemy(unit, spawnNode, team, prefab);

                return true;
            }
            else return false;
        }

        private void SpawnEnemy(CardUnit unit, Node spawnNode, UnitTeam team, BaseUnit prefab)
        {
            _lastUnitSpawn = InstantiateUnit(unit, unit.CardID, spawnNode, team, prefab,
                                _enemyZone, _eDragLogic, _team1Base);

            AddDefaultCalDamage(_lastUnitSpawn);

            VFXManager.Instance.SpawnUnit(spawnNode.WorldPosition, _lastUnitSpawn);
            //Add TriggerOnBoard after spawn
            OnSpawn?.Invoke();
        }

        private void SpawnPlayerUnit(CardUnit unit, Node spawnNode, UnitTeam team, BaseUnit prefab)
        {
            _lastUnitSpawn = InstantiateUnit(unit, unit.CardID, spawnNode, team, prefab,
                _playerZone, _pDragLogic, _team2Base);

            AddDefaultCalDamage(_lastUnitSpawn);
            _memberSystem.IncreaseMember();

            VFXManager.Instance.DropUnit(spawnNode.WorldPosition);

            //Add TriggerOnBoard after spawn
            OnSpawn?.Invoke();
        }


        private BaseUnit InstantiateUnit(CardUnit unit, int id, Node node, 
            UnitTeam team, BaseUnit prefab, Transform parrent, DragLogic dragLogic, Transform enemyBase)
        {
            BaseUnit newUnit = Instantiate(prefab, parrent);
            newUnit.Setup(node, unit, id, team, dragLogic, enemyBase);
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

