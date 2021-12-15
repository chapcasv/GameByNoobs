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

        private MemberSystem _player;
        public MemberSystem Player { set => _player = value; }

        private void Awake()
        {
            GridBoard.InitializeGraph(tilesHolder);
        }

        public void SpawnEnemy(Wave wave)
        {
            var enemies = wave.enemies;

            for (int i = 0; i < enemies.Length; i++)
            {
                Node node = GridBoard.ConvertPositiontoNode(enemies[i].Pos);
                SpawnUnit(enemies[i].GetEnemy, node, UnitTeam.Enemy);
            }
        }

        public void SpawnUnit(CardUnit unit, Node spawnNode, UnitTeam team = UnitTeam.Player)
        {
            BaseUnit prefab = GetUnit(unit.UnitID);

            if (prefab == null) throw new Exception("Cant find prefab");

            if (team == UnitTeam.Player)
            {
                InstantiatePlayerUnit(unit, spawnNode, team, prefab);
            }
            else if (team == UnitTeam.Enemy)
            {
                InstantiateEnemy(unit, spawnNode, team, prefab);
            }
        }

        private void InstantiateEnemy(CardUnit unit, Node spawnNode, UnitTeam team, BaseUnit prefab)
        {
            BaseUnit unitEnemy = Instantiate(prefab, enemyZone);
            unitEnemy.Setup(spawnNode, unit, team);
            DictionaryTeamBattle.AddUnit(team, unitEnemy);
        }

        private void InstantiatePlayerUnit(CardUnit unit, Node spawnNode, UnitTeam team, BaseUnit prefab)
        {
            BaseUnit newUnit = Instantiate(prefab, playerZone);
            newUnit.Setup(spawnNode, unit, team);
            DictionaryTeamBattle.AddUnit(team, newUnit);
            _player.IncreaseMember();
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
    }
}

