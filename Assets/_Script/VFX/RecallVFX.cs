using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class RecallVFX : GameVFX
    {
        private BaseUnit _unit;
        private RecallTrigger _recallTrigger;
        private float _time;
        private const float MAX_TIME = 0.75f;
        private bool isRecall;

        public void SetUp(BaseUnit unit, RecallTrigger recallTrigger)
        {
            _unit = unit;
            isRecall = false;
            _time = MAX_TIME;
            _recallTrigger = recallTrigger;
            transform.position = unit.transform.position;
            gameObject.SetActive(true);
        }

        protected override void Update()
        {
            base.Update();
            _time -= Time.deltaTime;

            if (_time <= 0 && !isRecall)
            {
                _recallTrigger.Execute(_unit);
                isRecall = true;
            }
        }
    }
}

