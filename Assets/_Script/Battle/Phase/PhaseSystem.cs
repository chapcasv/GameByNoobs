using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;


namespace PH
{
    public class PhaseSystem : MonoBehaviour
    {

        [SerializeField] Phase[] phases;

        private int _phaseIndex;

        public static Phase CurrentPhase { get; private set; }

        private void Awake()
        {
            StartCardSystem.OnComplete += CompleteStartCard;
        }



        private void Update()
        {

            if (CurrentPhase == null) return; ///OnStartCard

            bool phaseIsComplete = CurrentPhase.IsComplete();

            if (phaseIsComplete)
            {
                LoopPhase();
                SetPhase(phases[_phaseIndex]);
            }
        }


        private void SetPhase(Phase phase)
        {
            CurrentPhase = phase;
            CurrentPhase.OnStartPhase();
        }

        private void LoopPhase()
        {
            _phaseIndex++;

            if (_phaseIndex > phases.Length - 1)
            {
                _phaseIndex = 0;
            }
        }

        public void CompleteStartCard()
        {
            _phaseIndex = 0;
            SetPhase(phases[_phaseIndex]);
        }

    }
}

