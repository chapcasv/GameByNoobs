using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class SpawnVFX : GameVFX
    {
        private SpawnSystem _spawnSystem;
        private CardUnit _unitSpawn;
        private UnitTeam _team;
        private Node _node;
        private float _time = 1.2f;
        private const float MAX_TIME = 1.2f;
        private bool isSpawn;

        public void SetUp(SpawnSystem ss, CardUnit unit, Node node, UnitTeam team)
        {
            _spawnSystem = ss;
            _unitSpawn = unit;
            _team = team;
            _node = node;
            isSpawn = false;
            _time = MAX_TIME;
        }


        protected override void Update()
        {
            base.Update();
            _time -= Time.deltaTime;

            if(_time <= 0 && !isSpawn)
            {
                _spawnSystem.Spawn(_unitSpawn, _node, _team);
                isSpawn = true;
            }
        }
    }
}

