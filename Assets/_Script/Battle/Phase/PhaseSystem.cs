using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using UnityEngine.UI;

namespace PH
{
    public class PhaseSystem : MonoBehaviour
    {

        [SerializeField] Phase[] phases;
        [SerializeField] Button btnSkipControlPhase;

        private int _phaseIndex;
        private BoardSystem _boardSystem;
        private LocalPlayer _localPlayer;

        public static Phase CurrentPhase { get; private set; }
        public LocalPlayer LocalPlayer { get => _localPlayer; set => _localPlayer = value; }
        public BoardSystem BoardSystem { get => _boardSystem; set => _boardSystem = value; }

        private void Awake()
        {
            StartCardSystem.OnComplete += CompleteStartCard;
            btnSkipControlPhase.onClick.AddListener(SkipControlPhase);
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
            CurrentPhase.Init(this);
        }

        private void LoopPhase()
        {
            _phaseIndex++;

            if (_phaseIndex > phases.Length - 1)
            {
                _phaseIndex = 0;
            }
        }


        private void SkipControlPhase()
        {
            if(CurrentPhase as PlayerControl)
            {
                CurrentPhase.forceExit = true;
            }
        }

        public void CompleteStartCard()
        {
            _phaseIndex = 0;
            SetPhase(phases[_phaseIndex]);
        }
    }
}

