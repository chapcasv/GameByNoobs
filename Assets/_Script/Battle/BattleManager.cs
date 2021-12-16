using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] PVE_Raid currentRaid;
        [SerializeField] BattleUIManager battleUIManager;

        [Header("Battle Systems")]
        [SerializeField] MemberSystem memberSystem;
        [SerializeField] LifeSystem lifeSystem;
        [SerializeField] WaveSystem wavesSystem;
        [SerializeField] CoinSystem coinSystem;
        [SerializeField] DeckSystem deckSystem;
        [SerializeField] ResultSystem resultSystem;

        private PhaseSystem _phaseSystem;
        private BoardSystem _boardSystem;


        /// Constructor Injection

        private void Awake()
        {
            SetBattleUIManager();

            SetSystemByCurrentRaid();

            _boardSystem = GetComponent<BoardSystem>();
            SetBoardSystem();

            _phaseSystem = GetComponent<PhaseSystem>();
            SetPhaseSystem();

            DictionaryTeamBattle.Init();
        }

        private void SetSystemByCurrentRaid()
        {
            lifeSystem.SetData(currentRaid);
            wavesSystem.SetData(currentRaid);
            coinSystem.SetData(currentRaid);
            memberSystem.SetData();
        }

        private void SetBattleUIManager()
        {
            battleUIManager.Constructor(lifeSystem, wavesSystem, coinSystem, memberSystem, resultSystem);
        }


        private void SetBoardSystem()
        {
            _boardSystem.Player = memberSystem;
        }

        private void SetPhaseSystem()
        {
            _phaseSystem.Constructor(_boardSystem, lifeSystem, wavesSystem, deckSystem, coinSystem, resultSystem);
        }
    }
}

