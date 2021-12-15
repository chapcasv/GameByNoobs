using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] MemberSystem memberSystem;
        [SerializeField] PVE_Raid currentRaid;
        [SerializeField] LifeSystem lifeSystem;
        [SerializeField] WaveSystem wavesSystem;
        [SerializeField] CoinSystem coinSystem;
        [SerializeField] DeckSystem deckSystem;

        private PhaseSystem _phaseSystem;
        private BoardSystem _boardSystem;


        private void Awake()
        {
            lifeSystem.SetData(currentRaid);
            wavesSystem.SetData(currentRaid);
            coinSystem.SetData(currentRaid);

            _boardSystem = GetComponent<BoardSystem>();
            _phaseSystem = GetComponent<PhaseSystem>();
            memberSystem.Init();
            DictionaryTeamBattle.Init();
        }


        private void Start()
        {
            Setter();
        }

        private void Setter()
        {   
            SetPhaseSystem();
            SetBoardSystem();
        }

        private void SetBoardSystem()
        {
            _boardSystem.Player = memberSystem;
        }

        private void SetPhaseSystem()
        {
            _phaseSystem.Player = memberSystem;
            _phaseSystem.LifeSystem = lifeSystem;
            _phaseSystem.WaveSystem = wavesSystem;
            _phaseSystem.BoardSystem = _boardSystem;
            _phaseSystem.DeckSystem = deckSystem;
        }
    }
}

