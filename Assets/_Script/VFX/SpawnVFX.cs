using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class SpawnVFX : GameVFX
    {
        private BaseUnit _unitSpawn;
        private float _time;
        private const float MAX_TIME = 0.65f;
        private bool isSpawn;

        public void SetUp(BaseUnit unit)
        {
            _unitSpawn = unit;
            isSpawn = false;
            _time = MAX_TIME;
        }


        protected override void Update()
        {
            base.Update();
            _time -= Time.deltaTime;

            if(_time <= 0 && !isSpawn)
            {
                _unitSpawn.gameObject.SetActive(true);
                isSpawn = true;
            }
        }
    }
}

