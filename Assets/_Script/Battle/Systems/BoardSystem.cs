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

        private CastSpellSystem _castSpellSystem;
        private SpawnSystem _spawnSystem;

        public void Constructor(MemberSystem MS, EquipmentSystem ES, CastSpellSystem CS, SpawnSystem SS)
        {  
            _castSpellSystem = CS;
            _spawnSystem = SS;
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


        public bool TryDropSpell(CardSpell spell, Node nodeDrop, UnitTeam team = UnitTeam.Player)
        {
            bool canDrop = _castSpellSystem.Drop(spell, nodeDrop, team);
            return canDrop;
        }
    }
}

