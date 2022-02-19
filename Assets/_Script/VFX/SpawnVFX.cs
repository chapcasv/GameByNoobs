using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class SpawnVFX : GameVFX
    {
        private SpawnSystem _spawnSystem;
        private float _time = 2f;

        public SpawnSystem SpawnSystem { get => _spawnSystem; set => _spawnSystem = value; }


        protected override void Update()
        {
            base.Update();
            _time -= Time.deltaTime;

            if(_time <= 0)
            {

            }
        }
    }
}

