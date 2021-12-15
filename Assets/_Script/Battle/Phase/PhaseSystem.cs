using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using UnityEngine.UI;

namespace PH
{
    public class PhaseSystem : MonoBehaviour
    {
        public static Phase CurrentPhase { get; private set; }
        public static int WaveIndex { get; private set; }

        [SerializeField] Phase[] phases;
        [SerializeField] Button btnSkipControlPhase;

        private int _phaseIndex;
        private Wave[] waves;
        private BoardSystem _boardSystem;
        private LocalPlayer _localPlayer;

        public LocalPlayer Player { set => _localPlayer = value; }
        public BoardSystem BoardSystem { set => _boardSystem = value; }

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
            if (CurrentPhase as PlayerControl)
            {
                CurrentPhase.forceExit = true;
            }
        }

        public void CompleteStartCard()
        {
            WaveIndex = 0;
            _phaseIndex = 0;
            SetPhase(phases[_phaseIndex]);
        }

        public void IncreaseCurrentWaveIndex() => WaveIndex++;

        public void SpawnEnemy() => _boardSystem.SpawnEnemy(waves[WaveIndex]);

        public void PlayerDrawCard() => _localPlayer.DrawCard();

        public void CopyWave(Wave[] waves)
        {
            this.waves = new Wave[waves.Length];
            waves.CopyTo(this.waves, 0);
        }
    }
}

