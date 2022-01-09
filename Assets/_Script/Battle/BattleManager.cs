using UnityEngine;

namespace PH
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] PVE_Raid currentRaid;
        [SerializeField] BattleUIManager battleUIManager;
        [SerializeField] DragCardSelected dragCardSelected;

        [Header("Container")]
        [SerializeField] SystemContainer container;

        [Header("Extension Methods")]
        [SerializeField] GetBaseProperties GBP;

        private PhaseSystem _phaseSystem;
        private BoardSystem _boardSystem;

        /// Constructor Injection
        private void Awake()
        {
            var LS = container.GetLifeSystem();
            var WS = container.GetWaveSystem();
            var CS = container.GetCoinSystem();
            var RS = container.GetResultSystem();
            var MS = container.GetMemberSystem();
            var ES = container.GetEquipmentSystem();
            var DS = container.GetDeckSystem();
            var CSS = container.GetCastSpellSystem();
            var SS = container.GetSpawnSystem();

            SetSystemByCurrentRaid(LS, WS, CS, MS);

            battleUIManager.Constructor(LS, WS, CS, MS, RS);

            _boardSystem = GetComponent<BoardSystem>();
            _boardSystem.Constructor(MS, ES, CSS, SS);

            _phaseSystem = GetComponent<PhaseSystem>();
            _phaseSystem.Constructor(_boardSystem, LS, WS, DS, CS, RS);

            SetExtensionMethods(DS);

            DictionaryTeamBattle.Init();
            PlayerCacheUnitData.Init();
            dragCardSelected.Constructor(CS, DS, GBP);
        }

        private void SetExtensionMethods(DeckSystem DS)
        {
            DS.GetBaseProperties = GBP;
        }

        private void SetSystemByCurrentRaid(LifeSystem LS, WaveSystem WS, CoinSystem CS, MemberSystem MS)
        {
            LS.SetData(currentRaid);
            WS.SetData(currentRaid);
            CS.SetData(currentRaid);
            MS.SetData();
        }
    }
}

