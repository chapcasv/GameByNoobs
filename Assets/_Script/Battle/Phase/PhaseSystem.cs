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
        public static Phase CurrentPhase { get; set; }
        public static bool UseTimeBar { get; private set; }
        public static bool BattleIsEnd { get; set; }

        [SerializeField] ControlState controlState;
        [SerializeField] StartCard startCardPhase;
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

        public DeckSystem GetDeckSystem { get => _deckSystem; }
        public WaveSystem GetWaveSystem { get => _waveSystem; }
        public BoardSystem GetBoardSystem { get => _boardSystem; }
        public LifeSystem GetLifeSystem { get => _lifeSystem; }
        public ResultSystem GetResultSystem { get => _resultSystem; }

        public ResultLastRound ResultLastRound { get => resultLastRound; set => resultLastRound = value; }

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
            SetPhase(startCardPhase);
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
            CurrentPhase.OnStartPhase();
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
                UseTimeBar = false;
                timeBar.StopAllCoroutines();
            }
        }

        /// <summary>
        /// Run phase loop after complete start card phase
        /// </summary>
        public void CompleteStartCard()
        {
            _phaseIndex = 0;
            SetPhase(phases[_phaseIndex]);
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

        public void RewardClearWave()
        {
            int coin = _waveSystem.GetRewardClearWave();
            _coinSystem.Add(coin);
        }

        //Time bar
        public void StopTimeBar() => UseTimeBar = false;

        public void RunTimeBar(float maxTime)
        {
            UseTimeBar = true;
            timeBar.StopAllCoroutines();
            StartCoroutine(timeBar.TimeBarPhaseLoop(maxTime));
            timeBar.SetDuration(maxTime).Begin();
        }

        public void RunTimeBarStartCard(float maxTime)
        {
            UseTimeBar = true;
            timeBar.StopAllCoroutines();
            StartCoroutine(timeBar.TimeBarStartCardPhase(maxTime));
            timeBar.SetDuration(maxTime).Begin();
        }

        private void OnDisable()
        {
            DictionaryTeamBattle.OnTeamDefeat -= OnTeamDefeat;
            controlState.OnRightClick -= SkipControlPhase;
        }
    }
}

