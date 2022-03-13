using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Start Round")]
    public class StartRoundPhase : Phase
    {
        public event Action OnEnterPhase;

        [HideInInspector] public BattleNotifyUI notifyPhase;

        private WaveSystem _ws;
        private CoinSystem _cs;

        public override void Init(PhaseSystem phaseSystem)
        {
            base.Init(phaseSystem);
            _ws = phaseSystem.GetWaveSystem;
            _cs = phaseSystem.GetCoinSystem;
        }

        public override void OnStartPhase()
        {
            PhaseSystem.RunTimeBar(maxTime);

            OnEnterPhase?.Invoke();

            ActiveNotify();
            IncreaseCoin();

            ReLoadPlayerUnit();
        }

        private void IncreaseCoin()
        {
            int roundIndex = _ws.GetCurrentIndex();
            _cs.IncreaseCoinByRound(roundIndex);
        }

        private void ActiveNotify()
        {
            string round = _ws.GetCurrentIndexString();
            notifyPhase.SetNotifyRound(round);
        }

        private void ReLoadPlayerUnit()
        {
            var allUnitPlayer = PlayerCacheUnitData.GetAllUnit();

            foreach (var unit in allUnitPlayer)
            {
                PlayerCacheUnitData.ReuseUnit(unit);
                unit.GetUnitStatusEffect.RemoveAllStatusEffect();
            }
        }
    }
}

