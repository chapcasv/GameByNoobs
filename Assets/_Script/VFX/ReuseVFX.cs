using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class ReuseVFX : GameVFX
    {
        private BaseUnit _unit;
        private float _time;
        private const float MAX_TIME = 0.75f;
        private bool isReuse;

        public void SetUp(BaseUnit unit)
        {
            _unit = unit;
            isReuse = false;
            _time = MAX_TIME;
            transform.position = unit.transform.position;
            gameObject.SetActive(true);
        }

        protected override void Update()
        {
            base.Update();
            _time -= Time.deltaTime;

            if (_time <= 0 && !isReuse)
            {
                _unit.gameObject.SetActive(true);
                isReuse = true;
            }
        }
    }
}

