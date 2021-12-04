using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.States;

namespace PH
{
    public class BattleManager : MonoBehaviour
    {
        public State currentState;

        private void Start()
        {
            Setting.battleManager = this;
        }


        private void Update()
        {
            //currentState.Tick(Time.deltaTime);
        }
    }
}

