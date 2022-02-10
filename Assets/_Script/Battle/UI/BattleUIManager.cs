using UnityEngine;
using TMPro;
using System;

namespace PH
{
    public class BattleUIManager : MonoBehaviour
    {
        [SerializeField] HandZoneUI handZoneUI;

        [SerializeField] TextMeshProUGUI coinText;

        [SerializeField] TextMeshProUGUI waveText;

        [SerializeField] ResultBattleUI resultBattleUI;
        [SerializeField] BattleLifeUI lifePointUI;

        [Header("Member player")]
        [SerializeField] TextMeshPro memberAmountText;
        [SerializeField] GameObject memberObj;

        [Header("Phase")]
        [SerializeField] DrawCard drawCard;
        [SerializeField] BeforeTeamFight beforeTeamFight;

        [Header("Sell Button")]
        [SerializeField] SellUnit left;
        [SerializeField] SellUnit right;

        private LifeSystem _lifeSystem;
        private WaveSystem _wavesSystem;
        private CoinSystem _coinSystem;
        private MemberSystem _memberSystem;
        private ResultSystem _resultBattle;
        private GetBaseProperties _getBaseProperties;
        private CardInfoVisual _cardInfoViz;
        private PlayerDragLogic _playerDragLogic;

        private int _maxMemberAmount = 9;
        private int _maxWave;

        void Start()
        {
            resultBattleUI.Constructor(_resultBattle);
            lifePointUI.Constructor(_lifeSystem);
            ResetMemberAmount();
        }

        private void OnDisable()
        {   
            RemoveListerner();
        }

        public void Constructor(LifeSystem LS, WaveSystem WS, CoinSystem CS, MemberSystem MS, ResultSystem RS,
            CardInfoVisual CIV, PlayerDragLogic playerDragLogic)
        {
            _lifeSystem = LS;
            _wavesSystem = WS;
            _coinSystem = CS;
            _memberSystem = MS;
            _resultBattle = RS;
            _cardInfoViz = CIV;

            _playerDragLogic = playerDragLogic;

            handZoneUI.SetCardInfomation(_cardInfoViz);

            AddListener();
            SetUpBattleInfomation();
        }

        private void SetUpBattleInfomation()
        {
            _maxWave = _wavesSystem.GetWavesLength();
            DisplayWaves();
            DisplayMemberAmount();
            DisplayerCoin();
        }
        private void DisplayWaves()
        {
            ///Index start is 0
            int currentWaveIndex = _wavesSystem.GetCurrentIndex() + 1;
            waveText.text = currentWaveIndex.ToString();
            //waveText.text = $"{currentWaveIndex + 1} / {_maxWave}";
        }

        private void DisplayMemberAmount()
        {
            int amount = _memberSystem.GetMemberAmount;
            memberAmountText.text = amount + $"/{_maxMemberAmount}";
        }

        
        private void ResetMemberAmount()
        {
            memberAmountText.text = 0 + $"/{_maxMemberAmount}";
        }

        private void ShowMemberOBJ() => memberObj.SetActive(true);
        private void HidenMemberOBJ() => memberObj.SetActive(false);

        private void DisplayerCoin() => coinText.text = _coinSystem.GetCoin().ToString();

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

        private void AddListener()
        {
            _memberSystem.OnMemberAmountChange += DisplayMemberAmount;
            _wavesSystem.OnWaveIndexChange += DisplayWaves;
            _coinSystem.OnCoinValueChange += DisplayerCoin;

            drawCard.OnEnterDrawCard += ShowMemberOBJ;
            beforeTeamFight.OnEnterBeforeTeamFight += HidenMemberOBJ;

            _playerDragLogic.OnBeginDrag += ShowRemoveButton;
            _playerDragLogic.OnEndDrag += HidenRemoveButton;
        }

        private void RemoveListerner()
        {
            _memberSystem.OnMemberAmountChange -= DisplayMemberAmount;
            _wavesSystem.OnWaveIndexChange -= DisplayWaves;
            _coinSystem.OnCoinValueChange -= DisplayerCoin;

            drawCard.OnEnterDrawCard -= ShowMemberOBJ;
            beforeTeamFight.OnEnterBeforeTeamFight -= HidenMemberOBJ;

            _playerDragLogic.OnBeginDrag -= ShowRemoveButton;
            _playerDragLogic.OnEndDrag -= HidenRemoveButton;
        }
       
    }
}

