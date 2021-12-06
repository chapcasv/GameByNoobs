using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.States;

namespace PH
{
    public class BattlePVEManager : MonoBehaviour
    {
        public State currentState;
        public PlayerTeam playerTeam;
        public Phase currentPhase;

        private void Awake()
        {
            InitBattle();
        }

        private void Update()
        {
            //currentState.Tick(Time.deltaTime);
        }

        private void InitBattle()
        {
            playerTeam.Init();
        }

        public void SetPhase(Phase phase)
        {
            currentPhase = phase;
            currentPhase.OnStartPhase();
        }
    }
}

