using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using System;
using SO;

namespace PH
{
    public enum UnitTeam
    {
        Player,
        Enemy
    }

    public class BoardSystem : MonoBehaviour
    {
        [SerializeField] UnitsDatabaseSO unitsDatabase;
        [Header("GridBoard")]
        [SerializeField] Transform tilesHolder;
        [Header("Parent")]
        [SerializeField] Transform playerZone;
        [SerializeField] Transform enemyZone;

        private MemberSystem _memberSystem;
        private EquipmentSystem _equipmentSystem;

        public void Constructor(MemberSystem MB, EquipmentSystem ES)
        {
            _memberSystem = MB;
            _equipmentSystem = ES;
        }

        private void Awake()
        {
            GridBoard.InitializeGraph(tilesHolder);
        }

        public void SpawnEnemyInWave(Wave wave)
        {
            var enemies = wave.enemies;

            for (int i = 0; i < enemies.Length; i++)
            {
                Node node = GridBoard.IntPositiontoNode(enemies[i].Pos);
                SpawnUnit(enemies[i].GetEnemy, node, UnitTeam.Enemy);
            }
        }

        public void SpawnUnit(CardUnit unit, Node spawnNode, UnitTeam team = UnitTeam.Player)
        {
            BaseUnit prefab = GetUnit(unit.UnitID);

            if (prefab == null) throw new Exception("Cant find prefab");

            if (team == UnitTeam.Player)
            {
                InstantiateUnit(unit, spawnNode, team, prefab,playerZone);
                _memberSystem.IncreaseMember();
            }
            else if (team == UnitTeam.Enemy)
            {
                InstantiateUnit(unit, spawnNode, team, prefab, enemyZone);
            }
        }

        private void InstantiateUnit(CardUnit unit, Node spawnNode, UnitTeam team, BaseUnit prefab, Transform parrent)
        {
            BaseUnit newUnit = Instantiate(prefab, parrent);
            newUnit.Setup(spawnNode, unit, team);
            DictionaryTeamBattle.AddUnit(team, newUnit);
        }

        private BaseUnit GetUnit(BaseUnitID ID)
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

        public void DropItem(CardItem item, Node nodeDrop, UnitTeam team)
        {
            _equipmentSystem.DropItem(item, nodeDrop, team);
        }
    }
}

