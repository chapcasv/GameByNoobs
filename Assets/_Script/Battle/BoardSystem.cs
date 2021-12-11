using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using System;

namespace PH
{   
    public enum UnitTeam
    {
        Player,
        Enemy
    }

    public class BoardSystem : MonoBehaviour
    {
        [SerializeField] Transform playerZone;
        [SerializeField] Transform enemyZone;
        [SerializeField] Transform tilesHolder;
        [SerializeField] UnitsDatabaseSO unitsDatabase;
        private PVE_Raid _currentRaid;

        public PVE_Raid CurrentRaid {set => _currentRaid = value; }

        private void Awake()
        {
            GridBoard.InitializeGraph(tilesHolder);
        }

        public void SpawnEnemy()
        {
            var enemies = _currentRaid.ListWave[0].enemies;

            for (int i = 0; i < enemies.Length; i++)
            {
                Node node = GridBoard.ConvertPositiontoNode(enemies[i].Pos);
                SpawnUnit(enemies[i].GetEnemy, node, UnitTeam.Enemy);
            }

        }

        public void SpawnUnit(CardUnit unit, Node spawnNode, UnitTeam team = UnitTeam.Player)
        {
            BaseUnit prefab = GetUnit(unit.UnitID);

            if (prefab != null)
            {   
                if(team == UnitTeam.Player)
                {
                    BaseUnit baseUnit = Instantiate(prefab, playerZone);
                    baseUnit.Setup(spawnNode, unit, team);
                }
                else
                {
                    BaseUnit baseUnit = Instantiate(prefab, enemyZone);
                    baseUnit.Setup(spawnNode, unit, team);
                }
            }
            else throw new Exception("Card Unit dont have Unit prefab");
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

