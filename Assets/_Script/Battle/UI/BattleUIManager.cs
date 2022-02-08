using UnityEngine;
using TMPro;
using System;

namespace PH
{
    public class BattleUIManager : MonoBehaviour
    {
        [SerializeField] HandZoneUI handZoneUI;

        [Header("Player Info")]
        [SerializeField] RectTransform battleInfo;
        [SerializeField] TextMeshPro memberAmountText;
        [SerializeField] TextMeshProUGUI coinText;
       

        [Header("Enemy Info")]
        [SerializeField] TextMeshProUGUI waveText;
       

        [Header("Result UI")]
        [SerializeField] ResultBattleUI resultBattleUI;

        [SerializeField] BattleLifeUI lifePointUI;

        private LifeSystem _lifeSystem;
        private WaveSystem _wavesSystem;
        private CoinSystem _coinSystem;
        private MemberSystem _memberSystem;
        private ResultSystem _resultBattle;
        private GetBaseProperties _getBaseProperties;
        private CardInfoVisual _cardInfoViz;

        private int _maxMemberAmount = 9;
        private int _maxWave;

        private void Awake()
        {
            
        }

        void Start()
        {
            resultBattleUI.Constructor(_resultBattle);
            lifePointUI.Constructor(_lifeSystem);
        }

        private void OnDisable()
        {   
            RemoveListerner();
        }

        public void Constructor(LifeSystem LS, WaveSystem WS, CoinSystem CS, MemberSystem MS, ResultSystem RS, CardInfoVisual CIV)
        {
            _lifeSystem = LS;
            _wavesSystem = WS;
            _coinSystem = CS;
            _memberSystem = MS;
            _resultBattle = RS;
            _cardInfoViz = CIV;

            handZoneUI.SetCardInfomation(_cardInfoViz);

            AddListener();
            SetUpBattleInfomation();
        }

        private void MoveBattleInfo()
        {
            battleInfo.gameObject.SetActive(true);
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
            int currentWaveIndex = _wavesSystem.GetCurrentIndex();
            waveText.text = $"{currentWaveIndex + 1} / {_maxWave}";
        }

        

        private void DisplayMemberAmount()
        {
            int amount = _memberSystem.GetMemberAmount;
            memberAmountText.text = amount.ToString() + $"/{_maxMemberAmount}";
        }


        private void DisplayerCoin() => coinText.text = _coinSystem.GetCoin().ToString();

        private void AddListener()
        {
            _memberSystem.OnMemberAmountChange += DisplayMemberAmount;
            _wavesSystem.OnWaveIndexChange += DisplayWaves;
            _coinSystem.OnCoinValueChange += DisplayerCoin;
        }

        private void RemoveListerner()
        {
            _memberSystem.OnMemberAmountChange -= DisplayMemberAmount;
            _wavesSystem.OnWaveIndexChange -= DisplayWaves;
            _coinSystem.OnCoinValueChange -= DisplayerCoin;
        }
       
    }
}

