using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PH
{
    public class ResultBattleUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI resultText;
        [SerializeField] Button btnExitBattle;

        private ResultSystem _resultSystem;

        public void Constructor(ResultSystem result)
        {
            _resultSystem = result;
            _resultSystem.OnPlayerDefeated += DisplayPlayerDefeated;
            _resultSystem.OnPlayerVictory += DisplayPlayerVictory;
            btnExitBattle.onClick.AddListener(ExitBattle);
        }

        private void ExitBattle()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }

        private void DisplayPlayerVictory()
        {
            resultText.text = "Victory";
            gameObject.SetActive(true);
        }

        private void DisplayPlayerDefeated()
        {
            resultText.text = "Defeated";
            gameObject.SetActive(true);
        }
    }
}

