using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    public enum UnitTeam
    {
        Player,
        Enemy
    }

    public class BoardSystem : MonoBehaviour
    {
        [Header("GridBoard")]
        [SerializeField] Transform tilesHolder;
        [Header("Base")]
        [SerializeField] Transform team2Base;
        [SerializeField] Transform team1Base;
        [Header("Parent")]
        [SerializeField] Transform playerZone;
        [SerializeField] Transform enemyZone;

        private SpawnSystem _spawnSystem;
        private DeckSystem _deckSystem;

        //Dont need parameter CIV
        public void Constructor(MemberSystem MS, SpawnSystem SS, DeckSystem DS, CardInfoVisual CIV, UnitsDatabaseSO data)
        {  
            _spawnSystem = SS;
            //Set up trigger after spawn
            _deckSystem = DS;
            _spawnSystem.OnSpawn += SetUpTriggerOnBoard;
            _spawnSystem.Constructor(data, playerZone, enemyZone, MS, team2Base,team1Base);
        }

        private void Awake()
        {
            GridBoard.InitializeGraph(tilesHolder);
        }

        public void SpawnEnemyInWave(Wave wave) => _spawnSystem.SpawnEnemyInWave(wave);

        public bool TrySpawnUnit(CardUnit unit, Node spawnNode, UnitTeam team = UnitTeam.Player)
        {
            bool canSpawn = _spawnSystem.SpawnUnit(unit, spawnNode);
            return canSpawn;
        }

        public void SetUpTriggerOnBoard()
        {
            BaseUnit lastUnitSpawn = _spawnSystem.GetLastUnitSpawn();

            if(lastUnitSpawn != null)
            {
                lastUnitSpawn.AddListernerTriggerOnBoard();

                _spawnSystem.SetLastUnitSpawn();
            }
        }
    }
}

