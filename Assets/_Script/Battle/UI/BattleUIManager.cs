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
        [SerializeField] LocalPlayer player;

        [Header("BattleInfo")]
        [SerializeField] RectTransform battleInfo;
        [SerializeField] TextMeshProUGUI memberAmountText;
        [SerializeField] TextMeshProUGUI coinText;
        [SerializeField] TextMeshProUGUI playerLifeText;
        [SerializeField] TextMeshProUGUI waveText;

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
        }
        private void DisplayWaves()
        {   
            ///Index start is 0
            int currentWave = PhaseSystem.WaveIndex;
            waveText.text = $"{currentWave + 1} / {_maxWave}";
        }

        private void DisplayMemberAmount()
        {
            int amount = player.GetMemberAmount;
            memberAmountText.text = amount.ToString() + $"/{maxMemberAmount}";
        }

        private void DisplayPlayerLife() => playerLifeText.text = player.GetLife.ToString();

        private void DisplayerCoin() => coinText.text = player.GetCoin.ToString();

        private void AddListener()
        {
            StartCardSystem.OnComplete += MoveBattleInfo;
            player.OnMemberAmountChange += DisplayMemberAmount;
            player.OnCoinNumberChange += DisplayerCoin;
            player.OnLifeValueChange += DisplayPlayerLife;
        }
    }
}

