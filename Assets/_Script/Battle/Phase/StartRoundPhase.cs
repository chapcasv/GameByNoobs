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

        public override void Init(PhaseSystem phaseSystem)
        {
            base.Init(phaseSystem);
            _ws = phaseSystem.GetWaveSystem;
        }

        public override void OnStartPhase()
        {
            PhaseSystem.RunTimeBar(maxTime);

            OnEnterPhase?.Invoke();

            //Notify
            string round = _ws.GetCurrentIndexString();
            notifyPhase.SetNotifyRound(round);

            ReLoadPlayerUnit();
        }

        private void ReLoadPlayerUnit()
        {
            var allUnitPlayer = PlayerCacheUnitData.GetAllUnit();

            foreach (var unit in allUnitPlayer)
            {
                PlayerCacheUnitData.ReuseUnit(unit);
                SetInTeamFight(unit);
            }
        }

        private void SetInTeamFight(BaseUnit unit) => unit.InTeamFight = false;
    }
}

