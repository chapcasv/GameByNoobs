using UnityEngine;
using TMPro;
using System;

namespace PH
{
    public class BattleUIManager : MonoBehaviour
    {
        [Header("Player Info")]
        [SerializeField] RectTransform battleInfo;
        [SerializeField] TextMeshProUGUI memberAmountText;
        [SerializeField] TextMeshProUGUI coinText;
        [SerializeField] TextMeshProUGUI playerLifeText;

        [Header("Enemy Info")]
        [SerializeField] TextMeshProUGUI waveText;
        [SerializeField] TextMeshProUGUI enemyLifeText;

        [Header("Result UI")]
        [SerializeField] ResultBattleUI resultBattleUI;

        private LifeSystem _lifeSystem;
        private WaveSystem _wavesSystem;
        private CoinSystem _coinSystem;
        private MemberSystem _memberSystem;
        private ResultSystem _resultBattle;

        private const int maxMemberAmount = 9;
        private int _maxWave;

        private void Awake()
        {
            AddListener();
        }

        void Start()
        {
            resultBattleUI.Constructor(_resultBattle);
            SetUpBattleInfomation();
        }

        public void Constructor(LifeSystem LS, WaveSystem WS, CoinSystem CS, MemberSystem MS, ResultSystem RS)
        {
            _lifeSystem = LS;
            _wavesSystem = WS;
            _coinSystem = CS;
            _memberSystem = MS;
            _resultBattle = RS;
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
            DisplayPlayerLife();
            DisplayEnemyLife();
        }
        private void DisplayWaves()
        {
            ///Index start is 0
            int currentWaveIndex = _wavesSystem.GetCurrentIndex();
            waveText.text = $"{currentWaveIndex + 1} / {_maxWave}";
        }

        private void DisplayEnemyLife()
        {   
            int life = _lifeSystem.GetEnemyLife();
            enemyLifeText.text = life.ToString();
        }

        private void DisplayMemberAmount()
        {
            int amount = _memberSystem.GetMemberAmount;
            memberAmountText.text = amount.ToString() + $"/{maxMemberAmount}";
        }

        private void DisplayPlayerLife() 
        {
            int life = _lifeSystem.GetPlayerLife();
            playerLifeText.text = life.ToString();
        } 

        private void DisplayerCoin() => coinText.text = _coinSystem.GetCoin().ToString();

        private void AddListener()
        {
            StartCardPhase.OnComplete += MoveBattleInfo;
            _memberSystem.OnMemberAmountChange += DisplayMemberAmount;

            _lifeSystem.OnEnemyLifeChange += DisplayEnemyLife;
            _lifeSystem.OnPlayerLifeChange += DisplayPlayerLife;

            _wavesSystem.OnWaveIndexChange += DisplayWaves;

            _coinSystem.OnCoinValueChange += DisplayerCoin;

        }

       
    }
}

