using UnityEngine;

namespace PH
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] UnitsDatabaseSO dataSO;
        [SerializeField] ALLCard aLLCard;
        [SerializeField] PVE_Raid currentRaid;
        [SerializeField] BattleUIManager battleUIManager;
        [SerializeField] DragCardSelected dragCardSelected;
        [SerializeField] CardInfoVisual cardInfoVisual;

        [Header("Container")]
        [SerializeField] SystemContainer systemContainer;
        [SerializeField] FactionContainer factionContainer;

        [Header("Extension Methods")]
        [SerializeField] GetBaseProperties GBP;

        private PhaseSystem _phaseSystem;
        private BoardSystem _boardSystem;

        /// Constructor Injection
        private void Awake()
        {
            var LS = systemContainer.GetLifeSystem();
            var WS = systemContainer.GetWaveSystem();
            var CS = systemContainer.GetCoinSystem();
            var RS = systemContainer.GetResultSystem();
            var MS = systemContainer.GetMemberSystem();
            var DS = systemContainer.GetDeckSystem();
            var SS = systemContainer.GetSpawnSystem();

            SetSystemByCurrentRaid(LS, WS, CS, MS);

            cardInfoVisual.Init(aLLCard);
            battleUIManager.Constructor(LS, WS, CS, MS, RS, cardInfoVisual);

            _boardSystem = GetComponent<BoardSystem>();
            _boardSystem.Constructor(MS, SS, DS,cardInfoVisual,dataSO);

            _phaseSystem = GetComponent<PhaseSystem>();
            _phaseSystem.Constructor(_boardSystem, LS, WS, DS, CS, RS);

            SetExtensionMethods(DS);

            DictionaryTeamBattle.Init(factionContainer);
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

