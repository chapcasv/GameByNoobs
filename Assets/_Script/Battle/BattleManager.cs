using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] LocalPlayer localPlayer;
        [SerializeField] PVE_Raid currentRaid;
        private PhaseSystem _phaseSystem;
        private BoardSystem _boardSystem;


        private void Awake()
        {
            _boardSystem = GetComponent<BoardSystem>();
            _phaseSystem = GetComponent<PhaseSystem>();
            localPlayer.Init(currentRaid.PlayerStartCoin,currentRaid.PlayerLife);
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
            _boardSystem.Player = localPlayer;
        }

        private void SetPhaseSystem()
        {
            _phaseSystem.Player = localPlayer;
            _phaseSystem.BoardSystem = _boardSystem;
            _phaseSystem.CopyWave(currentRaid.Waves);
        }
    }
}

