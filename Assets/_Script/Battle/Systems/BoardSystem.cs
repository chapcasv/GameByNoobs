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
        [SerializeField] UnitsDatabaseSO unitsDatabase;
        [Header("GridBoard")]
        [SerializeField] Transform tilesHolder;
        [Header("Parent")]
        [SerializeField] Transform playerZone;
        [SerializeField] Transform enemyZone;

        private SpawnSystem _spawnSystem;
        private DeckSystem _deckSystem;
        public void Constructor(MemberSystem MS, SpawnSystem SS, DeckSystem DS)
        {  
            _spawnSystem = SS;

            //Set up trigger after spawn
            _deckSystem = DS;
            _deckSystem.OnDropCard += SetUpTriggerOnBoard;
            _spawnSystem.Constructor(unitsDatabase, playerZone, enemyZone, MS);
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

