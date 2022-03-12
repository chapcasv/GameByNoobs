using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PH.GraphSystem;
using DG.Tweening;

namespace PH
{
    public class ResultBattleUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI resultText;
        [SerializeField] Button btnExitBattle;
        [SerializeField] Canvas main;
        [SerializeField] Canvas result;
        [SerializeField] Image label;
        [SerializeField] float during;

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
            _resultSystem.OnPlayerDefeated -= DisplayPlayerDefeated;
            _resultSystem.OnPlayerDefeated -= DisplayPlayerVictory;
            btnExitBattle.onClick.RemoveAllListeners();

            GridBoard.Reset();
            DictionaryTeamBattle.Reset();
            CardDropHistory.Reset();
            PlayerCacheUnitData.Reset();
            SceneManager.LoadScene(SceneSelect.RewardMatch.ToString());
        }

        private void DisplayPlayerVictory()
        {
            main.gameObject.SetActive(false);
            result.gameObject.SetActive(true);
            MoveLabel();
            SetText("Victory");
        }

        private void DisplayPlayerDefeated()
        {
            MoveLabel();
            SetText("Defeated");
            main.gameObject.SetActive(false);
            result.gameObject.SetActive(true);
        }

        private void SetText(string text)
        {
            resultText.rectTransform.DOScale(Vector3.one, during).SetEase(Ease.OutBack);
            resultText.text = text;
        }

        private void MoveLabel()
        {
            label.rectTransform.DOScale(Vector3.one, during).SetEase(Ease.OutBack);
        }


    }
}

