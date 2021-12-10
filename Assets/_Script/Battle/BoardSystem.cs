using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;


namespace PH
{
    public class BoardSystem : MonoBehaviour
    {
        [SerializeField] Transform playerZone;
        [SerializeField] Transform tilesHolder;

        private void Awake()
        {
            GridManager.InitializeGraph(tilesHolder);
        }

        public void SpawnUnit(CardUnit unit, Node spawnNode)
        {
            BaseUnit baseUnit = Instantiate(unit.prefab, playerZone);
            baseUnit.Setup(spawnNode);

        }
    }
}

