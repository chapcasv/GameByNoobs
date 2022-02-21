using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace PH
{
    public class BattleCoinUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI coinText;
        [SerializeField] TextMeshProUGUI playerCoinInfoText;
        [SerializeField] TextMeshProUGUI enemyCoinInfoText;

        private CoinSystem _coinSystem;

        public void Constructor(CoinSystem cs)
        {
            _coinSystem = cs;

            _coinSystem.OnCoinValueChange += DisplayCoin;
            _coinSystem.OnCoinValueChange += DisplayPlayerCoinInfo;
            _coinSystem.OnEnemyCoinChange += DisplayEnemyCoinInfo;
        }

        private void DisplayCoin() => coinText.text = _coinSystem.GetCoin().ToString();

        private void DisplayPlayerCoinInfo() => playerCoinInfoText.text = _coinSystem.GetCoin().ToString();

        private void DisplayEnemyCoinInfo() => enemyCoinInfoText.text = _coinSystem.GetEnemyCoin().ToString();


        private void OnDisable()
        {
            _coinSystem.OnCoinValueChange -= DisplayCoin;
            _coinSystem.OnCoinValueChange -= DisplayPlayerCoinInfo;
            _coinSystem.OnEnemyCoinChange -= DisplayEnemyCoinInfo;
        }
    }
}

