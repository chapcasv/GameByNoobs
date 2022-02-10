using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using PH.State;

namespace PH
{
    public class PhaseSystem : MonoBehaviour
    {
        public static Phase CurrentPhase { get; private set; }
        public static bool UseTimeBar { get; private set; }
        public static bool BattleIsEnd { get; private set; }
        public ResultLastRound ResultLastRound { get => resultLastRound; set => resultLastRound = value; }

        [SerializeField] ControlState controlState;
        [SerializeField] List<Phase> phases;
        [SerializeField] TimeBar timeBar;

        private int _phaseIndex;
        private ResultLastRound resultLastRound;
        private WaveSystem _waveSystem;
        private BoardSystem _boardSystem;
        private CoinSystem _coinSystem;
        private DeckSystem _deckSystem;
        private LifeSystem _lifeSystem;
        private ResultSystem _resultSystem;

        public void Constructor(BoardSystem BS, LifeSystem LS, WaveSystem WS, DeckSystem DS, 
            CoinSystem CS, ResultSystem RS)
        {
            _boardSystem = BS;
            _lifeSystem = LS;
            _waveSystem = WS;
            _deckSystem = DS;
            _coinSystem = CS;
            _resultSystem = RS;

            CurrentPhase = null;
            UseTimeBar = true;
            BattleIsEnd = false;
        }

        private void Awake()
        {
            DictionaryTeamBattle.OnTeamDefeat += OnTeamDefeat;
            controlState.OnRightClick += SkipControlPhase;
        }

        private void Start()
        {
            SetPhase(phases[_phaseIndex]); //StartCardPhase
        }

        private void Update()
        {
            if (CurrentPhase == null || CurrentPhase is StartCard) return; ///Start Card phase or Result phase

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
            StateSystem.CurrentState = (CurrentPhase.state);
            CurrentPhase.Init(this);
        }

        public void RemovePhase(Phase phase)
        {
            phases.Remove(phase);
        }

        private void LoopPhase()
        {
            _phaseIndex++;

            if (_phaseIndex > phases.Count - 1)
            {
                _phaseIndex = 0;
            }
        }

        private void SkipControlPhase()
        {
            if (CurrentPhase as PlayerControl)
            {
                timeBar.StopAllCoroutines();
                UseTimeBar = false;
            }
        }


        /// <summary>
        /// Run phase loop after Complete start card phase
        /// </summary>
        public void CompleteStartCard()
        {
            _phaseIndex = 0;
            SetPhase(phases[_phaseIndex]);
        }

        //Draw Card Phase
        public void PlayerDrawCard() => _deckSystem.DrawCard();

        //Before Team Fight Phase
        public void SpawnEnemy()
        {
            var currentWave = _waveSystem.GetCurrentWave();
            _boardSystem.SpawnEnemyInWave(currentWave);
        }

        //Team Fight
        private void OnTeamDefeat(UnitTeam team)
        {
            switch (team)
            {
                case UnitTeam.Player:
                    resultLastRound = ResultLastRound.PlayerLose;
                    break;
                case UnitTeam.Enemy:
                    resultLastRound = ResultLastRound.PlayerWin;
                    break;
            }
        }


        //After Team Fight Phase

        public void AtkLifeTeamDefeat() => _lifeSystem.AtkByResultLastRound(resultLastRound);

        public bool PlayerLifeIsZero() => _lifeSystem.PlayerLifeIsZero();

        public bool EnemyLifeIsZero() => _lifeSystem.EnemyLifeIsZero();

        public void RewardClearWave()
        {
            int coin = _waveSystem.GetRewardClearWave();
            _coinSystem.Add(coin);
        }
        public void IncreaseWaveIndex() => _waveSystem.IncreaseIndex();

        public bool IsLastWave() => _waveSystem.IsLastWave();

        //Result Phase
        public void PlayerVictory()
        {
            BattleIsEnd = true;
            CurrentPhase = null;
            _resultSystem.PlayerVictory();
        }

        public void PlayerDefeated()
        {
            BattleIsEnd = true;
            CurrentPhase = null;
            _resultSystem.PlayerDefeated();
        }

        //Time bar
        public void StopTimeBar() => UseTimeBar = false;

        public void RunTimeBar(float maxTime)
        {
            UseTimeBar = true;
            timeBar.StopAllCoroutines();
            StartCoroutine(timeBar.TimeBarPhaseLoop(maxTime));
        }

        private void OnDisable()
        {
            DictionaryTeamBattle.OnTeamDefeat -= OnTeamDefeat;
            controlState.OnRightClick -= SkipControlPhase;

        }
    }
}

