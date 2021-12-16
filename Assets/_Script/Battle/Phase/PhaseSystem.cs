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

        public static bool UseTimeBar { get; private set; } = true;

        public static bool BattleIsEnd { get; private set; } = false;

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
        }


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
                timeBar.StopAllCoroutines();
                UseTimeBar = false;
            }
        }

        private void OnTeamDefeat(UnitTeam team)
        {
            if (team == UnitTeam.Player)
            {
                var allUnit = DictionaryTeamBattle.GetAllUnits(UnitTeam.Enemy);
                foreach (BaseUnit unit in allUnit)
                {
                    int dmg =  unit.GetDamageLife();
                    _lifeSystem.DescreasePlayerLife(dmg);
                }
            }
            else
            {
                var allUnit = DictionaryTeamBattle.GetAllUnits(UnitTeam.Player);
                foreach (BaseUnit unit in allUnit)
                {
                    int dmg = unit.GetDamageLife();
                    _lifeSystem.DecreaseEnemyLife(dmg);
                }
            }
        }


        public void CompleteStartCard()
        {
            _phaseIndex = 0;
            SetPhase(phases[_phaseIndex]);
        }

        public void PlayerDrawCard() => _deckSystem.DrawCard();

        public void SpawnEnemy() => _boardSystem.SpawnEnemy(_waveSystem.GetCurrentWave());

        public bool PlayerLifeIsZero() => _lifeSystem.PlayerLifeIsZero();

        public bool EnemyLifeIsZero() => _lifeSystem.EnemyLifeIsZero();

        public void RewardClearWave()
        {
            int coin = _waveSystem.GetRewardClearWave();
            _coinSystem.Add(coin);
        }
        public void IncreaseWaveIndex() => _waveSystem.IncreaseIndex();

        public bool IsLastWave() => _waveSystem.IsLastWave();

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

        public void StopTimeBar() => UseTimeBar = false;

        public void RunTimeBar(float maxTime)
        {
            UseTimeBar = true;
            timeBar.StopAllCoroutines();
            StartCoroutine(timeBar.TimeBarPhaseLoop(maxTime));
        }
       
    }
}

