using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using System;
using UnityEngine.UI;
using TMPro;

namespace PH
{

    public class BattleUIManager : MonoBehaviour
    {
        [SerializeField] PVE_Raid currentRaid;
        [SerializeField] LifeSystem lifeSystem;
        [SerializeField] WaveSystem wavesSystem;
        [SerializeField] CoinSystem coinSystem;
        [SerializeField] MemberSystem player;

        [Header("Player Info")]
        [SerializeField] RectTransform battleInfo;
        [SerializeField] TextMeshProUGUI memberAmountText;
        [SerializeField] TextMeshProUGUI coinText;
        [SerializeField] TextMeshProUGUI playerLifeText;

        [Header("Enemy Info")]
        [SerializeField] TextMeshProUGUI waveText;
        [SerializeField] TextMeshProUGUI enemyLifeText;

        private const int maxMemberAmount = 9;
        private int _maxWave;

        private void Awake()
        {
            AddListener();
            _maxWave = currentRaid.Waves.Length;

        }

        void Start()
        {
            SetUpBattleInfomation();
        }


        private void MoveBattleInfo()
        {
            battleInfo.gameObject.SetActive(true);
        }


        private void SetUpBattleInfomation()
        {
            DisplayWaves();
            DisplayMemberAmount();
            DisplayerCoin();
            DisplayPlayerLife();
            DisplayEnemyLife();
        }
        private void DisplayWaves()
        {
            ///Index start is 0
            int currentWaveIndex = wavesSystem.GetCurrentIndex();
            waveText.text = $"{currentWaveIndex + 1} / {_maxWave}";
        }

        private void DisplayEnemyLife()
        {   
            int life = lifeSystem.GetEnemyLife();
            enemyLifeText.text = life.ToString();
        }

        private void DisplayMemberAmount()
        {
            int amount = player.GetMemberAmount;
            memberAmountText.text = amount.ToString() + $"/{maxMemberAmount}";
        }

        private void DisplayPlayerLife() 
        {
            int life = lifeSystem.GetPlayerLife();
            playerLifeText.text = life.ToString();
        } 

        private void DisplayerCoin() => coinText.text = coinSystem.GetCoin().ToString();

        private void AddListener()
        {
            StartCardPhase.OnComplete += MoveBattleInfo;
            player.OnMemberAmountChange += DisplayMemberAmount;

            lifeSystem.OnEnemyLifeChange += DisplayEnemyLife;
            lifeSystem.OnPlayerLifeChange += DisplayPlayerLife;
            wavesSystem.OnWaveIndexChange += DisplayWaves;
            coinSystem.OnCoinValueChange += DisplayerCoin;
        }
    }
}

