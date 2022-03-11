using PH.Loader;
using PH.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace PH
{
    public class ResultMatchUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI coinRewardText;
        [SerializeField] TextMeshProUGUI diamondRewardText;
        [SerializeField] TextMeshProUGUI rankReward;
        [SerializeField] Image rankIcon;

        [SerializeField] Button b_back;
        [SerializeField] Button b_againt;

        private PlayerLocalSO _playerLocalSO;
        private RankSystem _rankSystem;

        void Start()
        {
            AddListerner();
            DisplayCurrentRankIcon();
        }

        public void Constructor(RankSystem rs, PlayerLocalSO playerLocalSO)
        {
            _rankSystem = rs;
            _playerLocalSO = playerLocalSO;
        }

        public void DisplayCoinReward(int coinRewardValue)
        {
            coinRewardText.text = "Tiền Xu +"+ coinRewardValue;
        }

        public void DisplayDiamonReward(int diamondRewardValue)
        {
            diamondRewardText.text = "" + diamondRewardValue;
        }

        private void DisplayCurrentRankIcon()
        {
            Rank currentRank = ConvertRank.Form(SaveSystem.LoadRank());
            RankInstance instance = _rankSystem.GetRank(currentRank.GetRankTier);
            rankIcon.sprite = instance.Icon;

            rankIcon.rectTransform.DOScale(Vector2.one, 0.5f)
                .SetEase(Ease.OutBack);
        }

        public void DisplayRank(string tier, string level, string bonus)
        {
            rankReward.text = $"{tier} {level} + {bonus}đ";
        }


        private void AddListerner()
        {
            b_back.onClick.AddListener(() => LoadSystem.Load(SceneSelect.MainMenu));
            b_againt.onClick.AddListener(() => LoadSystem.Load(SceneSelect.FindMatch));
        }

        private void OnDisable()
        {
            b_back.onClick.RemoveAllListeners();
            b_againt.onClick.RemoveAllListeners();
        }
    }
}


