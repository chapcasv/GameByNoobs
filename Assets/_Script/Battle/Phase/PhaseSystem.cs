using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using UnityEngine.UI;
using System;
using SO;

namespace PH
{
    public class PhaseSystem : MonoBehaviour
    {
        public static Phase CurrentPhase { get; private set; }

        [SerializeField] Phase[] phases;
        [SerializeField] Button btnSkipControlPhase;

        private int _phaseIndex;
        private WaveSystem _waveSystem;
        private BoardSystem _boardSystem;
        private MemberSystem _memberSystem;
        private DeckSystem _deckSystem;
        private LifeSystem _lifeSystem;

        public MemberSystem Player { set => _memberSystem = value; }
        public BoardSystem BoardSystem { set => _boardSystem = value; }
        public LifeSystem LifeSystem {set => _lifeSystem = value; }
        public WaveSystem WaveSystem {set => _waveSystem = value; }
        public DeckSystem DeckSystem {set => _deckSystem = value; }

        private void Awake()
        {
            StartCardPhase.OnComplete += CompleteStartCard;
            DictionaryTeamBattle.OnTeamDefeat += OnTeamDefeat;

            btnSkipControlPhase.onClick.AddListener(SkipControlPhase);
        }

        private void Update()
        {
            if (CurrentPhase == null) return; ///StartCardPhase or ResultBattlePhase

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

        private void OnTeamDefeat(UnitTeam team)
        {
            if (team == UnitTeam.Player)
            {
                var allUnit = DictionaryTeamBattle.GetAllUnits(UnitTeam.Enemy);
                foreach (BaseUnit unit in allUnit)
                {
                    unit.AtkLifeTarget(_lifeSystem);
                }
            }
            else
            {
                var allUnit = DictionaryTeamBattle.GetAllUnits(UnitTeam.Player);
                foreach (BaseUnit unit in allUnit)
                {
                    unit.AtkLifeTarget(_lifeSystem);
                }
            }
        }


        public void CompleteStartCard()
        {
            _phaseIndex = 0;
            SetPhase(phases[_phaseIndex]);
        }

        public bool IsEndBattle()
        {
            if (_lifeSystem.PlayerLifeIsZero())
            {
                return true;
            }
            if (_lifeSystem.EnemyLifeIsZero())
            {
                return true;
            }
            return false;
        }

        public void IncreaseCurrentWaveIndex() => _waveSystem.IncreaseIndex();

        public void SpawnEnemy() => _boardSystem.SpawnEnemy(_waveSystem.GetCurrentWave());

        public void PlayerDrawCard() => _deckSystem.DrawCard();

       
    }
}

