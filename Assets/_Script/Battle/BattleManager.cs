using UnityEngine;

namespace PH
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] UnitsDatabaseSO dataSO;
        [SerializeField] ALLCard aLLCard;

        [SerializeField] BoardGeneration board;
        [SerializeField] PlayerLocalSO player;
        [SerializeField] BattleUIManager battleUIManager;
        [SerializeField] DragCardSelected dragCardSelected;
        [SerializeField] CardInfoBattle cardInfoVisual;

        [Header("Play Mode Infomation")]
        [SerializeField] PlayModeRewards playModeRewards;
        [SerializeField] PlayModeEnemy currentEnemy;

        [Header("Use for Test")]
        [SerializeField] PVPMode defaultMode;
        [SerializeField] ModeSubAI defaultSub;

        [Header("Container")]
        [SerializeField] SystemContainer systemContainer;
        [SerializeField] FactionContainer factionContainer;

        [Header("Extension Methods")]
        [SerializeField] GetBaseProperties GBP;

        [Header("Drag Logic")]
        [SerializeField] PlayerDragLogic playerDragLogic;
        [SerializeField] EnemyDragLogic enemyDragLogic;

        private PhaseSystem _phaseSystem;
        private BoardSystem _boardSystem;

        /// Constructor Injection
        private void Awake()
        {
            LoadEnemy();

            var LS = systemContainer.GetLifeSystem();
            var WS = systemContainer.GetWaveSystem();
            var CS = systemContainer.GetCoinSystem();
            var RS = systemContainer.GetResultSystem();
            var MS = systemContainer.GetMemberSystem();
            var DS = systemContainer.GetDeckSystem();
            var SS = systemContainer.GetSpawnSystem();

            SS.SetEnemyDragLogic = enemyDragLogic;
            SS.SetPlayerDragLogic = playerDragLogic;

            SetSystemByCurrentPlayMode(LS, WS, CS, MS, RS);

            cardInfoVisual.Init(aLLCard);
            cardInfoVisual.Initialized();
            battleUIManager.Constructor(LS, WS, CS, MS, RS, cardInfoVisual, playerDragLogic, player, currentEnemy);

            board.Constructor(currentEnemy);

            _boardSystem = GetComponent<BoardSystem>();
            _boardSystem.Constructor(MS, SS, DS, cardInfoVisual, dataSO);

            _phaseSystem = GetComponent<PhaseSystem>();
            _phaseSystem.Constructor(_boardSystem, LS, WS, DS, CS, RS, dragCardSelected);

            SetExtensionMethods(DS);

            DictionaryTeamBattle.Init(factionContainer);
            CardDropHistory.Init();
            PlayerCacheUnitData.Init();
            dragCardSelected.Constructor(CS, DS);

            SetterDragLogic();
        }

        private void LoadEnemy()
        {
            if (PlayModeData.CurrentMode == null)
            {
#if UNITY_EDITOR
                PlayModeData.CurrentMode = defaultMode;
                PlayModeData.CurrentMode.ModeSub = defaultSub;
                Debug.LogWarning("Current Mode is null !!!");
#endif
            }
            currentEnemy = PlayModeData.CurrentMode.ModeSub.GetEnemy();
        }

        private void SetterDragLogic()
        {
            playerDragLogic.CardInfoVisual = cardInfoVisual;
            playerDragLogic.SetCam = Camera.main;

            enemyDragLogic.CardInfoVisual = cardInfoVisual;
            enemyDragLogic.SetCam = Camera.main;
        }

        private void SetExtensionMethods(DeckSystem DS)
        {
            DS.GetBaseProperties = GBP;
        }

        private void SetSystemByCurrentPlayMode(LifeSystem LS, WaveSystem WS, CoinSystem CS, 
            MemberSystem MS, ResultSystem RS)
        {
            LS.SetData(currentEnemy);
            WS.SetData(currentEnemy);
            CS.SetData();
            MS.SetData();
            RS.PlayModeRewards = playModeRewards;
        }
    }
}

