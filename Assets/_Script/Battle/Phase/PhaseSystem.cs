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

        public static bool UseTimeBar { get; private set; }

        public static bool BattleIsEnd { get; private set; }

        [SerializeField] Phase[] phases;
        [SerializeField] Button btnSkipControlPhase;
        [SerializeField] TimeBar timeBar;

        private int _phaseIndex;
        private WaveSystem _waveSystem;
        private BoardSystem _boardSystem;
        private CoinSystem _coinSystem;
        private DeckSystem _deckSystem;
        private LifeSystem _lifeSystem;
        private ResultSystem _resultSystem;

        public void Constructor(BoardSystem BS, LifeSystem LS, WaveSystem WS, DeckSystem DS, CoinSystem CS, ResultSystem RS)
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
            StartCardPhase.OnComplete += CompleteStartCard;
            DictionaryTeamBattle.OnTeamDefeat += OnTeamDefeat;

            btnSkipControlPhase.onClick.AddListener(SkipControlPhase);
        }


        private void Update()
        {
            if (CurrentPhase == null) return; ///Start Card phase or Result phase

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
        private void OnTeamDefeat(UnitTeam team) => _lifeSystem.AtkTo(team);

        //After Team Fight Phase
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
            StartCardPhase.OnComplete -= CompleteStartCard;
            DictionaryTeamBattle.OnTeamDefeat -= OnTeamDefeat;

            btnSkipControlPhase.onClick.RemoveAllListeners();
        }
    }
}

