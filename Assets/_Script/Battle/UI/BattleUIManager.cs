using UnityEngine;
using TMPro;
using System;

namespace PH
{
    public class BattleUIManager : MonoBehaviour
    {
        [SerializeField] BattleNotifyUI notifyPhase;
        [SerializeField] HandZoneUI handZoneUI;
        [SerializeField] TextMeshProUGUI waveText;
        [SerializeField] ResultBattleUI resultBattleUI;
        [SerializeField] BattleLifeUI lifePointUI;

        [Header("Member player")]
        [SerializeField] TextMeshPro memberAmountText;
        [SerializeField] GameObject memberObj;

        [Header("Phase")]
        [SerializeField] StartRoundPhase startRound;
        [SerializeField] BeforeTeamFight beforeTeamFight;

        [Header("Sell Button")]
        [SerializeField] SellUnit left;
        [SerializeField] SellUnit right;

        private PlayModeEnemy _currentEnemy;
        private PlayerLocalSO _player;

        private LifeSystem _lifeSystem;
        private WaveSystem _wavesSystem;
        private CoinSystem _coinSystem;
        private MemberSystem _memberSystem;
        private ResultSystem _resultBattle;
        private GetBaseProperties _getBaseProperties;
        private CardInfoBattle _cardInfoViz;
        private PlayerDragLogic _playerDragLogic;

        private BattlePlayerInfomation infomation;
        private BattleCoinUI battleCoinUI;

        private const int MAX_MEMBER = 9;
        //private int _maxWave;

        private void Awake()
        {
            infomation = GetComponent<BattlePlayerInfomation>();
            battleCoinUI = GetComponent<BattleCoinUI>();
        }

        void Start()
        {
            resultBattleUI.Constructor(_resultBattle);
            lifePointUI.Constructor(_lifeSystem);
            infomation.Constructor(_currentEnemy, _player);
            battleCoinUI.Constructor(_coinSystem);
            ResetMemberAmount();

            startRound.notifyPhase = notifyPhase;
        }

        public void Constructor(LifeSystem LS, WaveSystem WS, CoinSystem CS, MemberSystem MS, ResultSystem RS,
            CardInfoBattle CIV, PlayerDragLogic playerDragLogic, PlayerLocalSO player, PlayModeEnemy currentEnemy)
        {
            _lifeSystem = LS;
            _wavesSystem = WS;
            _coinSystem = CS;
            _memberSystem = MS;
            _resultBattle = RS;
            _cardInfoViz = CIV;
            _player = player;
            _currentEnemy = currentEnemy;
            _playerDragLogic = playerDragLogic;

            handZoneUI.Setter(_cardInfoViz, _playerDragLogic);

            AddListener();
            SetUpBattleInfomation();
        }

        private void AddListener()
        {
            _memberSystem.OnMemberAmountChange += DisplayMemberAmount;
            _wavesSystem.OnWaveIndexChange += DisplayWaves;

            startRound.OnEnterPhase += ShowMemberOBJ;
            beforeTeamFight.OnEnterPhase += HidenMemberOBJ;
            beforeTeamFight.OnEnterPhase += notifyPhase.SetNotifyBeforeBattle;

            _playerDragLogic.OnBeginDrag += ShowRemoveButton;
            _playerDragLogic.OnEndDrag += HidenRemoveButton;
        }

        private void SetUpBattleInfomation()
        {
            DisplayWaves();
            DisplayMemberAmount();
        }

        private void DisplayWaves()
        {
            //Index start is 0 
            waveText.text = _wavesSystem.GetCurrentIndexString();
            //waveText.text = $"{currentWaveIndex + 1} / {_maxWave}";
        }

        #region Member UI
        private void DisplayMemberAmount()
        {
            int amount = _memberSystem.GetMemberAmount;
            memberAmountText.text = amount + $"/{MAX_MEMBER}";
        }

        /// <summary>
        /// When start new battle,
        /// reset current member amount to zero
        /// </summary>
        private void ResetMemberAmount() => memberAmountText.text = 0 + $"/{MAX_MEMBER}";

        private void ShowMemberOBJ() => memberObj.SetActive(true);

        private void HidenMemberOBJ() => memberObj.SetActive(false);

        #endregion



        private void ShowRemoveButton()
        {
            left.gameObject.SetActive(true);
            right.gameObject.SetActive(true);
        }

        private void HidenRemoveButton()
        {
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
        }

        private void OnDisable() => RemoveListerner();

        private void RemoveListerner()
        {
            _memberSystem.OnMemberAmountChange -= DisplayMemberAmount;
            _wavesSystem.OnWaveIndexChange -= DisplayWaves;

            startRound.OnEnterPhase -= ShowMemberOBJ;
            beforeTeamFight.OnEnterPhase -= HidenMemberOBJ;
            beforeTeamFight.OnEnterPhase -= notifyPhase.SetNotifyBeforeBattle;

            _playerDragLogic.OnBeginDrag -= ShowRemoveButton;
            _playerDragLogic.OnEndDrag -= HidenRemoveButton;
        }   
    }
}

